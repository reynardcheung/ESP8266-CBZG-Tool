#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "freertos/queue.h"

#include <stdint.h>
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>
#include "sdkconfig.h"
#include "driver/uart.h"

#include "user_heart_beat.h"

extern uint8_t ERR_FLAG;

typedef struct
{
    uint8_t *Data;
    int32_t Length;
    bool *Lock;                                 //告知接收任务不可覆盖此项
}CommandPack_t;

typedef struct
{
    char* Name;                                 //用户控制：显示给用户的名称
    char* Brief;                                //用户控制：显示给用户的简介
    char* ModuleVersion;                        //用户控制：显示给用户的模块版本
    char* CommandName;                          //用户控制：命令名称，调用该库的命令
    uint32_t HashCode;                          //系统控制：自动计算CommandName的Hash(DJB2)
    bool Running;                               //用户控制：是否需要循环运行
    bool RunningStatus;                         //系统控制：记录是否运行
    uint8_t Status;                             //用户控制(选填)：默认为0，用于记录信息，例子：用户可以在ExtiAction读取，判断Init是否完成
    void* (*InitAction)(void *);                //用户控制：Analyse任务分析数据Hash与该命令Hash相等会执行入口函数
    void* (*Action)(void *);                    //用户控制(选填)：执行完Init后由Running的状态控制是否定期事件循环
    void* (*ExtiAction)(void *);                //用户控制(选填)：Action被Delete后会执行出口函数
}Command_t;

extern int tcp_socket;

QueueHandle_t CommandAnalyseQueue;

extern Command_t* CommandMenu[CONFIG_MAX_COMMAND_NUM];
extern uint32_t CommandCount;

extern uint32_t DJB2GetHash(const uint8_t *data, size_t length);
extern bool CommandMenuInit(void);
extern BaseType_t SendToCommandAnalyse(CommandPack_t CommandPack,TickType_t xTicksToWait);
extern BaseType_t SendToDataQueue(CommandPack_t CommandPack,TickType_t xTicksToWait);
extern bool CommandMenuAdd(Command_t* Command);
extern bool RunningActionAdd(Command_t* Command);
extern bool RunningActionDelete(Command_t* Command,void* ExtiArgs);
extern BaseType_t RunningActionDeleteSelf(Command_t* Command,int16_t Length);
extern bool SetActionAddLock(Command_t* Command, bool Status, TickType_t CycleTime);

#ifdef __cplusplus
}
#endif
