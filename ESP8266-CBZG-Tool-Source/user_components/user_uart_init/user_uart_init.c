#include "user_uart_init.h"

void ICACHE_FLASH_ATTR user_uart_init(void)
{
    if(uart_param_config(UART_NUM_0,&globe_user_config.uart_config)==ESP_ERR_INVALID_ARG)                         //bps = 115200
    {
        reset_config();
        esp_restart();
    }

    if(uart_driver_install(UART_NUM_0, 512 * 2, 0, 0, 0, 0)==ESP_ERR_INVALID_ARG)
    {
        reset_config();
        esp_restart();
    }
}
