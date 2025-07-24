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
#include "get_module.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "ESPGETMODULE";
static char CMD_NAME[] = CONFIG_ESP_GET_MODULE_CMD_NAME;
static char NAME[] = CONFIG_ESP_GET_MODULE_MODULE_NAME;
static char BRIEF[] = CONFIG_ESP_GET_MODULE_BRIEF;
static char VERSION[] = CONFIG_ESP_GET_MODULE_VERSION;
#ifndef CONFIG_ESP_GET_MODULE_RUNNING
#define CONFIG_ESP_GET_MODULE_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_ESP_GET_MODULE_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};

static char NullText[] = "NULL";

typedef struct {
    char* Name;
    char* Brief;
    char* ModuleVersion;
    char* CommandName;
} ModuleInfo;

typedef struct {
    ModuleInfo* modules;
    int count;
} ModuleList;

ModuleList create_module_list()
{
    ModuleList list;
    list.count = CommandCount;
    list.modules = (ModuleInfo*)malloc(sizeof(ModuleInfo) * list.count);

    if (list.modules == NULL)
    {  
        ESP_LOGE(TAG, "Memory allocation failed for module list");  
        return list;
    }  

    for(int index = 0;index  < list.count;index++)
    {
        if(CommandMenu[index]->Name != NULL)
        {
            list.modules[index].Name = CommandMenu[index]->Name;
        }
        else
        {
            list.modules[index].Name = NullText;
        }
        if(CommandMenu[index]->Brief != NULL)
        {
            list.modules[index].Brief = CommandMenu[index]->Brief;
        }
        else
        {
            list.modules[index].Brief = NullText;
        }

        list.modules[index].ModuleVersion = CommandMenu[index]->ModuleVersion;

        if(CommandMenu[index]->CommandName != NULL)
        {
            list.modules[index].CommandName = CommandMenu[index]->CommandName;
        }
        else
        {
            list.modules[index].CommandName = NullText;
        }
    }

    return list;
}

cJSON* modules_to_json(const ModuleList* list)
{
    cJSON* root = cJSON_CreateArray();
    
    for (int i = 0; i < list->count; i++)
    {
        cJSON* module = cJSON_CreateObject();
        if (module == NULL)
        {  
            ESP_LOGE(TAG, "Failed to create JSON object");  
            return NULL;
        }

        const char* name = list->modules[i].Name ? list->modules[i].Name : "NULL";  
        const char* brief = list->modules[i].Brief ? list->modules[i].Brief : "NULL";  
        const char* moduleVersion = list->modules[i].ModuleVersion ? list->modules[i].ModuleVersion : "NULL";
        const char* commandName = list->modules[i].CommandName ? list->modules[i].CommandName : "NULL";  
        cJSON_AddStringToObject(module, "Name", name);  
        cJSON_AddStringToObject(module, "Brief", brief);  
        cJSON_AddStringToObject(module, "ModuleVersion", moduleVersion);  
        cJSON_AddStringToObject(module, "CommandName", commandName);  

        cJSON_AddItemToArray(root, module);
    }
    
    return root;
}

void free_module_list(ModuleList* list)
{
    if (list->modules != NULL)
    {
        free(list->modules);
        list->modules = NULL;
        list->count = 0;
    }
}

static void* InitAction(void *arg)
{
    ModuleList Modules = create_module_list();
    cJSON* json = modules_to_json(&Modules);
    char* json_str = cJSON_PrintUnformatted(json);

    if(json_str == NULL)
    {
        lwip_send(tcp_socket,"NULL",4, 0);
    }

    lwip_send(tcp_socket, json_str, strlen(json_str), 0);
    free_module_list(&Modules);
    cJSON_Delete(json);
    free(json_str);
    return NULL;
}

// static void* Action(void *arg)
// {
//     return NULL;
// }

// static void* EXTIAction(void *arg)
// {
//     return NULL;
// }


void menu_esp_get_module(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Command add err");
    }
}




