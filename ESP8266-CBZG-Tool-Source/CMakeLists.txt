# The following five lines of boilerplate have to be in your project's
# CMakeLists in this exact order for cmake to work correctly
cmake_minimum_required(VERSION 3.5)

ProjectPath := $(CURDIR)
set(MULTI_COMPONENT_DIRS  
    ${PROJECT_PATH}/user_components
)

set(MULTI_COMPONENT_DIRS  
    ${PROJECT_PATH}/module_components
)  

set(EXTRA_COMPONENT_DIRS  
    user_components/user_wifi_sta_init  
    user_components/user_uart_init  
    user_components/user_gpio_init
    user_components/user_stm_isp_switch
    user_components/user_flash_rw
    user_components/user_heart_beat
    user_components/user_oled_driver
    user_components/user_web_config
    module_components/command_components
    module_components/test_menu
    module_components/esp_get_id
    module_components/esp_get_version
    module_components/esp_get_module
    module_components/gpio_manager
    module_components/heart_beat
    module_components/wuart
    module_components/restart
    module_components/read_info
    module_components/set_info
    module_components/stm32f10x_programer
    module_components/I2C_Tool
    module_components/SSD1306
)  

include($ENV{IDF_PATH}/tools/cmake/project.cmake)
project(CBZG_Tool)
