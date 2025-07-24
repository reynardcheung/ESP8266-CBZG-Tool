/* 
 * Copyright (c) 2025, 长不着弓
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain this notice.
 * 2. Redistributions in binary form must reproduce this notice in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of 长不着弓 nor the names of its contributors may be used
 *    to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DAMAGES.
 */
#include "gpio_manager.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "GPIOManager";
static char NAME[] = CONFIG_GPIO_MANAGER_MODULE_NAME;
static char BRIEF[] = CONFIG_GPIO_MANAGER_BRIEF;
static char VERSION[] = CONFIG_GPIO_MANAGER_VERSION;
static char CMD_NAME[] = CONFIG_GPIO_MANAGER_CMD_NAME;
#ifndef CONFIG_GPIO_MANAGER_RUNNING
#define CONFIG_GPIO_MANAGER_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_GPIO_MANAGER_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};


/*
JSON Example:
{
    "GPIO_NUM": 2,
    "GPIO_MODE": {
        "Enable": 0,
        "Mode":0,
        "PullUp": 1,
        "PUllDown": 0,
        "Level":1
    },
    "PWM_MODE": {
        "Enable": 0,
        "Duty": 10,
        "Period": 1000,
        "Phase": 10.0
    }
}
*/

void ExecuteJsonCommand(CommandPack_t *Pack);

/*
拼劲全力无法实现，暂未查明原因
pwm_init放于入口函数InitAction，直接触发↓
Guru Meditation Error: Core  0 panic'ed (LoadProhibited). Exception was unhandled.
Core 0 register dump:
PC      : 0x401015da  PS      : 0x00000033  A0      : 0x40103715  A1      : 0x3ffea510
0x401015da: pwm_timer_intr_handler at E:/ESP8266/msys32/home/A/esp/ESP8266_RTOS_SDK/components/esp8266/driver/pwm.c:287
pwm_init放入ExecuteJsonCommand由解析JSON后执行无法正常产生波形，示波器检测无响应
*/
#define PWM_FUNCTION 0
#if PWM_FUNCTION 
bool PWMISINIT_0 = false;
bool PWMISINIT_2 = false;
#endif

static void* InitAction(void *arg)
{

    SetActionAddLock(&Command,true,portMAX_DELAY);
    user_gpio_init();
    #if PWM_FUNCTION
    uint32_t duties[2] = {500,500};
    uint32_t pin[2] = {GPIO_NUM_0, GPIO_NUM_2};
    pwm_init(1000,duties,2,&pin);
    #endif
    lwip_send(tcp_socket,"y",1,0);
    return NULL;
}

static void* Action(void *arg)
{
    if(arg == NULL)
    {
        return NULL;
    }

    CommandPack_t *CommandPack = (CommandPack_t *)arg;

    ExecuteJsonCommand(CommandPack);

    return NULL;
}

static void* EXTIAction(void *arg)
{
    SetActionAddLock(&Command,false,10);
    #if PWM_FUNCTION
    if(PWMISINIT_0 || PWMISINIT_2)
    {
        PWMISINIT_0 = false;
        PWMISINIT_2 = false;
        pwm_deinit();
    }
    #endif
    user_gpio_init();
    lwip_send(tcp_socket,"y",1,0);
    return NULL;
}

void menu_gpio_manager(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}

void ExecuteJsonCommand(CommandPack_t *Pack)
{
    
    if (Pack->Length <= 0)
    {
        return;
    }

    cJSON *root = cJSON_Parse((char*)Pack->Data);
    if (!root)
    {
        return;
    }

    cJSON *gpio_num = cJSON_GetObjectItem(root, "GPIO_NUM");
    if (!cJSON_IsNumber(gpio_num))
    {
        cJSON_Delete(root);
        return;
    }
    int pin = gpio_num->valueint;

    cJSON *gpio_mode = cJSON_GetObjectItem(root, "GPIO_MODE");
    if (gpio_mode)
    {
        cJSON *enable = cJSON_GetObjectItem(gpio_mode, "Enable");
        if (enable->valueint)
        {
            cJSON *mode = cJSON_GetObjectItem(gpio_mode, "Mode");
            cJSON *pullup = cJSON_GetObjectItem(gpio_mode, "PullUp");
            cJSON *pulldown = cJSON_GetObjectItem(gpio_mode, "PullDown");
            cJSON *level = cJSON_GetObjectItem(gpio_mode, "Level");
            
            gpio_mode_t Mode;
            switch (mode->valueint)
            {
            case 0:
            {
                Mode = GPIO_MODE_DISABLE;
                break;
            }
            case 1:
            {
                Mode = GPIO_MODE_INPUT;
                break;
            }
            case 2:
            {
                Mode = GPIO_MODE_OUTPUT;
                break;
            }
            case 3:
            {
                Mode = GPIO_MODE_OUTPUT_OD;
                break;
            }
            default:
                Mode = GPIO_MODE_DISABLE;
                break;
            }

            gpio_config_t io_conf =
            {
                .pin_bit_mask = (1ULL << pin),
                .mode = Mode,
                .pull_up_en = pullup->valueint ? 1 : 0,
                .pull_down_en = pulldown->valueint ? 1 : 0,
                .intr_type = GPIO_INTR_DISABLE
            };
            
            #if PWM_FUNCTION
            if((PWMISINIT_0 == true && pin == 0))
            {
                PWMISINIT_0 = false;
                pwm_stop(0xff);
            }
            else if(PWMISINIT_2 == true && pin == 2)
            {
                PWMISINIT_2 = false;
                pwm_stop(0xff);
            }
            else
            {

            }
            #endif

            gpio_config(&io_conf);
            
            gpio_set_level((gpio_num_t)pin, level->valueint);

            ESP_LOGI(TAG,"GPIO PIN:%d",pin);
            cJSON_Delete(root);
            lwip_send(tcp_socket,"y",1,0);
            return;
        }
    }

    #if PWM_FUNCTION
    cJSON *pwm_mode = cJSON_GetObjectItem(root, "PWM_MODE");
    if (pwm_mode)
    {
        cJSON *enable = cJSON_GetObjectItem(pwm_mode, "Enable");
        if (enable->valueint)
        {
            cJSON *duty = cJSON_GetObjectItem(pwm_mode, "Duty");
            cJSON *period = cJSON_GetObjectItem(pwm_mode, "Period");
            cJSON *phase = cJSON_GetObjectItem(pwm_mode, "Phase");
            
            if(pin == 0)
            {
                uint32_t Pin = GPIO_Pin_0;
                uint32_t Duty = duty->valueint;
                if(PWMISINIT_0 == false)
                {
                    user_pwm_gpio_config(Pin);
                }
                pwm_set_phase(0,(float)phase->valuedouble);
                pwm_set_duty(0,Duty);
                pwm_start();
                PWMISINIT_0 = true;
            }
            else if(pin == 2)
            {
                uint32_t Pin = GPIO_Pin_2;
                uint32_t Duty = duty->valueint;
                if(PWMISINIT_2 == false)
                {
                    user_pwm_gpio_config(Pin);
                }
                pwm_set_phase(1,(float)phase->valuedouble);
                pwm_set_duty(1,Duty);
                pwm_start();
                PWMISINIT_2 = true;
            }
            else
            {

            }
            ESP_LOGI(TAG,"GPIO PIN PWM:%d",pin);
            cJSON_Delete(root);
            lwip_send(tcp_socket,"y",1,0);
            return;
        }
    }
    #endif
    cJSON_Delete(root);
    return;
}

