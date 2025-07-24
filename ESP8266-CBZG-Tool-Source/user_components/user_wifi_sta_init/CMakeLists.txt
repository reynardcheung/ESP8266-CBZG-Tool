idf_component_register(SRCS "user_wifi_sta_init.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "esp_netif" "esp_wifi" \
                    "esp_event" "esp_err" "esp_attr" "nvs_flash" \
                    "esp_flash_rw" "event_groups" "user_flash_rw" \
                    "user_web_config"
                    )