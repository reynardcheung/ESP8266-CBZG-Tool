#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include "sdkconfig.h"
#include "freertos/FreeRTOS.h"
#include "freertos/queue.h"
#include "driver/uart.h"
#include "esp_attr.h"
#include "esp_err.h"
#include "user_flash_rw.h"

extern void ICACHE_FLASH_ATTR user_uart_init(void);

#ifdef __cplusplus
}
#endif
