idf_component_register(SRCS "read_info.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "esp_log" "lwip" \
                    "user_flash_rw" "cjson"
                    )