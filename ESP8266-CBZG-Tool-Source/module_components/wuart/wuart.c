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
#include "wuart.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "ESPWUART";
static char NAME[] = CONFIG_ESP_WUART_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_WUART_BRIEF;
static char VERSION[] = CONFIG_ESP_WUART_VERSION;
static char CMD_NAME[] = CONFIG_ESP_WUART_CMD_NAME;
#ifndef CONFIG_ESP_WUART_RUNNING
#define CONFIG_ESP_WUART_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_WUART_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};

extern int tcp_socket;
extern gpio_num_t BOOT_PIN;
extern gpio_num_t RST_PIN;
TaskHandle_t UARTReceiveHandle;
static uint8_t Buffer[CONFIG_UART_READ_CHAR_MAX_LENGTH];
static uint32_t WriteNumber = 0;

static void UARTReceiveTask(void *arg)
{
    int Number = 0;
    TickType_t FrontSendTime = 0;
    TickType_t CurrentTime = 0;
    WriteNumber = 0;
    ESP_LOGI(TAG,"UARTReceiveTask Create");
    vTaskDelay(100);
    uart_flush(UART_NUM_0);
    while(Command.RunningStatus)
    {

        Number = uart_read_bytes(
            UART_NUM_0,
            &Buffer[WriteNumber],
            CONFIG_UART_READ_CHAR_MAX_LENGTH - WriteNumber,
            pdMS_TO_TICKS(CONFIG_UART_READ_TIMEOUT)
        );

        if(Number > 0 || WriteNumber > 0)
        {
            CurrentTime = xTaskGetTickCount();
            WriteNumber += Number;

            if(WriteNumber == CONFIG_UART_READ_CHAR_MAX_LENGTH)
            {
                lwip_send(tcp_socket,Buffer,WriteNumber,0);
                ESP_LOGI(TAG,"Send %d Bytes to server",WriteNumber);
                WriteNumber= 0;
            }
            else if(CurrentTime - FrontSendTime > CONFIG_UART_SEND_TIMEOUT)
            {
                lwip_send(tcp_socket,Buffer,WriteNumber,0);
                ESP_LOGI(TAG,"Send %d Bytes to server",WriteNumber);
                WriteNumber= 0;
            }
            else
            {
                FrontSendTime = CurrentTime;
            }
        }
    }
    ESP_LOGI(TAG,"UARTReceiveTask Delete");
    vTaskDelete(NULL);
}

void SetTCPNoDelay(int sock, bool enable)
{
    int flag = enable ? 1 : 0;
    if (setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, &flag, sizeof(flag)) < 0) 
    {
        ESP_LOGE(TAG,"Failed to set TCP_NODELAY to %d\n", flag);
    }
}

static void* InitAction(void *arg)
{
    if(!SetActionAddLock(&Command,true,portMAX_DELAY))
    {
        ESP_LOGE(TAG,"Locked Err");
        goto ERR;
    }

    if(xTaskCreate(
        UARTReceiveTask,
        Command.CommandName,
        1596,
        NULL,
        5,
        &UARTReceiveHandle
    ) == pdFALSE
    )
    {
        ESP_LOGE(TAG,"UartReceive Task Start Err");
        goto ERR;
    }

    heart_beat_enable(false);

    lwip_send(tcp_socket,"\171",1,0);
    
    ESP_LOGI(TAG,"Start success");
    return NULL;
ERR:
    lwip_send(tcp_socket,"\037",1,0);
    RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
    return NULL;
}

static void* Action(void *arg)
{
    if (arg == NULL)
    {
        return NULL;
    }

    CommandPack_t *CommandPack = (CommandPack_t *)arg;

    if (CommandPack->Data == NULL || CommandPack->Length <= 0)
    {
        return NULL;
    }

    if(CommandPack->Length == 12)
    {
        if(strncmp((char *)CommandPack->Data,"GPIOADJUST",10)==0)
        {
            if(CommandPack->Data[10]=='0')
            {
                SetGPIOLow(BOOT_PIN);
            }
            else
            {
                SetGPIOHigh(BOOT_PIN);
            }

            if(CommandPack->Data[11]=='0')
            {
                SetGPIOLow(RST_PIN);
            }
            else
            {
                SetGPIOHigh(RST_PIN);
            }
        }
    }

    uart_write_bytes(UART_NUM_0, (const char *)CommandPack->Data, CommandPack->Length);
    uart_wait_tx_done(UART_NUM_0,portMAX_DELAY);
    return NULL;
}

static void* EXTIAction(void *arg)
{
    heart_beat_enable(true);
    SetTCPNoDelay(tcp_socket,false);
    SetActionAddLock(&Command,false,0);
    return NULL;
}

void menu_esp_wuart(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}


