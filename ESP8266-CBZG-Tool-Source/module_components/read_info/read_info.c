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
#include "read_info.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "ESPREADINFO";
static char NAME[] = CONFIG_ESP_READ_INFO_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_READ_INFO_BRIEF;
static char VERSION[] = CONFIG_ESP_READ_INFO_VERSION;
static char CMD_NAME[] = CONFIG_ESP_READ_INFO_CMD_NAME;
#ifndef CONFIG_ESP_READ_INFO_RUNNING
#define CONFIG_ESP_READ_INFO_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_READ_INFO_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};

uart_config_t uart_read_config(uart_port_t uart_num)
{
    uart_config_t config = {
        .flow_ctrl = UART_HW_FLOWCTRL_DISABLE,
        .rx_flow_ctrl_thresh = 0
    };

    if(uart_get_baudrate(uart_num,(uint32_t *)&config.baud_rate) == ESP_ERR_INVALID_ARG)
    {
        config.baud_rate = CONFIG_UART0_BAUD_RATE;
    }

    // 禁用此方法方便用户观察波特率误差
    // if(abs(globe_user_config.uart_config.baud_rate - config.baud_rate) * 1000 / globe_user_config.uart_config.baud_rate < 10)
    // {
    //     config.baud_rate = globe_user_config.uart_config.baud_rate;
    // }

    if(uart_get_word_length(uart_num,&config.data_bits) == ESP_ERR_INVALID_ARG)
    {
        config.data_bits = CONFIG_UART0_DATA_BITS;
    }

    if(uart_get_stop_bits(uart_num,&config.stop_bits) == ESP_ERR_INVALID_ARG)
    {
        config.stop_bits = CONFIG_UART0_STOP_BITS;
    }

    if(uart_get_parity(uart_num,&config.parity) == ESP_ERR_INVALID_ARG)
    {
        config.parity = CONFIG_UART0_PARITY;
    }
    return config;
}

user_wifi_config wifi_read_config(void)
{
    return globe_user_config.wifi_config;
}
 
system_config_t system_read_config(void)
{
    return globe_user_config.system_config;
}

user_config_t create_userconfig_list(uart_config_t uart_config ,
                                    user_wifi_config wifi_config ,
                                    system_config_t system_config
                                )
{
    user_config_t config = 
    {
        .uart_config = uart_config,
        .wifi_config = wifi_config,
        .system_config = system_config
    };
    return config;
}

cJSON* config_to_json(const user_config_t* config)
{
    cJSON* root = cJSON_CreateObject();

    if (root == NULL)
    {
        ESP_LOGE(TAG, "Failed to create JSON array");
        return NULL;
    }

    cJSON* uart_object = cJSON_CreateObject();
    if (uart_object != NULL)
    {
        cJSON_AddItemToObject(root,"uart_config",uart_object);
        cJSON_AddNumberToObject(uart_object,"BaudRate",config->uart_config.baud_rate);
        cJSON_AddNumberToObject(uart_object,"DataBits",config->uart_config.data_bits);
        cJSON_AddNumberToObject(uart_object,"StopBits",config->uart_config.stop_bits);
        cJSON_AddNumberToObject(uart_object,"Parity",config->uart_config.parity);
        cJSON_AddNumberToObject(uart_object,"FlowCtrl",config->uart_config.flow_ctrl);
        cJSON_AddNumberToObject(uart_object,"FlowCtrlThresh",config->uart_config.rx_flow_ctrl_thresh);
    }
    else
    {
        ESP_LOGE(TAG, "Failed to create UART JSON object");
    }

    cJSON* wifi_object = cJSON_CreateObject();
    if (wifi_object != NULL)
    {
        cJSON_AddItemToObject(root,"wifi_config",wifi_object);
        cJSON_AddStringToObject(wifi_object,"SSID",config->wifi_config.ssid ? config->wifi_config.ssid : CONFIG_WIFI_SSID);
        cJSON_AddStringToObject(wifi_object,"PassWord",config->wifi_config.password ? config->wifi_config.password : CONFIG_WIFI_PASSWORD);
        cJSON_AddNumberToObject(wifi_object,"Channel",config->wifi_config.channel);
        cJSON_AddStringToObject(wifi_object,"IP",config->wifi_config.ip ? config->wifi_config.ip : CONFIG_IPV4_ADDR);
        cJSON_AddNumberToObject(wifi_object,"TCPPort",config->wifi_config.port);
        cJSON_AddNumberToObject(wifi_object,"UDPPort",config->wifi_config.udp_port);
    }
    else
    {
        ESP_LOGE(TAG, "Failed to create Wifi JSON object");
    }

    cJSON* system_object = cJSON_CreateObject();
    if (system_object != NULL)
    {
        cJSON_AddItemToObject(root,"system_config",system_object);
        cJSON_AddStringToObject(system_object,"Author",config->system_config.Author);
    }
    else
    {
        ESP_LOGE(TAG, "Failed to create System JSON object");
    }

    return root;
}

static void* InitAction(void *arg)
{
    uart_config_t  uart_config = uart_read_config(UART_NUM_0);
    user_wifi_config wifi_config = wifi_read_config();
    system_config_t  system_config = system_read_config();
    user_config_t config = create_userconfig_list(uart_config,wifi_config,system_config);
    cJSON* json = config_to_json(&config);

    if (json == NULL)
    {
        ESP_LOGE(TAG, "config_to_json() returned NULL");
        return NULL;
    }

    char* json_str = cJSON_PrintUnformatted(json);
    if(json_str == NULL)
    {
        ESP_LOGE(TAG, "Failed to create JSON object");  
        cJSON_Delete(json);
        return NULL;
    }
    else
    {
        lwip_send(tcp_socket, json_str, strlen(json_str), 0);
    }

    cJSON_Delete(json);
    free(json_str);
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

void menu_esp_read_info(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}


