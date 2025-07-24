#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include <string.h>
#include "freertos/FreeRTOS.h"
#include "sdkconfig.h"
#include "esp_timer.h"
#include "driver/uart.h"
#include "esp_system.h"
#include "esp_ping.h"
#include "ping/ping_sock.h"
#include "user_flash_rw.h"

extern void ICACHE_FLASH_ATTR heart_beat_init(void);
extern void heart_beat_reset(void);
extern void heart_beat_enable(bool enabled);
extern void force_restart(char* RestartReason);

#ifdef __cplusplus
}
#endif
