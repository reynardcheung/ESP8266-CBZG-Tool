idf_component_register(SRCS "user_gpio_init.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "driver" "esp8266" "esp_err" "esp_attr"
                    )