idf_component_register(SRCS "heart_beat.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "lwip" "esp_log" "user_heart_beat"
                    )