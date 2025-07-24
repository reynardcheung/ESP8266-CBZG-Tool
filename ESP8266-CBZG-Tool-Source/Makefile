#
# This is a project Makefile. It is assumed the directory this Makefile resides in is a
# project subdirectory.
#

PROJECT_NAME := CBZG_Tool
EXTRA_COMPONENT_DIRS += module_components
EXTRA_COMPONENT_DIRS += module_components/command_components
EXTRA_COMPONENT_DIRS += module_components/get_id
EXTRA_COMPONENT_DIRS += module_components/get_version
EXTRA_COMPONENT_DIRS += module_components/get_module
EXTRA_COMPONENT_DIRS += module_components/test_menu
EXTRA_COMPONENT_DIRS += module_components/gpio_manager
EXTRA_COMPONENT_DIRS += module_components/heart_beat
EXTRA_COMPONENT_DIRS += module_components/wuart
EXTRA_COMPONENT_DIRS += module_components/restart
EXTRA_COMPONENT_DIRS += module_components/read_info
EXTRA_COMPONENT_DIRS += module_components/set_info
EXTRA_COMPONENT_DIRS += module_components/stm32isp_programer
EXTRA_COMPONENT_DIRS += module_components/I2C_Tool
EXTRA_COMPONENT_DIRS += module_components/SSD1306

EXTRA_COMPONENT_DIRS += user_components
EXTRA_COMPONENT_DIRS += user_components/user_wifi_sta_init
EXTRA_COMPONENT_DIRS += user_components/user_uart_init
EXTRA_COMPONENT_DIRS += user_components/user_gpio_init
EXTRA_COMPONENT_DIRS += user_components/user_flash_rw
EXTRA_COMPONENT_DIRS += user_components/user_heart_beat
EXTRA_COMPONENT_DIRS += user_components/user_oled_driver
EXTRA_COMPONENT_DIRS += user_components/user_web_config

export ProjectPath := $(CURDIR)

include $(IDF_PATH)/make/project.mk

