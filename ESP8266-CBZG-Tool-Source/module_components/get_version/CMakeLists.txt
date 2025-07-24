idf_component_register(SRCS "get_version.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "sdkconfig" "module_command" "lwip"
                    )