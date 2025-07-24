#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include "sdkconfig.h"
#include "freertos/FreeRTOS.h"
#include "esp_event_base.h"
#include "tcpip_adapter.h"
#include "nvs.h"
#include "nvs_flash.h"
#include "esp_attr.h"
#include "user_wifi_sta_init.h"
#include "driver/uart.h"
#include "user_web_config.h"
#include "task.h"

typedef struct
{  
    char ssid[32];  
    char password[64];  
    int channel;
    uint16_t port;
    uint16_t udp_port;
    char ip[16];  
}user_wifi_config;

typedef struct
{  
    char Author[5];
}system_config_t;  

typedef struct
{
    user_wifi_config wifi_config;
    uart_config_t uart_config;
    system_config_t system_config;
}user_config_t;  

extern user_config_t globe_user_config;

extern void ICACHE_FLASH_ATTR ICACHE_FLASH_ATTR esp_flash_rw_init(bool* FLAG);
extern void ICACHE_FLASH_ATTR reset_config(void);
extern bool ICACHE_FLASH_ATTR read_config(void);
extern bool ICACHE_FLASH_ATTR save_config(const user_config_t *config);

#ifdef __cplusplus
}
#endif
