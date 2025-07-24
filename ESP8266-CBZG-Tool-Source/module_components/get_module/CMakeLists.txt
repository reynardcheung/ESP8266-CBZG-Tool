idf_component_register(SRCS "get_module.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "lwip" "cjson"
                    )