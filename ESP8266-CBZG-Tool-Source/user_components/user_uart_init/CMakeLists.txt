idf_component_register(SRCS "user_uart_init.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "freertos" "driver" "esp_attr" "esp_err" "user_flash_rw"
                    )