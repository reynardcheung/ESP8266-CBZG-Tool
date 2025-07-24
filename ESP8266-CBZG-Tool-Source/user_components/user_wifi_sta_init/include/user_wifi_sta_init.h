#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include "sdkconfig.h"
#include "esp_wifi.h"
#include "event_groups.h"
#include <string.h>
#include <stdint.h>
#include "esp_event_base.h"
#include "esp_event.h"
#include "esp_err.h"
#include "esp_attr.h"
#include "esp_netif.h"
#include "user_flash_rw.h"
#include "user_web_config.h"


extern void wifi_init_sta(bool* FLAG);

#ifdef __cplusplus
}
#endif
