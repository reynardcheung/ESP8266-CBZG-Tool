#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "sdkconfig.h"
#include "module_command.h"
#include "lwip/sockets.h"
#include "esp_log.h"
#include "user_heart_beat.h"

void menu_esp_heart_beat(void);

#ifdef __cplusplus
}
#endif
