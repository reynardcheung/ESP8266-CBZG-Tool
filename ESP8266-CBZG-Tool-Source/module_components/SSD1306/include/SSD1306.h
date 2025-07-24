#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "sdkconfig.h"
#include "module_command.h"
#include "esp_log.h"
#include "lwip/sockets.h"
#include "user_gpio_init.h"
#include "OLED.h"
#include "cJSON.h"
#include <stdlib.h>

void menu_ssd1306(void);

#ifdef __cplusplus
}
#endif
