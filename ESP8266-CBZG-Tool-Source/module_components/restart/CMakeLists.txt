idf_component_register(SRCS "restart.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log"
                    )