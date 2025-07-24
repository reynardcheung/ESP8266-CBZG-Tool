idf_component_register(SRCS "SSD1306.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log" "lwip" \
                    "user_gpio_init" "OLED" "cjson"
                    )