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
#include "I2C_Tool.h"

static void* InitAction(void *arg);
static void* Action(void *arg);
static void* EXTIAction(void *arg);

static char* TAG = "I2CTool";
static char NAME[] = CONFIG_I2C_TOOL_MODULE_NAME;
static char BRIEF[] = CONFIG_I2C_TOOL_BRIEF;
static char VERSION[] = CONFIG_I2C_TOOL_VERSION;
static char CMD_NAME[] = CONFIG_I2C_TOOL_CMD_NAME;
#ifndef CONFIG_I2C_TOOL_RUNNING
#define CONFIG_I2C_TOOL_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_I2C_TOOL_RUNNING,
    .InitAction = InitAction,
    .Action = Action,
    .ExtiAction = EXTIAction
};

enum CMD_e
{
    ScanCMD = 0,
    ReadReg = 1,
    WriteReg = 2
};

/*
Recv JSON:
[
	{
		"CMD": 1,
		"Addr": 60,
		"Reg": 123,
        "Size":0,
		"Value": []
	},
	{
		"CMD": 1,
		"Addr": 60,
		"Reg": 123,
        "Size":0,
		"Value": []
	}
]

Send JSON:
[
	{
		"CMD": 1,
		"Addr": 60,
		"Reg": 123,
        "Size":0,
		"Value": []
	},
	{
		"CMD": 1,
		"Addr": 60,
		"Reg": 123,
        "Size":0,
		"Value": []
	}
]

Example:
[
  {
    "CMD": 0,
    "Addr": 0,
    "Reg": 0,
    "Size":0,
    "Value": []
  },
  {
    "CMD": 2,
    "Addr": 60,
    "Reg": 0,
    "Size":23,
    "Value": [174,213,240,168,63,211,0,64,161,200,218,18,129,207,217,241,219,48,164,166,141,20,175]
  }
]
*/

static uint8_t DeviceScan(i2c_port_t Port, uint8_t DeviceGroup[], uint8_t Number);
static cJSON* DeviceReadReg(i2c_port_t Port, uint8_t Address, int Size, uint8_t Reg);
static int DeviceWriteReg(i2c_port_t Port, uint8_t Address, uint8_t Reg, int Length, cJSON* Value);

#define I2C_CHECK(expr, str) \
    do { \
        esp_err_t __ret = (expr); \
        if (__ret != ESP_OK) { \
            ESP_LOGE(TAG, "%s failed: %s", str, esp_err_to_name(__ret)); \
            goto error; \
        } \
    } while(0)

#define DEVICE_MAX_NUMBER 8
uint8_t DeviceAddress[DEVICE_MAX_NUMBER] = {0};

static void* InitAction(void *arg)
{
    SetActionAddLock(&Command,true,portMAX_DELAY);

    int i2c_master_port = I2C_NUM_0;
    i2c_config_t conf;
    conf.mode = I2C_MODE_MASTER;
    conf.sda_io_num = GPIO_NUM_0;
    conf.sda_pullup_en = 1;
    conf.scl_io_num = GPIO_NUM_2;
    conf.scl_pullup_en = 1;
    conf.clk_stretch_tick = 300;
    ESP_ERROR_CHECK(i2c_driver_install(i2c_master_port, conf.mode));
    ESP_ERROR_CHECK(i2c_param_config(i2c_master_port, &conf));

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
    cJSON* root = cJSON_Parse((char *)CommandPack->Data);
    if(root == NULL)
    {
        lwip_send(tcp_socket,"[]",2,0);
        return NULL;
    }

    cJSON* send_root = cJSON_CreateArray();
    if(send_root == NULL)
    {
        lwip_send(tcp_socket,"[]",2,0);
        cJSON_Delete(root);
        return NULL;
    }


    for(int i = 0;;i++)
    {
        cJSON* item = cJSON_GetArrayItem(root,i);
        if(item == NULL)
        {
            break;
        }

        cJSON* CMD = cJSON_GetObjectItem(item,"CMD");
        cJSON* Addr = cJSON_GetObjectItem(item,"Addr");
        cJSON* Reg = cJSON_GetObjectItem(item,"Reg");
        cJSON* Size = cJSON_GetObjectItem(item,"Size");
        cJSON* Value = cJSON_GetObjectItem(item,"Value");

        if(CMD == NULL || Addr == NULL || Size == NULL || Reg == NULL || Value == NULL)
        {
            continue;
        }

        switch ((enum CMD_e)CMD->valueint)
        {
        case ScanCMD:
        {
            /* code */
            uint8_t FoundNum = DeviceScan(I2C_NUM_0, DeviceAddress, DEVICE_MAX_NUMBER);
            cJSON* Obj = cJSON_CreateObject();
            cJSON* addrArray = cJSON_CreateIntArray((int*)DeviceAddress, FoundNum);
            if(Obj == NULL)
            {
                continue;
            }
            cJSON_AddNumberToObject(Obj,"CMD",CMD->valueint);
            cJSON_AddNumberToObject(Obj,"Addr",Addr->valueint);
            cJSON_AddNumberToObject(Obj,"Reg",Reg->valueint);
            cJSON_AddNumberToObject(Obj,"Size",FoundNum);
            cJSON_AddItemToObject(Obj,"Value",addrArray);
            cJSON_AddItemToArray(send_root,Obj);
            break;
        }
        case ReadReg:
        {
            /* code */

            cJSON* RegValue = DeviceReadReg(I2C_NUM_0,Addr->valueint,Size->valueint,Reg->valueint);
            if(RegValue == NULL)
            {
                RegValue = cJSON_CreateArray();
            }
            cJSON* Obj = cJSON_CreateObject();
            if(Obj == NULL)
            {
                continue;
            }
            cJSON_AddNumberToObject(Obj,"CMD",CMD->valueint);
            cJSON_AddNumberToObject(Obj,"Addr",Addr->valueint);
            cJSON_AddNumberToObject(Obj,"Reg",Reg->valueint);
            cJSON_AddNumberToObject(Obj,"Size",Size->valueint);
            cJSON_AddItemToObject(Obj,"Value",RegValue);
            cJSON_AddItemToArray(send_root,Obj);
            break;
        }
        case WriteReg:
        {
            /* code */
            int Ret = DeviceWriteReg(I2C_NUM_0,Addr->valueint,Reg->valueint,Size->valueint,Value);
            cJSON* Obj = cJSON_CreateObject();
            cJSON* Arr = cJSON_CreateIntArray(&Ret,1);
            if(Obj == NULL)
            {
                continue;
            }
            cJSON_AddNumberToObject(Obj,"CMD",CMD->valueint);
            cJSON_AddNumberToObject(Obj,"Addr",Addr->valueint);
            cJSON_AddNumberToObject(Obj,"Reg",Reg->valueint);
            cJSON_AddNumberToObject(Obj,"Size",Size->valueint);
            cJSON_AddItemToObject(Obj,"Value",Arr);
            cJSON_AddItemToArray(send_root,Obj);
            break;
        }
        default:
        {
            continue;
        }
        }
    }

    if (cJSON_GetArraySize(send_root) != 0)
    {
        char* json_str = cJSON_PrintUnformatted(send_root);
        if(json_str == NULL)
        {
            ESP_LOGE(TAG, "Failed to create JSON object");  
            cJSON_Delete(root);
            cJSON_Delete(send_root);
            return NULL;
        }

        lwip_send(tcp_socket, json_str, strlen(json_str), 0);
        free(json_str);
    }

    cJSON_Delete(root);
    cJSON_Delete(send_root);
    return NULL;
}

static void* EXTIAction(void *arg)
{
    SetActionAddLock(&Command,false,0);
    i2c_driver_delete(I2C_NUM_0);
    user_gpio_init();
    lwip_send(tcp_socket, "y", 1, 0);
    return NULL;
}

void menu_i2c_tool(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}

/*
 * Address: 7Bit Address + 1Bit Write or Read
 * send data
 * ___________________________________________________________________________________________________
 * | start | slave_addr + wr_bit + ack | write reg_address + ack | write data_len byte + ack  | stop |
 * --------|---------------------------|-------------------------|----------------------------|------|
 * 
 * read data
 * 1. send reg address
 * ______________________________________________________________________
 * | start | slave_addr + wr_bit + ack | write reg_address + ack | stop |
 * --------|---------------------------|-------------------------|------|
 * 2. read data
 * ___________________________________________________________________________________
 * | start | slave_addr + wr_bit + ack | read data_len byte + ack(last nack)  | stop |
 * --------|---------------------------|--------------------------------------|------|
*/

static uint8_t DeviceScan(i2c_port_t Port,uint8_t DeviceGroup[], uint8_t Number)
{
    uint8_t found_count = 0;

    for (uint8_t addr = 0x00; addr <= 0x7F; addr++)
    {
        if (found_count >= Number)
        {
            break;
        }

        i2c_cmd_handle_t cmd = i2c_cmd_link_create();

        //start
        esp_err_t ret = i2c_master_start(cmd);
        if (ret != ESP_OK)
        {
            i2c_cmd_link_delete(cmd);
            continue;
        }

        //slave_addr + wr_bit + ack
        ret = i2c_master_write_byte(cmd, (addr << 1) | I2C_MASTER_WRITE, true);
        if (ret != ESP_OK)
        {
            i2c_cmd_link_delete(cmd);
            continue;
        }

        //stop
        ret = i2c_master_stop(cmd);
        if (ret != ESP_OK)
        {
            i2c_cmd_link_delete(cmd);
            continue;
        }

        ret = i2c_master_cmd_begin(Port, cmd, pdMS_TO_TICKS(100));
        i2c_cmd_link_delete(cmd);

        if (ret != ESP_OK)
        {
            continue;
        }

        DeviceGroup[found_count] = addr;
        found_count++;
    }

    return found_count;
}

static cJSON* DeviceReadReg(i2c_port_t Port, uint8_t Address, int Size, uint8_t Reg)
{
    if (Size <= 0)
    {
        return NULL;
    }

    cJSON* Array = cJSON_CreateArray();
    if(Array == NULL)
    {
        return NULL;
    }

    i2c_cmd_handle_t cmd = i2c_cmd_link_create();
    if (cmd == NULL)
    {
        return Array;
    }

    uint8_t* data = (uint8_t*)malloc(Size);
    if (data == NULL) {
        i2c_cmd_link_delete(cmd);
        return Array;
    }

    I2C_CHECK(i2c_master_start(cmd), "start");
    I2C_CHECK(i2c_master_write_byte(cmd, (Address << 1) | I2C_MASTER_WRITE, true), "write_addr");
    I2C_CHECK(i2c_master_write_byte(cmd, Reg, true), "write_reg");
    I2C_CHECK(i2c_master_start(cmd), "restart");
    I2C_CHECK(i2c_master_write_byte(cmd, (Address << 1) | I2C_MASTER_READ, true), "read_addr");

    if (Size > 1)
    {
        I2C_CHECK(i2c_master_read(cmd, data, Size - 1, I2C_MASTER_ACK), "read_data");
    }

    I2C_CHECK(i2c_master_read_byte(cmd, data + Size - 1, I2C_MASTER_LAST_NACK), "read_last");

    i2c_master_stop(cmd);
    
    esp_err_t ret = i2c_master_cmd_begin(Port, cmd, pdMS_TO_TICKS(100));

    i2c_cmd_link_delete(cmd);
    

    if (ret == ESP_OK)
    {
        for (int i = 0; i < Size; i++)
        {
            cJSON_AddItemToArray(Array, cJSON_CreateNumber(data[i]));
        }
    }
    // NULL由空数组替代
    // else
    // {
    //     cJSON_Delete(Array);
    //     Array = NULL;
    //     free(data);
    //     return cJSON_CreateNULL();
    // }

    free(data);
    return Array;


error:
    i2c_cmd_link_delete(cmd);
    free(data);
    return Array;
}

static int DeviceWriteReg(i2c_port_t Port, uint8_t Address, uint8_t Reg, int Length, cJSON* Value)
{
    if (Length <= 0)
    {
        return 11;
    }

    if(Value == NULL)
    {
        return 12;
    }

    if(!cJSON_IsArray(Value))
    {
        return 13;
    }

    int arraySize = cJSON_GetArraySize(Value);
    if (arraySize < Length)
    {
        return 14;
    }

    uint8_t* data = (uint8_t*)malloc(Length * sizeof(uint8_t));
    if (data == NULL)
    {
        return 15;
    }

    for(int i = 0; i < Length; i++)
    {
        cJSON* item = cJSON_GetArrayItem(Value, i);
        if (item == NULL || !cJSON_IsNumber(item))
        {
            free(data);
            return 16;
        }
        data[i] = (uint8_t)item->valueint;
    }

    i2c_cmd_handle_t cmd = i2c_cmd_link_create();
    if (cmd == NULL)
    {
        free(data);
        return 21;
    }

    esp_err_t ret = i2c_master_start(cmd);
    if (ret != ESP_OK)
    {
        ret = 22;
        goto cleanup;
    }

    ret = i2c_master_write_byte(cmd, (Address << 1) | I2C_MASTER_WRITE, true);
    if (ret != ESP_OK)
    { 
        ret = 23;
        goto cleanup;
    }

    ret = i2c_master_write_byte(cmd, Reg, true);
    if (ret != ESP_OK)
    {
        ret = 24;
        goto cleanup;
    }

    ret = i2c_master_write(cmd, data, Length, true);
    if (ret != ESP_OK)
    {
        ret = 25;
        goto cleanup;
    }

    ret = i2c_master_stop(cmd);
    if (ret != ESP_OK)
    {
        ret = 26;
        goto cleanup;
    }

    ret = i2c_master_cmd_begin(Port, cmd, pdMS_TO_TICKS(100));
    if(ret != ESP_OK)
    {
        ret = 27;
    }
 
cleanup:
    i2c_cmd_link_delete(cmd);
    free(data);
    return ret;
}


