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
#include "get_version.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "ESPGETVERSION";
static char NAME[] = CONFIG_ESP_GET_VERSION_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_GET_VERSION_BRIEF;
static char VERSION[] = CONFIG_ESP_GET_VERSION_VERSION;
static char CMD_NAME[] = CONFIG_ESP_GET_VERSION_CMD_NAME;
#ifndef CONFIG_ESP_GET_VERSION_RUNNING
#define CONFIG_ESP_GET_VERSION_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_GET_VERSION_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};

// static uint8_t DeviceID[27] = {0};

static void* InitAction(void *arg)
{
    lwip_send(tcp_socket,CONFIG_ESP_DEVICE_VERSION,sizeof(CONFIG_ESP_DEVICE_VERSION),0);
    ESP_LOGI(TAG,"Ver:%s",CONFIG_ESP_DEVICE_VERSION);
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


void menu_esp_get_version(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}


