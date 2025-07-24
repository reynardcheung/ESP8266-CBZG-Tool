idf_component_register(SRCS "module_command.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "freertos" "sdkconfig" "driver" "user_heart_beat"
                    )