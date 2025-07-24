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

#include "CBZG_Tool.h"

/*User Define*/
#define USER_ACK    "\171"
#define USER_NACK   "\037"
#define USER_ERR    "E"

#define ERROR_BASE_TYPE(ret)    if(ret == false) \
                                { \
                                    char ERROR_BASE_TYPE_TAG[32]; \
                                    ESP_LOGE(ERROR_BASE_TYPE_TAG,"Create Error:%d",__LINE__); \
                                    esp_restart(); \
                                }



/*Enum Declaration*/


/*Struct Declaration*/
typedef struct 
{
    uint8_t buffer[8][2048];
    uint32_t usage;
} tcp_recv_buf_t;

bool BufferLock[8] = {0};

typedef struct 
{
    uint8_t buffer[2][512];
    uint32_t usage;
} udp_recv_buf_t;

/*Variable Declaration*/
int tcp_socket;
int udp_socket;
bool WIFI_INIT_FLAG = false;
bool WIFI_CONNECT_FLAG = false;

static tcp_recv_buf_t tcp_recv_mem_pool = 
{
    .buffer = {{0}},
    .usage = 0,
};
static udp_recv_buf_t udp_recv_mem_pool = 
{
    .buffer = {{0}},
    .usage = 0,
};

/*Task Handle Declaration*/
TaskHandle_t wifi_client_task_handle;
TaskHandle_t wifi_client_udp_task_handle;

/*Function Declaration*/
void ICACHE_FLASH_ATTR task_init(void);
void wifi_client_task(void *arg);
void wifi_client_udp_task(void *arg); 

/*
void MUART0_Printf(const char *format, ...)
{  
    char buffer[256];
    va_list args;  

    va_start(args, format);  

    vsnprintf(buffer, sizeof(buffer), format, args);  

    uart_write_bytes(UART_NUM_0,buffer,strlen(buffer));

    va_end(args);  
}
*/
/***************主任务***************/
void user_udp_send(uint8_t* bytes,uint32_t len)
{
    static const char *TAG = "Dev UDP Send";
    const struct sockaddr_in destUDPAddr = 
    {
        .sin_addr.s_addr = inet_addr(globe_user_config.wifi_config.ip),
        .sin_family = AF_INET,
        .sin_port = htons(globe_user_config.wifi_config.udp_port),
    };

    int err = sendto(udp_socket, bytes, len, 0, (struct sockaddr *)&destUDPAddr, sizeof(destUDPAddr));
    if (err < 0) 
    {
        ESP_LOGE(TAG, "Error during sendto: %d", errno);
    }
    else
    {
        ESP_LOGI(TAG, "Message sent");
    }
}

void menu_init()
{
    menu_esp_get_id();
    menu_esp_get_version();
    menu_esp_get_module();
    menu_gpio_manager();
    menu_esp_heart_beat();
    menu_esp_wuart();
    menu_esp_restart();
    menu_esp_read_info();
    menu_esp_set_info();
    menu_stm32f10x_programer();
    menu_i2c_tool();
    menu_ssd1306();
}

void app_main()
{
    tcpip_adapter_init();
    esp_flash_rw_init(&WIFI_INIT_FLAG);
    if(!WIFI_INIT_FLAG)
    {
        start_web_config();
        vTaskDelete(NULL);
    }

    user_uart_init();
    user_gpio_init();
    wifi_init_sta(&WIFI_CONNECT_FLAG);
    if(!WIFI_CONNECT_FLAG)
    {
        vTaskDelete(NULL);
    }
    

    if(!CommandMenuInit())
    {
        esp_restart();
    }

    menu_init();
    task_init();
    vTaskDelete(NULL);
}
/***************任务初始化***************/
void ICACHE_FLASH_ATTR task_init(void)
{
    ERROR_BASE_TYPE(xTaskCreate(
        wifi_client_task, 
        CONFIG_WIFI_CLIENT_TASK_NAME, 
        CONFIG_WIFI_CLIENT_STACK_DEPTH, 
        NULL,
        CONFIG_WIFI_CLIENT_PRIORITY, 
        &wifi_client_task_handle
    ));

    ERROR_BASE_TYPE(xTaskCreate(
        wifi_client_udp_task, 
        CONFIG_WIFI_CLIENT_UDP_TASK_NAME, 
        CONFIG_WIFI_CLIENT_UDP_STACK_DEPTH, 
        NULL,
        CONFIG_WIFI_CLIENT_UDP_PRIORITY, 
        &wifi_client_udp_task_handle
    ));
}
/***************监听任务***************/
void wifi_client_task(void *arg)
{
    static const char *TAG = "Dev RCV";
    static CommandPack_t CommandPack;
    char addr_str[128];
    struct sockaddr_in destTCPAddr;
    int addr_family;
    int ip_protocol;
    int opt = 1;
    int recv_buf_size = CONFIG_MAX_RECEIVE_DATA_LEN;
    
    while (true)
    {
#ifdef CONFIG_IPV4
        destTCPAddr.sin_addr.s_addr = inet_addr(globe_user_config.wifi_config.ip);
        destTCPAddr.sin_family = AF_INET;
        destTCPAddr.sin_port = htons(globe_user_config.wifi_config.port);
        addr_family = AF_INET;
        ip_protocol = IPPROTO_IP;
        inet_ntoa_r(destTCPAddr.sin_addr, addr_str, sizeof(addr_str) - 1);
#elif CONFIG_IPV6
        struct sockaddr_in6 destTCPAddr;
        inet6_aton(globe_user_config.wifi_config.ip, &destTCPAddr.sin6_addr);
        destTCPAddr.sin6_family = AF_INET6;
        destTCPAddr.sin6_port = htons(globe_user_config.wifi_config.port);
        destTCPAddr.sin6_scope_id = tcpip_adapter_get_netif_index(TCPIP_ADAPTER_IF_STA);
        addr_family = AF_INET6;
        ip_protocol = IPPROTO_IPV6;
        inet6_ntoa_r(destTCPAddr.sin6_addr, addr_str, sizeof(addr_str) - 1);
#endif

        tcp_socket = socket(addr_family, SOCK_STREAM, ip_protocol);

        if (tcp_socket < 0)
        {
            ESP_LOGE(TAG, "Unable to create tcp socket: errno %d", errno);
            vTaskDelay(pdMS_TO_TICKS(1000));
            close(tcp_socket);
            continue;
        }
        ESP_LOGD("TCP","IP:%s,Port:%d",addr_str,globe_user_config.wifi_config.port);
        int err = connect(tcp_socket, (struct sockaddr *)&destTCPAddr, sizeof(destTCPAddr));
        if (err != 0)
        {
            ESP_LOGE(TAG, "Socket unable to connect: errno %d", errno);
            close(tcp_socket);
            vTaskDelay(pdMS_TO_TICKS(1000));
            continue;
        }
        ESP_LOGI(TAG, "Successfully connected");

        heart_beat_init();

        if(setsockopt(tcp_socket, SOL_SOCKET, SO_KEEPALIVE,(const void *)&opt, sizeof(int)) != 0)
        {
            ESP_LOGE(TAG,"Unable to set SO_KEEPALIVE");
        }
        
        if(setsockopt(tcp_socket, IPPROTO_TCP, TCP_NODELAY, (const void *)&opt, sizeof(int)) != 0)
        {
            ESP_LOGE(TAG,"Unable to set TCP_NODELAY");
        }

        if(setsockopt(tcp_socket, SOL_SOCKET, SO_RCVBUF, &recv_buf_size, sizeof(recv_buf_size)) != 0)
        {
            ESP_LOGE(TAG,"Unable to set SO_RCVBUF");
        }

        while (true)
        {
            while(BufferLock[tcp_recv_mem_pool.usage])
            {
                vTaskDelay(pdMS_TO_TICKS(50));
            }
            CommandPack.Data = tcp_recv_mem_pool.buffer[tcp_recv_mem_pool.usage];
            CommandPack.Lock = &BufferLock[tcp_recv_mem_pool.usage];
            CommandPack.Length = recv(tcp_socket, CommandPack.Data, sizeof(tcp_recv_mem_pool.buffer[0]), 0);

            if (CommandPack.Length < 0)
            {
                ESP_LOGE(TAG, "recv failed: errno %d", errno);
                break;
            }

            tcp_recv_mem_pool.usage == (sizeof(tcp_recv_mem_pool.buffer)/sizeof(tcp_recv_mem_pool.buffer[0]) - 1)?tcp_recv_mem_pool.usage=0:tcp_recv_mem_pool.usage++;

            SendToCommandAnalyse(CommandPack,portMAX_DELAY);
            
            ESP_LOGI(TAG, "Received %d bytes from %s:", CommandPack.Length, addr_str);
        }

        if (tcp_socket != -1)
        {
            shutdown(tcp_socket, 0);
            close(tcp_socket);
        }
        force_restart("Socket has been disconnect");
    }
    force_restart("A fatal socket problem has been occurred");
    vTaskDelete(NULL);
}

void wifi_client_udp_task(void *arg)
{
    static const char *TAG = "Dev UDP RCV";
    static CommandPack_t CommandPack;
    char addr_str[128];
    struct sockaddr_in destUDPAddr;
    int addr_family;
    int ip_protocol;
    struct sockaddr_in sourceAddr;
    socklen_t socklen = sizeof(sourceAddr);

    memset(&destUDPAddr, 0, sizeof(destUDPAddr));

    while (true)
    {
#ifdef CONFIG_IPV4
        destUDPAddr.sin_addr.s_addr = inet_addr("0.0.0.0");
        destUDPAddr.sin_family = AF_INET;
        destUDPAddr.sin_port = htons(globe_user_config.wifi_config.udp_port);
        addr_family = AF_INET;
        ip_protocol = IPPROTO_IP;
        inet_ntoa_r(destUDPAddr.sin_addr, addr_str, sizeof(addr_str) - 1);
#elif CONFIG_IPV6
        struct sockaddr_in6 destAddr;
        inet6_aton("ff02::1", &destAddr.sin6_addr);
        destAddr.sin6_family = AF_INET6;
        destAddr.sin6_port = htons(globe_user_config.wifi_config.udp_port);
        destAddr.sin6_scope_id = tcpip_adapter_get_netif_index(TCPIP_ADAPTER_IF_STA);
        addr_family = AF_INET6;
        ip_protocol = IPPROTO_IPV6;
        inet6_ntoa_r(destAddr.sin6_addr, addr_str, sizeof(addr_str) - 1);
#endif

        udp_socket = socket(addr_family, SOCK_DGRAM, ip_protocol);

        if (udp_socket < 0)
        {
            ESP_LOGE(TAG, "Unable to create udp socket: errno %d", errno);
            vTaskDelay(pdMS_TO_TICKS(1000));
            close(udp_socket);
            break;
        }

        ESP_LOGD("UDP","Port:%d",globe_user_config.wifi_config.udp_port);

        if (bind(udp_socket, (struct sockaddr *)&destUDPAddr, sizeof(destUDPAddr)) != 0)
        {
            ESP_LOGE(TAG, "Socket bind failed, errno %d", errno);
            close(udp_socket);
            break;
        }
        ESP_LOGI(TAG, "Socket bind OK, waiting for data...");

        while (true)
        {
            CommandPack.Data = udp_recv_mem_pool.buffer[udp_recv_mem_pool.usage];
            CommandPack.Length = recvfrom(udp_socket, CommandPack.Data, sizeof(udp_recv_mem_pool.buffer[0]), 0, (struct sockaddr *)&sourceAddr, &socklen);

            if (CommandPack.Length < 0)
            {
                ESP_LOGE(TAG, "recvfrom failed: errno %d", errno);
                break;
            }
            else
            {
                ESP_LOGI(TAG, "Received %d bytes from %s:", CommandPack.Length, addr_str);
            }

            udp_recv_mem_pool.usage == (sizeof(udp_recv_mem_pool.buffer)/sizeof(udp_recv_mem_pool.buffer[0]) - 1)?udp_recv_mem_pool.usage=0:udp_recv_mem_pool.usage++;

            SendToDataQueue(CommandPack,portMAX_DELAY);

            ESP_LOGI(TAG, "Received %d bytes from %s:", CommandPack.Length, addr_str);
            //CommandPack.Data[CommandPack.Length] = '\0';
            //ESP_LOGI(TAG, "%s", CommandPack.Data);
        }

        if (udp_socket != -1)
        {
            shutdown(udp_socket, 0);
            close(udp_socket);
        }
        force_restart("UDP Socket has been disconnect");
    }
    force_restart("A fatal socket problem has been occurred");
    vTaskDelete(NULL);
}





















