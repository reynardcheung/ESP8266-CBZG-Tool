idf_component_register(SRCS "wuart.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log" "lwip" \
                    "driver" "freertos" "user_heart_beat" "user_gpio_init"
                    )