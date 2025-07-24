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
#include "set_info.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "ESPSETTING";
static char NAME[] = CONFIG_ESP_SETTING_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_SETTING_BRIEF;
static char VERSION[] = CONFIG_ESP_SETTING_VERSION;
static char CMD_NAME[] = CONFIG_ESP_SETTING_CMD_NAME;
#ifndef CONFIG_ESP_SETTING_RUNNING
#define CONFIG_ESP_SETTING_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_SETTING_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};

uart_config_t JsonToUartConfig(cJSON* UartConfig);
user_wifi_config JsonToWifiConfig(cJSON* WifiConfig);
system_config_t JsonToSystemConfig(cJSON* SystemConfig);
user_config_t CombineAllConfig(uart_config_t UartConfig, 
                            user_wifi_config WifiConfig,
                            system_config_t SystemConfig);

static void* InitAction(void *arg)
{
    Command.Status = true;
    SetActionAddLock(&Command,true,10);
    send(tcp_socket,"\171",1,0);
    return NULL;
}

static void* Action(void *arg)
{
    if(Command.Status == false)
    {
        return NULL;
    }

    if(arg == NULL)
    {
        return NULL;
    }

    CommandPack_t *CommandPack = (CommandPack_t *)arg;

    cJSON* Root = cJSON_Parse((char *)CommandPack->Data);
    if(Root == NULL)
    {
        ESP_LOGE(TAG,"Error Json");
        send(tcp_socket,"\037",1,0);
        goto EXTI;
    }

    cJSON* UartConfigJson = cJSON_GetObjectItem(Root,"uart_config");
    cJSON* WifiConfigJson = cJSON_GetObjectItem(Root,"wifi_config");
    cJSON* SystemConfigJson = cJSON_GetObjectItem(Root,"system_config");
    uart_config_t UartConfig = JsonToUartConfig(UartConfigJson);
    user_wifi_config WifiConfig = JsonToWifiConfig(WifiConfigJson);
    system_config_t SystemConfig = JsonToSystemConfig(SystemConfigJson);
    user_config_t UserConfig = CombineAllConfig(UartConfig,WifiConfig,SystemConfig);
    bool Result = save_config(&UserConfig);
    if(Result)
    {
        ESP_LOGE(TAG,"Setting OK");
        send(tcp_socket,"\171",1,0);
    }
    else
    {
        ESP_LOGE(TAG,"Setting Error");
        send(tcp_socket,"\037",1,0);
    }
EXTI:
    ESP_LOGI(TAG,"Settings Module End");
    RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
    return NULL;
}

static void* EXTIAction(void *arg)
{
    SetActionAddLock(&Command,false,10);
    return NULL;
}


void menu_esp_set_info(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}

uart_config_t JsonToUartConfig(cJSON* UartConfig)
{
    if(UartConfig == NULL)
    {
        return globe_user_config.uart_config;
    }
    uart_config_t config;
    cJSON* BaudRatePtr = cJSON_GetObjectItem(UartConfig,"BaudRate");
    config.baud_rate = BaudRatePtr?BaudRatePtr->valueint:CONFIG_UART0_BAUD_RATE;

    cJSON* DataBitsPtr = cJSON_GetObjectItem(UartConfig,"DataBits");
    config.data_bits = DataBitsPtr?DataBitsPtr->valueint:CONFIG_UART0_DATA_BITS;

    cJSON* StopBitsPtr = cJSON_GetObjectItem(UartConfig,"StopBits");
    config.stop_bits = StopBitsPtr?StopBitsPtr->valueint:CONFIG_UART0_STOP_BITS;

    cJSON* ParityPtr = cJSON_GetObjectItem(UartConfig,"Parity");
    config.parity = ParityPtr?ParityPtr->valueint:CONFIG_UART0_PARITY;

    //Not support for FlowCTRL
    config.flow_ctrl = UART_HW_FLOWCTRL_DISABLE;
    config.rx_flow_ctrl_thresh = 0;

    return config;
}

user_wifi_config JsonToWifiConfig(cJSON* WifiConfig)
{
    if(WifiConfig == NULL)
    {
        return globe_user_config.wifi_config;
    }

    user_wifi_config config;
    cJSON* SSIDPtr = cJSON_GetObjectItem(WifiConfig,"SSID");
    memcpy(config.ssid,SSIDPtr?SSIDPtr->valuestring:CONFIG_WIFI_SSID,sizeof(config.ssid));

    cJSON* PasswordPtr = cJSON_GetObjectItem(WifiConfig,"PassWord");
    memcpy(config.password,PasswordPtr?PasswordPtr->valuestring:CONFIG_WIFI_PASSWORD,sizeof(config.password));

    cJSON* ChannelPtr = cJSON_GetObjectItem(WifiConfig,"Channel");
    config.channel = ChannelPtr?ChannelPtr->valueint:CONFIG_WIFI_CHANNEL;

    cJSON* IPPtr = cJSON_GetObjectItem(WifiConfig,"IP");
    memcpy(config.ip,IPPtr?IPPtr->valuestring:CONFIG_IPV4_ADDR,sizeof(config.ip));

    cJSON* PortPtr = cJSON_GetObjectItem(WifiConfig,"TCPPort");
    config.port = PortPtr?PortPtr->valueint:CONFIG_PORT;

    cJSON* UDPPortPtr = cJSON_GetObjectItem(WifiConfig,"UDPPort");
    config.udp_port = UDPPortPtr?UDPPortPtr->valueint:CONFIG_UDP_PORT;

    return config;
}

system_config_t JsonToSystemConfig(cJSON* SystemConfig)
{
    if(SystemConfig == NULL)
    {
        return globe_user_config.system_config;
    }

    system_config_t config;
    cJSON* AuthorPtr = cJSON_GetObjectItem(SystemConfig,"Author");
    ESP_LOGI("Copyright","©2025 ChangBuZhaoGong");
    ESP_LOGI("Author","%s",AuthorPtr->string);
    memcpy(config.Author,"\103\102\132\107",sizeof("\103\102\132\107"));
    return config;
}

user_config_t CombineAllConfig(uart_config_t UartConfig, 
                            user_wifi_config WifiConfig,
                            system_config_t SystemConfig)
{
    user_config_t Config = {
        .uart_config = UartConfig,
        .wifi_config = WifiConfig,
        .system_config = SystemConfig
    };
    return Config;
}

