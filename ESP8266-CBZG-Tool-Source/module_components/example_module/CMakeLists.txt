idf_component_register(SRCS "example_module.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "freertos" "esp_log"
                    )