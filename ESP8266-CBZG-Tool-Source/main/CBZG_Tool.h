#include "sdkconfig.h"
#include <stdbool.h>
#include <string.h>
#include <sys/param.h>
#include <errno.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "freertos/queue.h"
#include "freertos/semphr.h"
#include "esp_system.h"
#include "esp_log.h"
#include "esp_netif.h"
#include "esp_event.h"
#include "esp_wifi.h"
#include "esp_err.h"
#include "esp_attr.h"
#include "nvs.h"
#include "nvs_flash.h"
#include "driver/uart.h"
#include "driver/gpio.h"
#include "lwip/err.h"
#include "lwip/sockets.h"
#include "lwip/sys.h"
#include <lwip/netdb.h>
#include "esp_sleep.h"
#include "esp_err.h"
#include "esp_heap_caps.h"

/*User Include*/
#include "user_wifi_sta_init.h"
#include "user_uart_init.h"
#include "user_gpio_init.h"
#include "user_flash_rw.h"
#include "user_heart_beat.h"
#include "wuart.h"
#include "module_command.h"
#include "user_web_config.h"

/*Menu Include*/
//#include "test_menu.h"
#include "get_id.h"
#include "get_version.h"
#include "get_module.h"
#include "gpio_manager.h"
#include "heart_beat.h"
#include "restart.h"
#include "read_info.h"
#include "set_info.h"
#include "stm32isp_programer.h"
#include "I2C_Tool.h"
#include "SSD1306.h"

extern int tcp_socket;
extern int udp_socket;
extern void user_udp_send(uint8_t* bytes,uint32_t len);
















