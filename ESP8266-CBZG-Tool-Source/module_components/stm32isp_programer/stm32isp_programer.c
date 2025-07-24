/* 
 * Copyright (c) 2025, 长不着弓
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain this notice.
 * 2. Redistributions in binary form must reproduce this notice in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of 长不着弓 nor the names of its contributors may be used
 *    to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DAMAGES.
 */
#include "stm32isp_programer.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "STM32ISPPROG";
static char NAME[] = CONFIG_STM32ISPPROG_MODULE_NAME;
static char BRIEF[] = CONFIG_STM32ISPPROG_BRIEF;
static char VERSION[] = CONFIG_STM32ISPPROG_VERSION;
static char CMD_NAME[] = CONFIG_STM32ISPPROG_CMD_NAME;
#ifndef CONFIG_STM32ISPPROG_RUNNING
#define CONFIG_STM32ISPPROG_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_STM32ISPPROG_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};

#define USER_ACK    "\171"
#define USER_NACK   "\037"
#define TIMEOUT     CONFIG_DEVICE_RESPONSE_TIMEOUT
#define QUEUE_RECEIVE_TIMEOUT 500
#ifdef CONFIG_GPIO0_BOOT
gpio_num_t  BOOT_PIN = GPIO_NUM_0;
#elif CONFIG_GPIO0_RST
gpio_num_t  RST_PIN = GPIO_NUM_0;
#endif

#ifdef CONFIG_GPIO2_BOOT
gpio_num_t  BOOT_PIN = GPIO_NUM_2;
#elif CONFIG_GPIO2_RST
gpio_num_t  RST_PIN = GPIO_NUM_2;
#endif

enum
{
    BAUD_SYNC = 0x7F,
    GET = 0x00,
    GET_VERSION = 0x01,
    GET_ID = 0x02,
    READ_MEMORY = 0x11,
    GO = 0x21,
    WRITE_MEMORY = 0x31,
    ERASE = 0x43,
    EXTENDED_ERASE = 0x44,
    WRITE_PROTECT = 0x63,
    WRITE_UNPROTECT = 0x73,
    READOUT_PROTECT = 0x82,
    READOUT_UNPROTECT = 0x92
};

enum
{
    ACK = 0x79,
    NACK = 0x1F
};

enum ERR_TYPE
{
    NO_PROBLEM = 0x0000,
    CONNECT_ERR = 0x1000,
    CREATE_TASK_ERR = 0x1001,
    CREATE_QUEUE_ERR = 0x1002,
    DEVICE_CONNECT_ERR = 0x1003,
    DEVICE_TIMEOUT = 0x1004,
    UART_ERR = 0x1005,
    BAUD_SYNC_ERR = 0x7F,
    GET_ERR = 0x00,
    GET_VERSION_ERR = 0x01,
    GET_ID_ERR = 0x02,
    READ_MEMORY_ERR = 0x11,
    GO_ERR = 0x21,
    WRITE_MEMORY_ERR = 0x31,
    ERASE_ERR = 0x43,
    EXTENDED_ERASE_ERR = 0x44,
    WRITE_PROTECT_ERR = 0x63,
    WRITE_UNPROTECT_ERR = 0x73,
    READOUT_PROTECT_ERR = 0x82,
    READOUT_UNPROTECT_ERR = 0x92
};

static CommandPack_t UartReceive(uint32_t TimeOutMs);
static void set_target_isp_mode(bool enable);
static bool send_sync_cmd(void);
static void ProgramerTask(void *arg);
static void UartReceiveTask(void *arg);
static uint64_t JsonGetFileSize(cJSON* json);
static int PackDivided(CommandPack_t* Pack,QueueHandle_t Queue,int Size);
extern void user_gpio_init(void);

extern int tcp_socket;
extern bool BufferLock[8];
static QueueHandle_t WifiDataQueue;
static QueueHandle_t UartDataQueue;
static TaskHandle_t ProgramerHandle;
static TaskHandle_t UartReceiveTaskHandle;
static bool STOPFlag = false;
static enum ERR_TYPE ErrStatus;
static TickType_t TimeoutTick;
static const uint64_t Addr = CONFIG_DEVICE_FLASH_PROG_START_ADDR;
static uint64_t FileSize = 0;
static bool InitParamReceived = false;
uart_parity_t parity_mode;



static void* InitAction(void *arg)
{    
    STOPFlag = false;
    InitParamReceived = false;
    ErrStatus = BAUD_SYNC_ERR;
    heart_beat_enable(false);
    user_gpio_init();

    SetActionAddLock(&Command,true,100);
    uart_get_parity(UART_NUM_0,&parity_mode);
    if(parity_mode != UART_PARITY_EVEN)
    {
        uart_set_parity(UART_NUM_0,UART_PARITY_EVEN);
    }

    WifiDataQueue = xQueueGenericCreate(
                    64,
                    sizeof(CommandPack_t),
                    queueQUEUE_TYPE_BASE 
                );

    if(WifiDataQueue == NULL)
    {
        ESP_LOGE(TAG,"WifiDataQueue Create Error");
        ErrStatus = CREATE_QUEUE_ERR;
        RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        return &ERR_FLAG;
    }
    ESP_LOGI(TAG,"WifiDataQueue Create Success");

    UartDataQueue = xQueueGenericCreate(
                8,
                sizeof(CommandPack_t),
                queueQUEUE_TYPE_BASE 
            );

    if(UartDataQueue == NULL)
    {
        ESP_LOGE(TAG,"UartDataQueue Create Error");
        ErrStatus = CREATE_QUEUE_ERR;
        RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        return &ERR_FLAG;
    }
    ESP_LOGI(TAG,"UartDataQueue Create Success");

    BaseType_t Ret = xTaskCreate(
        ProgramerTask,
        "Programer",
        2048,
        NULL,
        5,
        &ProgramerHandle
    );

    if(Ret == pdFALSE)
    {
        ESP_LOGE(TAG,"ProgramerTask Create Error");
        ErrStatus = CREATE_TASK_ERR;
        RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        return &ERR_FLAG;
    }
    ESP_LOGI(TAG,"ProgramerTask Create Success");

    Ret = xTaskCreate(
        UartReceiveTask,
        "UartReceiveTask",
        3840,
        NULL,
        5,
        &UartReceiveTaskHandle
    );

    if(Ret == pdFALSE)
    {
        ESP_LOGE(TAG,"UartReceiveTask Create Error");
        ErrStatus = CREATE_TASK_ERR;
        RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        return &ERR_FLAG;
    }
    ESP_LOGI(TAG,"UartReceiveTask Create Success");

    set_target_isp_mode(true);

    ssize_t size = lwip_send(tcp_socket,USER_ACK,1,0);

    if(size <= 0)
    {
        ESP_LOGE(TAG,"Connect to Server Err");
        ErrStatus = CONNECT_ERR;
        RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        return &ERR_FLAG;
    }
    ESP_LOGI(TAG,"Connect to Server");
    TimeoutTick = xTaskGetTickCount();

    return NULL;
}

static void* Action(void *arg)
{
    static uint64_t RecvFileLength = 0;
    if(arg == NULL)
    {
        if((xTaskGetTickCount()-TimeoutTick > pdMS_TO_TICKS(TIMEOUT)))
        {
            STOPFlag = true;
            RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
        }
        return NULL;
    }

    CommandPack_t *CommandPack = (CommandPack_t *)arg;

    TimeoutTick = xTaskGetTickCount();

    if(InitParamReceived == false)
    {
        InitParamReceived = true;
        RecvFileLength = 0;
        cJSON* Root = cJSON_Parse((char *)CommandPack->Data);
        FileSize = JsonGetFileSize(Root);
        ESP_LOGI(TAG,"FileSize:%llu",FileSize);
        lwip_send(tcp_socket,"c",1,0);
        return NULL;
    }

    *CommandPack->Lock = true;

    RecvFileLength += PackDivided(CommandPack,WifiDataQueue,256);

    if(RecvFileLength == FileSize || (RecvFileLength % 10240 == 0 && RecvFileLength != 0))
    {
        xTaskNotifyGive(ProgramerHandle);
    }

    return NULL;
}

static void* EXTIAction(void *arg)
{
    SetActionAddLock(&Command,false,10);
    uart_set_parity(UART_NUM_0,parity_mode);
    
    STOPFlag = true;
    for(int i = 0;i < 8;i++)
    {
        BufferLock[i] = false;
    }

    if(UartReceiveTaskHandle!=NULL)
    {
        UartReceiveTaskHandle = NULL;
    }
 
    xTaskNotifyGive(ProgramerHandle);
    if(ProgramerHandle!=NULL)
    {
        ProgramerHandle = NULL;
    }

    if(ErrStatus != NO_PROBLEM)
    {
        uint8_t data[1] = {ErrStatus};
        lwip_send(tcp_socket,data,1,0);
        ESP_LOGI(TAG,"Code upload failed");
    }
    else
    {
        lwip_send(tcp_socket,USER_ACK,1,0);
        ESP_LOGI(TAG,"Code has upload");
    }

    heart_beat_enable(true);
    return NULL;
}

void menu_stm32f10x_programer(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}

static CommandPack_t UartReceive(uint32_t TimeOutMs)
{
    CommandPack_t CommandPack;
    BaseType_t ret = xQueueReceive(UartDataQueue,&CommandPack,pdMS_TO_TICKS(TimeOutMs));
    if(ret == pdFALSE)
    {
        CommandPack.Length = 0;
    }
    return CommandPack;
}

static uint64_t JsonGetFileSize(cJSON* json)
{
    if(json == NULL)
    {
        return 0;
    }

    cJSON* FileSize = cJSON_GetObjectItem(json,"FileSize");
    if(FileSize == NULL)
    {
        return 0;
    }
    return FileSize->valuedouble;
}

static int PackDivided(CommandPack_t* CommandPack,QueueHandle_t Queue,int PackSize)
{
    int Count = 0;
    while (Count < CommandPack->Length)
    {
        int32_t Div =  CommandPack->Length - Count;
        CommandPack_t Pack =  
        {
            .Data = &CommandPack->Data[Count],
            .Length = Div>PackSize?PackSize:Div,
            .Lock = Div<=PackSize ? CommandPack->Lock:NULL
        };

        xQueueSend(
            Queue,
            &Pack,
            portMAX_DELAY
        );
        
        Count += Div>PackSize?PackSize:Div;
    }
    return Count;
}

static void UartWriteByte(uint8_t Byte)
{
    uart_write_bytes(UART_NUM_0,(char *)&Byte,1);
    uart_wait_tx_done(UART_NUM_0,portMAX_DELAY);
    TimeoutTick = xTaskGetTickCount();
}

static void UartWriteBytes(uart_port_t uart_num, char *src, size_t size)
{
    uart_write_bytes(uart_num,src,size);
    uart_wait_tx_done(UART_NUM_0,portMAX_DELAY);
    TimeoutTick = xTaskGetTickCount();
}

static void reset_target(void)
{
    gpio_set_level(RST_PIN,false);
    vTaskDelay(pdMS_TO_TICKS(CONFIG_DEVICE_RESTART_TIME));
    gpio_set_level(RST_PIN,true);
    vTaskDelay(pdMS_TO_TICKS(CONFIG_DEVICE_RESTART_TIME));
}

static void set_target_isp_mode(bool enable)
{
    if(enable)
    {
        gpio_set_level(BOOT_PIN,1);
        gpio_set_level(RST_PIN,0);
        vTaskDelay(CONFIG_DEVICE_RESTART_TIME);
        gpio_set_level(RST_PIN,1);
    }
    else
    {
        gpio_set_level(BOOT_PIN,0);
        gpio_set_level(RST_PIN,0);
        vTaskDelay(CONFIG_DEVICE_RESTART_TIME);
        gpio_set_level(RST_PIN,1);
    }
}

static bool ack_byte_check(uint32_t time_ms)
{
    CommandPack_t CommandPack = UartReceive(time_ms);

    if(CommandPack.Length == 0)
    {
        uart_flush(UART_NUM_0);
        return false;
    }
    else if(CommandPack.Length == -1)
    {
        return false;
    }
    else
    {
        
    }

    if(CommandPack.Data[0] == ACK)
    {
        return true;
    }
    else
    {
        uart_flush(UART_NUM_0);
        return false;
    }
}

static bool send_sync_cmd(void)
{
    int times = 0;
    while(true)
    {
        CommandPack_t CommandPack = {
            .Length = 0
        };
        UartWriteByte(BAUD_SYNC);
        CommandPack = UartReceive(100);
        if(CommandPack.Length == 0)
        {
            continue;
        }
        else
        {

        }

        if(CommandPack.Data[0] == ACK)
        {
            break;
        }
        else if(CommandPack.Data[0] == 0x1F)
        {
            if(times > 3)
            {
                return false;
            }
            reset_target();
            vTaskDelay(pdMS_TO_TICKS(100));
            times++;
            continue;
        }
        else
        {
            return false;
        }
    }
    return true;
}

static int8_t read_protect_check(void)
{
    CommandPack_t CommandPack = {
        .Data = (uint8_t *)"\0",
        .Length = 0
    };

    //这里是读取选项字节观察是否可写
    uint8_t Addr[5] = {0x1F,0xFF,0xF8,0x00,0x18};
    uint8_t LenReq[2] = {0x01,0xFE};

    UartWriteByte(READ_MEMORY);
    UartWriteByte(~READ_MEMORY);

    CommandPack = UartReceive(TIMEOUT);
    if(CommandPack.Length == 0)
    {
        return -1;
    }
    else if(CommandPack.Length == -1)
    {
        return -1;
    }
    else
    {

    }

    if(CommandPack.Data[0] == 0x1F)
    {
        return false;
    }
    else if(CommandPack.Data[0] == ACK)
    {
        
    }
    else
    {
        return -1;
    }
    
    UartWriteBytes(UART_NUM_0,(char *)Addr,sizeof(Addr));
    CommandPack = UartReceive(TIMEOUT);
    if(CommandPack.Length == 0)
    {
        return -1;
    }
        else if(CommandPack.Length == -1)
    {
        return -1;
    }
    else
    {
        
    }

    if(CommandPack.Data[0] == ACK)
    {
        UartWriteBytes(UART_NUM_0,(char *)LenReq,sizeof(LenReq));
        CommandPack = UartReceive(TIMEOUT);
        if(CommandPack.Length == 0)
        {
            return -1;
        }
        return true;
    }
    else
    {
        return -1;
    }
}

static bool erase_flash(void)
{
    ESP_LOGI(TAG,"Erase the flash memory");
    UartWriteByte(ERASE);
    UartWriteByte(~ERASE);

    if(!ack_byte_check(TIMEOUT))
    {
        return false;
    }

    UartWriteByte(0xFF);
    UartWriteByte(0x00);

    if(!ack_byte_check(TIMEOUT))
    {
        return false;
    }
    
    return true;
}

static bool write_flash(uint32_t Address,CommandPack_t CommandPack)
{
    unsigned char temp[5];
    temp[0] = ((Address>>24) & 0xFF);
    temp[1] = ((Address>>16) & 0xFF);
    temp[2] = ((Address>>8 ) & 0xFF);
    temp[3] = ((Address    ) & 0xFF);
    temp[4] = temp[0] ^ temp[1] ^ temp[2] ^ temp[3];

    UartWriteByte(WRITE_MEMORY);
    UartWriteByte(~WRITE_MEMORY);
    if(!ack_byte_check(TIMEOUT))
    {
        return false;
    }

    UartWriteBytes(UART_NUM_0,(char *)temp,sizeof(temp));
    if(!ack_byte_check(TIMEOUT))
    {
        return false;
    }

    uint8_t Length = (uint8_t)(CommandPack.Length - 1);
    uint8_t bcc_check_sum = Length;

    for(int i = 0;i<CommandPack.Length;i++)
    {
        bcc_check_sum = bcc_check_sum ^ CommandPack.Data[i];
    }

    UartWriteByte(Length);
    UartWriteBytes(UART_NUM_0,(char *)CommandPack.Data,CommandPack.Length);
    UartWriteByte(bcc_check_sum);
    
    if(!ack_byte_check(TIMEOUT))
    {
        return false;
    }
    
    return true;
}

static void UartReceiveTask(void *arg)
{
    static uint8_t UartData[8][384];
    CommandPack_t CommandPack;
    uint8_t Count = 0;
    uart_flush(UART_NUM_0);
    ESP_LOGI(TAG,"UartReceiveTask Running");
    while(!STOPFlag)
    {
        CommandPack.Length = uart_read_bytes(UART_NUM_0,UartData[Count],sizeof(UartData[Count]),pdMS_TO_TICKS(CONFIG_USART_READ_BYTE_WAITTIME));

        if(CommandPack.Length > 0)
        {
            CommandPack.Data = UartData[Count];
            BaseType_t ret = xQueueSend(
                UartDataQueue,
                &CommandPack,
                portMAX_DELAY
            );
            if(ret == pdFALSE)
            {
                ESP_LOGE(TAG,"Uart Queue Send Error");
            }
            ESP_LOGI(TAG,"Uart Received %d Bytes",CommandPack.Length);
            TimeoutTick = xTaskGetTickCount();
            Count = Count == 7?0:Count+1;
        }
        else if(CommandPack.Length < 0)
        {
            ESP_LOGE(TAG,"Uart Receive Error");
            ErrStatus = UART_ERR;
            RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
            break;
        }
        else
        {

        }
    }
    ESP_LOGI(TAG,"UartDataQueue Deleted");
    vQueueDelete(UartDataQueue);
    UartDataQueue = NULL;
    ESP_LOGI(TAG,"UartReceiveTask Deleted");
    vTaskDelete(NULL);
}

static void ProgramerTask(void *arg)
{
    CommandPack_t CommandPack;
    uint32_t WritedSize = 0;

    vTaskDelay(10);
    ESP_LOGI(TAG,"Connect to Device");
    ErrStatus = BAUD_SYNC_ERR;
    if(!send_sync_cmd())
    {
        ESP_LOGE(TAG,"Connect failed");
        goto Err;
    }
    ESP_LOGI(TAG,"Detects the read-only status");
    ErrStatus = READOUT_PROTECT_ERR;
    switch(read_protect_check())
    {
    case -1:
    {
        ESP_LOGE(TAG,"Detection failed");
        goto Err;
        break;
    }
    case 0:
    {
        ESP_LOGI(TAG,"Read-only is enabled");
        ErrStatus = ERASE_ERR;
        if(!erase_flash())
        {
            ESP_LOGE(TAG,"Connect failed");
            goto Err;
        }
        break;
    }
    case 1:
    {
        ESP_LOGI(TAG,"Read-only is disable");
        ErrStatus = ERASE_ERR;
        if(!erase_flash())
        {
            ESP_LOGE(TAG,"Connect failed");
            goto Err;
        }
        break;
    }
    }

    ESP_LOGI(TAG,"Send Receive Sign");
    
    ulTaskNotifyTake(pdTRUE, portMAX_DELAY);
    
    uint32_t Address = Addr;
    ErrStatus = WRITE_MEMORY_ERR;
    while(!STOPFlag)
    {
        if(STOPFlag || xQueueReceive(
            WifiDataQueue,
            &CommandPack,
            0
            ) == pdFALSE
            )
        {
            if(STOPFlag == false && WritedSize < FileSize)
            {
                lwip_send(tcp_socket,"c",1,0);
                ulTaskNotifyTake(pdTRUE, portMAX_DELAY);
                continue;
            }
            else
            {
                break;  
            }
        }

        if(!write_flash(Address,CommandPack))
        {
            ESP_LOGE(TAG,"Download Address failed");
            goto Err;
        }
        if(CommandPack.Lock != NULL)
        {
            *CommandPack.Lock = false;
        }

        Address += CommandPack.Length;
        WritedSize += CommandPack.Length;
    }

    ESP_LOGI(TAG,"Device Restart");
    set_target_isp_mode(false);
    ErrStatus = NO_PROBLEM;
    RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
    ESP_LOGI(TAG,"WifiDataQueue Deleted");
    vQueueDelete(WifiDataQueue);
    WifiDataQueue = NULL;
    ESP_LOGI(TAG,"Programer Deleted");
    vTaskDelete(NULL);

    Err:
    RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
    ESP_LOGI(TAG,"WifiDataQueue Deleted");
    vQueueDelete(WifiDataQueue);
    WifiDataQueue = NULL;
    ESP_LOGI(TAG,"Programer Deleted");
    vTaskDelete(NULL);
}

