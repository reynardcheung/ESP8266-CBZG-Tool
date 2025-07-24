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
#include "SSD1306.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "SSD1306";
static char NAME[] = CONFIG_SSD1306_MODULE_NAME;
static char BRIEF[] = CONFIG_SSD1306_BRIEF;
static char VERSION[] = CONFIG_SSD1306_VERSION;
static char CMD_NAME[] = CONFIG_SSD1306_CMD_NAME;
#ifndef CONFIG_SSD1306_RUNNING
#define CONFIG_SSD1306_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_SSD1306_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};

/*
Recv JSON:
[
	{
		"CMD": 1,
		"Addr": 60,
        "Size":0,
		"Reg": 123,
		"Value": 1
	},
	{
		"CMD": 1,
		"Addr": 60,
        "Size":0,
		"Reg": 123,
		"Value": 1
	}
]

Send JSON:
[
	{
		"CMD": 1,
		"Addr": 60,
        "Size":0,
		"Reg": 123,
		"Value": 1
        "Status":0
	},
	{
		"CMD": 1,
		"Addr": 60,
        "Size":0,
		"Reg": 123,
        "Length":1024,
		"Value": 1
        "Status":1
	}
]
*/

/*
{WaitCommandMode}
{ScreenSyncMode}
{SerialSyncMode}
*/

static void CommandRecv(CommandPack_t* Pack);
static void ScreenSync(CommandPack_t* Pack);
static void SerialReadTask(void *arg);

typedef enum 
{
    WaitCommandMode = 0,
    _ScreenSyncMode = 1,
    _SerialSyncMode = 2,
}SystemStatus;

uint32_t Hash_WaitCommandMode;
uint32_t Hash_ScreenSyncMode;
uint32_t Hash_SerialSyncMode;

SystemStatus CurrentStatus;
TaskHandle_t SerialReadTaskHandle;

static void* InitAction(void *arg)
{
    SetActionAddLock(&Command,true,portMAX_DELAY);
    Command.Status = true;
    
    OLED_Init();
    
    Hash_WaitCommandMode = DJB2GetHash((uint8_t *)"{WaitCommandMode}",17);
    Hash_ScreenSyncMode = DJB2GetHash((uint8_t *)"{_ScreenSyncMode}",17);
    Hash_SerialSyncMode = DJB2GetHash((uint8_t *)"{_SerialSyncMode}",17);

    if(xTaskCreate(
        SerialReadTask,
        Command.CommandName,
        1596,
        NULL,
        5,
        &SerialReadTaskHandle
    ) == pdFALSE
    )
    {
        ESP_LOGE(TAG,"SerialReadTask Start Err");
    }

    CurrentStatus = WaitCommandMode;
    OLED_Clear();
    OLED_Update();
    lwip_send(tcp_socket,"y",1,0);
    return NULL;
}

static void* Action(void *arg)
{
    if(arg == NULL)
    {
        return NULL;
    }

    CommandPack_t *CommandPack = (CommandPack_t *)arg;
    *CommandPack->Lock = true;
    
    switch (CurrentStatus)
    {
    case WaitCommandMode:
    {
        CommandRecv(CommandPack);
        break;
    }
    case _ScreenSyncMode:
    {
        ScreenSync(CommandPack);
        break;
    }
    case _SerialSyncMode:
    {
        CommandPack->Lock = false;
        break;
    }
    default:
    {
        lwip_send(tcp_socket,"n",1,0);
        break;
    }
    }

    return NULL;
}

static void* EXTIAction(void *arg)
{
    SetActionAddLock(&Command,false,portMAX_DELAY);
    OLED_Uninstall();
    user_gpio_init();
    Command.Status = false;
    SerialReadTaskHandle = NULL;
    lwip_send(tcp_socket,"y",1,0);
    return NULL;
}

void menu_ssd1306(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}

static void CommandRecv(CommandPack_t* Pack)
{
    if(Pack->Length == 17)
    {
        uint32_t CMDHash = DJB2GetHash(Pack->Data,17);
        if(CMDHash == Hash_WaitCommandMode)
        {
            CurrentStatus = WaitCommandMode;
            lwip_send(tcp_socket,"y",1,0);
        }
        else if(CMDHash == Hash_ScreenSyncMode)
        {
            CurrentStatus = _ScreenSyncMode;
            lwip_send(tcp_socket,"y",1,0);
        }
        else if(CMDHash == Hash_SerialSyncMode)
        {
            CurrentStatus = _SerialSyncMode;
            xTaskNotifyGive(SerialReadTaskHandle);
            lwip_send(tcp_socket,"y",1,0);
        }
        else
        {
            lwip_send(tcp_socket,"n",1,0);
        }
    }
    *Pack->Lock =false;
}

static void ScreenSync(CommandPack_t* Pack)
{
    if(Pack->Length == 1024)
    {
        OLED_UploadScreen(Pack->Data);
    }
    else if(Pack->Length == 17)
    {
        CommandRecv(Pack);
    }
    else
    {

    }
    *Pack->Lock = false;
}

#define OLED_DISPLAY_LINES 9  // 屏幕可显示的行数
#define LINE_CHAR_LENGTH 21    // 每行最多21字符（128/6≈21.3，留1像素边界）
#define BUFFER_SIZE (OLED_DISPLAY_LINES + 1) // 环形缓冲区大小（多1行用于滚动过渡）
static void SerialReadTask(void *arg) {
    // 环形缓冲区：存储BUFFER_SIZE行，每行LINE_CHAR_LENGTH+1（含结束符）
    uint8_t SerialData[BUFFER_SIZE][LINE_CHAR_LENGTH + 1] = {0};
    uint8_t startLine = 0;  // 环形缓冲区起始索引
    uint8_t totalLines = 0; // 当前有效数据行数
    uint8_t DataLength = 0;

    while (true)
    {
        if (Command.Status == false) break;

        if (CurrentStatus != _SerialSyncMode)
        {
            ulTaskNotifyTake(pdTRUE, portMAX_DELAY);
        }

        // 读取串口数据到缓冲区当前行
        uint8_t currentLine = (startLine + totalLines) % BUFFER_SIZE;
        DataLength = uart_read_bytes(UART_NUM_0,
                        SerialData[currentLine],
                        LINE_CHAR_LENGTH,  // 读取最大长度
                        pdMS_TO_TICKS(100));
        
        // 添加结束符并处理非ASCII字符
        SerialData[currentLine][DataLength] = '\0';
        for (int i = 0; i < DataLength; i++)
        {
            if (SerialData[currentLine][i] < 32 || SerialData[currentLine][i] > 126)
            {
                SerialData[currentLine][i] = '.'; // 替换非打印字符
            }
        }

        // 更新行计数
        if (totalLines < BUFFER_SIZE)
        {
            totalLines++;
        }
        else
        {
            // 缓冲区满：淘汰最旧行
            startLine = (startLine + 1) % BUFFER_SIZE;
        }

        // 清屏（仅清文本区域）
        OLED_Clear();

        // 显示所有有效行（从startLine开始取OLED_DISPLAY_LINES行）
        for (int i = 0; i < OLED_DISPLAY_LINES; i++)
        {
            if (i < totalLines)
            {
                uint8_t lineIndex = (startLine + i) % BUFFER_SIZE;
                // 每行从y=6*i的位置开始打印
                OLED_Printf(0, 7 * i, "%s", SerialData[lineIndex]);
            }
        }

        OLED_Update(); // 更新到屏幕
    }
    vTaskDelete(NULL);
}
