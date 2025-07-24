idf_component_register(SRCS "user_web_config.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "freertos" "esp_log" \
                    "esp_http_server" "cjson" "esp_attr"
                    )