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

#include "module_command.h"

QueueHandle_t DataQueue;

static char* TAG = "CommandMenu";
Command_t* CommandMenu[CONFIG_MAX_COMMAND_NUM];
uint32_t CommandCount = 0;

static bool Lock = false;
static TickType_t CycleTime = CONFIG_ACTION_INTERVAL;
static uint64_t ActionHashCode = 0;
static List_t ActionList;

uint8_t ERR_FLAG = 0xFF;

void CommandAnalyse(void *args);
TaskHandle_t CommandAnalyseHandle;
void RunningActionTask(void* arg);
TaskHandle_t RunningActionHandle;
// static void UART0_Printf(const char *format, ...)
// {  
//     char buffer[256];  // 用于存储格式化后的字符串  
//     va_list args;
//     va_start(args, format);  
//     // 使用 vsnprintf 或类似函数格式化字符串  
//     vsnprintf(buffer, sizeof(buffer), format, args);  
//     // 将格式化后的字符串通过 UART 发送  
//     uart_write_bytes(UART_NUM_0,buffer,strlen(buffer));
//     va_end(args);  
// }

uint32_t DJB2GetHash(const uint8_t *data, size_t length)
{
    uint32_t hash = 5381;
    for (size_t i = 0; i < length; i++)
    {  
        hash = ((hash << 5) + hash) + data[i];
    }  
    return hash;  
}

bool CommandMenuInit(void)
{
    CommandAnalyseQueue = xQueueGenericCreate(
        CONFIG_CommandAnalyseQueueLength,
        sizeof(CommandPack_t),
        queueQUEUE_TYPE_BASE
    );
    if(CommandAnalyseQueue == NULL)
    {
        ESP_LOGE(TAG, "CommandAnalyseQueue Init Err");
        return false;
    }

    DataQueue = xQueueGenericCreate(
        CONFIG_DataQueueLength,
        sizeof(CommandPack_t),
        queueQUEUE_TYPE_BASE
    );
    if(DataQueue == NULL)
    {
        ESP_LOGE(TAG, "DataQueue Init Err");
        return false;
    }

    vListInitialise(&ActionList);

    if(xTaskCreate(
        CommandAnalyse,
        CONFIG_CommandAnalyse_NAME, 
        CONFIG_CommandAnalyse_STACK_DEPTH, 
        NULL,
        CONFIG_CommandAnalyse_PRIORITY, 
        &CommandAnalyseHandle
    ) == pdFALSE)
    {
        ESP_LOGE(TAG, "CommandAnalyse Task Init Err");
        return false;
    }

    if(xTaskCreate(
        RunningActionTask,
        CONFIG_RunningActionTask_NAME, 
        CONFIG_RunningActionTask_STACK_DEPTH, 
        NULL,
        CONFIG_RunningActionTask_PRIORITY, 
        &RunningActionHandle
    ) == pdFALSE)
    {
        ESP_LOGE(TAG, "RunningActionTask Task Init Err");
        return false;
    }
    ESP_LOGI(TAG, "CommandMenuInit Success");
    return true;
}

void NetworkLoadRegulation(void)
{
    static UBaseType_t C_A_PriorityLevel = CONFIG_CommandAnalyse_PRIORITY;
    static UBaseType_t R_A_PriorityLevel = CONFIG_RunningActionTask_PRIORITY;
    static uint8_t times = 0;
    static TickType_t last_adjust_time = 0;

    if(xTaskGetTickCount() - last_adjust_time < pdMS_TO_TICKS(100))
    {
        return;
    }

    if(times >= 3)
    {
        UBaseType_t C_A_Q_SpaceAvail = uxQueueSpacesAvailable(CommandAnalyseQueue);
        UBaseType_t D_Q_SpaceAvail = uxQueueSpacesAvailable(DataQueue);
        times = 0;
        if(((float)C_A_Q_SpaceAvail/CONFIG_CommandAnalyseQueueLength) >= 0.75)
        {
            if(uxTaskPriorityGet(CommandAnalyseHandle) != C_A_PriorityLevel)
            {
                C_A_PriorityLevel = CONFIG_CommandAnalyse_PRIORITY;
                vTaskPrioritySet(CommandAnalyseHandle,CONFIG_CommandAnalyse_PRIORITY);
            }
        }
        else if(((float)C_A_Q_SpaceAvail/CONFIG_CommandAnalyseQueueLength) >= 0.5)
        {
            if(uxTaskPriorityGet(CommandAnalyseHandle) != C_A_PriorityLevel)
            {
                C_A_PriorityLevel = 5;
                vTaskPrioritySet(CommandAnalyseHandle,C_A_PriorityLevel);
            }
        }
        else
        {
            if(uxTaskPriorityGet(CommandAnalyseHandle) != C_A_PriorityLevel)
            {
                C_A_PriorityLevel = 6;
                vTaskPrioritySet(CommandAnalyseHandle,C_A_PriorityLevel);
            }
        }

        if(((float)D_Q_SpaceAvail/CONFIG_CommandAnalyseQueueLength) >= 0.75)
        {
            if(uxTaskPriorityGet(RunningActionHandle) != R_A_PriorityLevel)
            {
                R_A_PriorityLevel = CONFIG_RunningActionTask_PRIORITY;
                vTaskPrioritySet(RunningActionHandle,CONFIG_RunningActionTask_PRIORITY);
            }
        }
        else if(((float)D_Q_SpaceAvail/CONFIG_CommandAnalyseQueueLength) >= 0.5)
        {
            if(uxTaskPriorityGet(RunningActionHandle) != R_A_PriorityLevel)
            {
                R_A_PriorityLevel = 4;
                vTaskPrioritySet(RunningActionHandle,R_A_PriorityLevel);
            }
        }
        else
        {
            if(uxTaskPriorityGet(RunningActionHandle) != R_A_PriorityLevel)
            {
                R_A_PriorityLevel = 5;
                vTaskPrioritySet(RunningActionHandle,R_A_PriorityLevel);
            }
        }
        last_adjust_time = xTaskGetTickCount();
    }
    else
    {
        times ++;
    }
}

BaseType_t SendToCommandAnalyse(CommandPack_t CommandPack,TickType_t xTicksToWait)
{
    BaseType_t ret = pdTRUE;
    if(CommandPack.Length < CONFIG_MAX_COMMAND_NUM)
    {
        ret = xQueueSend(
            CommandAnalyseQueue,
            &CommandPack,
            xTicksToWait
        );
    }
    else if(ActionList.uxNumberOfItems)
    {
        ret = xQueueSend(
            DataQueue,
            &CommandPack,
            xTicksToWait
        );
    }

    NetworkLoadRegulation();
    return ret;
}

BaseType_t SendToDataQueue(CommandPack_t CommandPack,TickType_t xTicksToWait)
{
    BaseType_t ret = xQueueSend(
        DataQueue,
        &CommandPack,
        xTicksToWait
    );
    NetworkLoadRegulation();
    return ret;
}

bool CommandMenuAdd(Command_t* Command)
{
    if(Command == NULL)
    {
        ESP_LOGE(TAG,"Command point to NULL when add command to menu!");
        return false;
    }

    if(CommandCount <= CONFIG_MAX_COMMAND_NUM)
    {
        if(Command->InitAction != NULL && Command->CommandName != NULL)
        {
            Command->Status = 0;
            if(Command->HashCode == 0)
            {    
                Command->HashCode = DJB2GetHash((const uint8_t *)Command->CommandName,strlen(Command->CommandName));
            }
            CommandMenu[CommandCount] = Command;
            CommandCount++;
            ESP_LOGI(TAG, "Name:%s,Hash:%d",Command->CommandName,(int)Command->HashCode);
            return true;
        }
        else
        {
            ESP_LOGE(TAG,"%s config is not vaild",Command->CommandName);
        }
    }
    else
    {
        ESP_LOGE(TAG,"Not have enough space to add command");
    }
    return false;
}

void CommandAnalyse(void *args)
{
    CommandPack_t QueueData;
    while(true)
    {
        BaseType_t ret = xQueueReceive(
            CommandAnalyseQueue,
            &QueueData,
            portMAX_DELAY
        );
        
        if(ret == pdFALSE)
        {
            ESP_LOGE(TAG,"CommandAnalyseQueue Queue receive err");
            continue;
        }
        
        ESP_LOGI(TAG,"CommandAnalyseQueue Queue received %d bytes",QueueData.Length);
        
        heart_beat_reset();

        uint64_t HashCode = DJB2GetHash(QueueData.Data,QueueData.Length);

        for(uint32_t Index = 0;Index < CommandCount; Index++)
        {
            if(HashCode != CommandMenu[Index] -> HashCode)
            {
                continue;
            }

            ESP_LOGI(TAG,"%s hash is equal",CommandMenu[Index]->CommandName);

            if(Lock == false)//非连续的控制代码可以在Lock时执行
            {
                if(CommandMenu[Index] -> InitAction != NULL && CommandMenu[Index] -> RunningStatus == false)
                {
                    ESP_LOGI(TAG,"%s Run",CommandMenu[Index]->CommandName);
                    if((uint8_t *)(CommandMenu[Index] -> InitAction(QueueData.Data)) == &ERR_FLAG)
                    {
                        goto END;
                    }
                }

                if(CommandMenu[Index] -> Running == true && CommandMenu[Index] -> RunningStatus == false)
                {
                    if(RunningActionAdd(CommandMenu[Index]) == false)
                    {
                        ESP_LOGE(TAG,"%s Running failed",CommandMenu[Index] -> CommandName);
                    }
                }
            }
            else if(HashCode == ActionHashCode)
            {
                ESP_LOGI(TAG,"%s Exti running",CommandMenu[Index] -> CommandName);
                RunningActionDelete(CommandMenu[Index],QueueData.Data);
            }
            goto END;
        }

        if(ActionList.uxNumberOfItems)
        {
            xQueueSend(
                DataQueue,
                &QueueData,
                portMAX_DELAY
            );  
        }

        END:
        ESP_LOGI(TAG,"CommandAnalyse:%d",uxTaskGetStackHighWaterMark(NULL));
        continue;
    }
}

void RunningActionTask(void* arg)
{
    Command_t* CommandPtr = NULL;
    CommandPack_t QueueData;
    void* Data = NULL;
    while(true)
    {
        if(xQueueReceive(
            DataQueue,
            &QueueData,
            pdMS_TO_TICKS(CycleTime)
        ) == pdFALSE)
        {
            Data = NULL;
        }
        else
        {
            ESP_LOGI(TAG,"DataQueue Queue Received");
            Data = &QueueData;
        }

        ListItem_t *pxIterator = listGET_NEXT(&ActionList.xListEnd);

        uint32_t index = ActionList.uxNumberOfItems;

        while(index--)
        {
            if(listIS_CONTAINED_WITHIN(&ActionList,pxIterator))
            {
                CommandPtr = (Command_t *)pxIterator->pvOwner;
                if(CommandPtr->Running == true)
                {
                    CommandPtr->Action(Data);
                }
                        
                if(pxIterator == (ListItem_t *)&ActionList.xListEnd)
                {
                    break;
                }
                pxIterator = listGET_NEXT(pxIterator);
            }
            else
            {
                pxIterator = listGET_NEXT(&ActionList.xListEnd);
            }
        }
    }
}

bool RunningActionAdd(Command_t* Command)
{
    if(Command == NULL)
    {
        ESP_LOGE(TAG,"Command point to NULL when add running action!");
        return false;
    }

    if(Command->Action == NULL)
    {
        Command->RunningStatus = false;
        ESP_LOGE(TAG,"%s Action is not valid function",Command->CommandName); 
        return false;
    }

    if(Lock == true && Command->HashCode != ActionHashCode)
    {
        Command->RunningStatus = false;
        ESP_LOGE(TAG,"%s can not add in list when lock is true",Command->CommandName); 
        return false;
    }

    ListItem_t *pItem = pvPortMalloc(sizeof(ListItem_t));  
    if (pItem != NULL)
    {  
        vListInitialiseItem(pItem);  
        pItem->pvOwner = Command;  
        vListInsertEnd(&ActionList, pItem);  
        Command->RunningStatus = true;
        ESP_LOGI(TAG,"%s Add in Running Task",Command->CommandName);
        return true;
    }
    else
    {
        ESP_LOGE(TAG,"%s can not apply free space",Command->CommandName); 
        return false;
    }
}

bool RunningActionDelete(Command_t* Command,void* ExtiArgs)
{
    if(Command == NULL)
    {
        ESP_LOGE(TAG,"Command point to NULL when delete action!");
        return false;
    }

    if (ActionList.uxNumberOfItems == 0 || Command->Running == false || Command->RunningStatus == false)
    {
        ESP_LOGE(TAG, "No actions to delete,ActionList:%d,Running:%d,RunningStatus:%d",
            ActionList.uxNumberOfItems,Command->Running,Command->RunningStatus);  
        return false;  
    }  
    ListItem_t *pxIterator;

    pxIterator = listGET_NEXT(&ActionList.xListEnd);
    for(int index = ActionList.uxNumberOfItems;index > 0; index--) 
    {
        Command_t *ItemPtr = (Command_t *)pxIterator->pvOwner;

        if(Command->HashCode == ItemPtr->HashCode)
        {  
            ItemPtr->RunningStatus = false;
            if(ItemPtr->ExtiAction != NULL)
            {
                ItemPtr->ExtiAction(ExtiArgs);
                ESP_LOGI(TAG, "%s EXTI Run",Command->CommandName); 
            }
            uxListRemove(pxIterator);
            ESP_LOGI(TAG, "%s has been delete",Command->CommandName);
            return true;
        }  
    }
    ESP_LOGE(TAG, "Not found item to delete");  
    return false;
}

BaseType_t RunningActionDeleteSelf(Command_t* Command,int16_t Length)
{
    if(Command->RunningStatus == true)
    {
        CommandPack_t CommandPack = 
        {
            .Data = (uint8_t *)Command->CommandName,
            .Length = strnlen(Command->CommandName,Length)
        };
        return SendToCommandAnalyse(CommandPack,portMAX_DELAY);
    }
    return pdTRUE;
}

bool SetActionAddLock(Command_t* Command, bool Status, TickType_t TimeMS)
{
    if(Command == NULL)
    {
        ESP_LOGE(TAG,"Command point to NULL when set action lock!");
        return false;
    }

    if(ActionHashCode == Command->HashCode || ActionHashCode == 0)
    {
        CycleTime = (Status == true ? TimeMS : (CONFIG_ACTION_INTERVAL * (ActionList.uxNumberOfItems + 1) ) );
        Lock = Status;
        ActionHashCode = (Status == true ? Command->HashCode : 0);
        ESP_LOGI(TAG,"%s set lock: %d,ActionHashCode:%llu",Command->CommandName,(int)Lock,ActionHashCode);
        return true;
    }
    else
    {
        ESP_LOGE(TAG,"%s set lock: %d ,Failed",Command->CommandName,(int)Status);
        return false;
    }
}





