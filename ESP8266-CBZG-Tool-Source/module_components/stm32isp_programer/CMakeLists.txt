idf_component_register(SRCS "stm32isp_programer.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log" "lwip" \
                    "driver" "freertos" "user_heart_beat" "cjson"
                    )