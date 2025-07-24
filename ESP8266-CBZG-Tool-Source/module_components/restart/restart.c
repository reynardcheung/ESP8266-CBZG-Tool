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
#include "restart.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "ESPRESTART";
static char NAME[] = CONFIG_ESP_RESTART_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_RESTART_BRIEF;
static char VERSION[] = CONFIG_ESP_RESTART_VERSION;
static char CMD_NAME[] = CONFIG_ESP_RESTART_CMD_NAME;
#ifndef CONFIG_ESP_RESTART_RUNNING
#define CONFIG_ESP_RESTART_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_RESTART_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};

static void* InitAction(void *arg)
{
    ESP_LOGI(TAG,"ESP Restart");
    esp_restart();
    return NULL;
}


// static void* Action(void *arg)
// {
//     return NULL;
// }

// static void* EXTIAction(void *arg)
// {
//     return NULL;
// }


void menu_esp_restart(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}


