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
#include "get_id.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "ESPGETID";
static char NAME[] = CONFIG_ESP_GET_ID_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_GET_ID_BRIEF;
static char VERSION[] = CONFIG_ESP_GET_ID_VERSION;
static char CMD_NAME[] = CONFIG_ESP_GET_ID_CMD_NAME;
static uint8_t DeviceID[27] = {0};
#ifndef CONFIG_ESP_GET_ID_RUNNING
#define CONFIG_ESP_GET_ID_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_GET_ID_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};

void IDCreate()
{
    char mac_str[18];
    esp_wifi_get_mac(ESP_IF_WIFI_STA,(uint8_t *)mac_str);
    snprintf(mac_str, sizeof(mac_str),MACSTR,MAC2STR(mac_str));
    sprintf((char *)DeviceID,"%s@%s",mac_str,__TIME__);
}

static void* InitAction(void *arg)
{
    lwip_send(tcp_socket,DeviceID,sizeof(DeviceID),0);
    ESP_LOGI(TAG,"DeviceID:%s",DeviceID);
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


void menu_esp_get_id(void)
{
    if(CommandMenuAdd(&Command))
    {
        IDCreate();
    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}


