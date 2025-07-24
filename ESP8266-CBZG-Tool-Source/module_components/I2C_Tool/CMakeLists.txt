idf_component_register(SRCS "I2C_Tool.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log" "lwip" \
                    "user_gpio_init" "driver" "cjson"
                    )