#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "sdkconfig.h"
#include "module_command.h"
#include "esp_log.h"
#include "lwip/sockets.h"
#include "user_flash_rw.h"
#include <stdlib.h>
#include "cJSON.h"

void menu_esp_set_info(void);

#ifdef __cplusplus
}
#endif
