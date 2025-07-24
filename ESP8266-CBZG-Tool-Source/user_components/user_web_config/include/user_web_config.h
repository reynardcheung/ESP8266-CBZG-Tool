#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "sdkconfig.h"
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "esp_log.h"
#include "esp_http_server.h"
#include "cJSON.h"
#include "esp_attr.h"

extern void start_web_config();

#ifdef __cplusplus
}
#endif
