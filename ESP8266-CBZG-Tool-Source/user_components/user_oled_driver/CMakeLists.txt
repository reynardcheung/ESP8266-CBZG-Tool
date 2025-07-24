idf_component_register(SRCS "OLED.c" "OLED_Data.c"
                    INCLUDE_DIRS "include"
                    REQUIRES "driver" "freertos"
                    )