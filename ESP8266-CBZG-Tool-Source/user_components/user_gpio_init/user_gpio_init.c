#include "user_gpio_init.h"

void ICACHE_FLASH_ATTR user_gpio_init(void)
{
    gpio_config_t gpio_config_t;
    /* 初始化gpio配置结构体 */
    
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_0);      //选择gpio
    gpio_config_t.mode = CONFIG_GPIO0_MODE;                 //输出模式
    gpio_config_t.pull_down_en = CONFIG_GPIO0_PULLDOWN_MODE;//下拉模式
    gpio_config_t.pull_up_en = CONFIG_GPIO0_PULLUP_MODE;    //上拉模式
    gpio_config_t.intr_type = CONFIG_GPIO0_INTR_LEVEL;      //中断模式
    /* 用给定的设置配置GPIO */
    gpio_config(&gpio_config_t);
    
    /* 初始化gpio配置结构体 */
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_2);      //选择gpio
    gpio_config_t.mode = CONFIG_GPIO2_MODE;                  //输出模式
    gpio_config_t.pull_down_en = CONFIG_GPIO2_PULLDOWN_MODE;     //下拉模式
    gpio_config_t.pull_up_en = CONFIG_GPIO2_PULLUP_MODE;          //上拉模式
    gpio_config_t.intr_type = CONFIG_GPIO2_INTR_LEVEL;            //中断模式
    /* 用给定的设置配置GPIO */
    gpio_config(&gpio_config_t);

    PIN_FUNC_SELECT(PERIPHS_IO_MUX_GPIO2_U,FUNC_GPIO2);
    gpio_set_level(GPIO_NUM_0,CONFIG_GPIO0_INIT_LEVEL);
    gpio_set_level(GPIO_NUM_2,CONFIG_GPIO2_INIT_LEVEL);
}

void ICACHE_FLASH_ATTR user_gpio_iic_init(void)
{
    gpio_config_t gpio_config_t;
    /* 初始化gpio配置结构体 */
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_0);      //选择gpio
    gpio_config_t.mode = GPIO_MODE_OUTPUT_OD;               //输出模式
    gpio_config_t.pull_up_en = GPIO_PULLUP_DISABLE;         //上拉模式
    gpio_config_t.pull_down_en = GPIO_PULLDOWN_DISABLE;     //下拉模式
    gpio_config_t.intr_type = GPIO_INTR_DISABLE;            //中断模式
    /* 用给定的设置配置GPIO0 */
    gpio_config(&gpio_config_t);
    /* 初始化gpio配置结构体 */
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_2);      //选择gpio
    /* 用给定的设置配置GPIO2 */
    gpio_config(&gpio_config_t);
    PIN_FUNC_SELECT(PERIPHS_IO_MUX_GPIO2_U,FUNC_GPIO2);
}

void ICACHE_FLASH_ATTR user_gpio_reset(void)
{
    gpio_config_t gpio_config_t;

    // 配置 GPIO0 为输出（原来为输入）
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_0);
    gpio_config_t.mode = GPIO_MODE_OUTPUT;
    gpio_config_t.pull_down_en = GPIO_PULLDOWN_DISABLE;
    gpio_config_t.pull_up_en = GPIO_PULLUP_DISABLE; // 如果你需要高电平或低电平控制，用输出模式
    gpio_config_t.intr_type = GPIO_INTR_DISABLE;
    gpio_config(&gpio_config_t);

    // 配置 GPIO2 为输出（原来为输入）
    gpio_config_t.pin_bit_mask = (1ULL << GPIO_NUM_2);
    gpio_config_t.mode = GPIO_MODE_OUTPUT;
    gpio_config_t.pull_down_en = GPIO_PULLDOWN_DISABLE;
    gpio_config_t.pull_up_en = GPIO_PULLUP_DISABLE; // 根据实际需求设置
    gpio_config_t.intr_type = GPIO_INTR_DISABLE;
    gpio_config(&gpio_config_t);

    PIN_FUNC_SELECT(PERIPHS_IO_MUX_GPIO2_U,FUNC_GPIO2);//使用GPIO2必须带有本行代码
}

void ICACHE_FLASH_ATTR user_pwm_gpio_config(uint32_t pin)
{
    gpio_config_t io_conf;
    io_conf.intr_type = GPIO_INTR_DISABLE;
    io_conf.mode = GPIO_MODE_OUTPUT;
    io_conf.pin_bit_mask = (1ULL << pin);
    io_conf.pull_down_en = 0;
    io_conf.pull_up_en = 0;
    gpio_config(&io_conf);
}