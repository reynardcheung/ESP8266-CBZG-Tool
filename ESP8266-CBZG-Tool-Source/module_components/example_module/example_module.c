/*
下面为编写模块的例子
步骤：
1.按照该文件夹组织文件结构
2.编写Kconfig
3.将Kconfig的路径写入module_components的Kconfig.projbuild文件中
4.添加本文件夹和include文件夹进入本项目根目录的Makefile文件和CMakeLists.txt中
5.终端使用make menuconfig打开调节参数并保存
6.编写代码
7.将类似于example_menu_init的函数添加入main文件夹的CBZG_Tool.c的menu_init函数内
*/


/*
#include "example_module.h"

static void* InitAction(void *arg);
// static void* Action(void *arg);
// static void* EXTIAction(void *arg);

static char* TAG = "EXAMPLE";
static char NAME[] = CONFIG_EXAMPLE_MODULE_NAME;
static char BRIEF[] = CONFIG_EXAMPLE_BRIEF;
static char VERSION[] = CONFIG_EXAMPLE_VERSION;
static char CMD_NAME[] = CONFIG_EXAMPLE_CMD_NAME;
static uint8_t DeviceID[27] = {0};
#ifndef CONFIG_EXAMPLE_RUNNING
#define CONFIG_EXAMPLE_RUNNING false
#endif

static Command_t Command = {
    .Name = NAME,
    .Brief = BRIEF,
    .ModuleVersion = VERSION,
    .CommandName = CMD_NAME,
    .Running = CONFIG_EXAMPLE_RUNNING,
    .InitAction = InitAction,
    .Action = NULL,
    .ExtiAction = NULL
};


int count = 0;
static void* InitAction(void *arg)
{
    uart_write_bytes(UART_NUM_0,"TestInit\n",9);
    return NULL;
}

static void* Action(void *arg)
{
    count++;
    
    uart_write_bytes(UART_NUM_0,"TestAction\n",11);
    RunningActionDeleteSelf(&Command,strlen(Command.CommandName));
    return NULL;
}

static void* EXTIAction(void *arg)
{
    count = 0;
    uart_write_bytes(UART_NUM_0,"TestEXTIAction\n",15);
    return NULL;
}


void example_menu_init(void)
{
    if(CommandMenuAdd(&Command))
    {

    }
    else
    {
        ESP_LOGE(TAG,"Test Create Failed");
    }
}

*/
