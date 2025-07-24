idf_component_register(SRCS "user_flash_rw.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "freertos" "esp_event" "tcpip_adapter" \
                    "nvs" "nvs_flash" "esp_attr" "user_wifi_sta_init" \
                    "driver" "user_web_config"
                    )