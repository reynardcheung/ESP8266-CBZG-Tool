#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include <stdint.h>
#include <string.h>
#include "sdkconfig.h"
#include "module_command.h"
#include "freertos/queue.h"
#include "esp_log.h"

void example_menu_init(void);

#ifdef __cplusplus
}
#endif
