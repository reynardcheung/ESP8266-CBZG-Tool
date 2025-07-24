idf_component_register(SRCS "user_heart_beat.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "freertos" "sdkconfig" "esp_timer" "driver" \
                    "esp_system" "esp_ping" "ping" "user_flash_rw"
                    )