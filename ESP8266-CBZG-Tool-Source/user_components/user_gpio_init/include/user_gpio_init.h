#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include "sdkconfig.h"
#include "driver/gpio.h"
#include "esp8266/gpio_struct.h"
#include "esp_err.h"
#include "esp_attr.h"

#define SetGPIOHigh(Number) GPIO.out_w1ts |= (0x1 << Number)
#define SetGPIOLow(Number)  GPIO.out_w1tc |= (0x1 << Number)
#define GPIO_SET_LEVEL(Number, IOLevel) if(IOLevel){SetGPIOHigh(Number);}else{SetGPIOLow(Number);}

extern void ICACHE_FLASH_ATTR user_gpio_init(void);
extern void ICACHE_FLASH_ATTR user_gpio_iic_init(void);
extern void ICACHE_FLASH_ATTR user_gpio_reset(void);
extern void ICACHE_FLASH_ATTR user_pwm_gpio_config(uint32_t pin);

#ifdef __cplusplus
}
#endif
