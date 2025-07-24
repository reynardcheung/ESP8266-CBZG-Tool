#include "user_gpio_init.h"
#include <stdint.h>
#include <stdbool.h>
#include "driver/gpio.h"
#include "driver/i2c.h"
#include "OLED.h"
#include "OLED_Data.h"
#include <string.h>
#include <stdio.h>
#include <stdarg.h>
#include "FreeRTOS.h"
#include "task.h"

/**
  * 数据存储格式：
  * 纵向8点，高位在下，先从左到右，再从上到下
  * 每一个Bit对应一个像素点
  * 
  *      B0 B0                  B0 B0
  *      B1 B1                  B1 B1
  *      B2 B2                  B2 B2
  *      B3 B3  ------------->  B3 B3 --
  *      B4 B4                  B4 B4  |
  *      B5 B5                  B5 B5  |
  *      B6 B6                  B6 B6  |
  *      B7 B7                  B7 B7  |
  *                                    |
  *  -----------------------------------
  *  |   
  *  |   B0 B0                  B0 B0
  *  |   B1 B1                  B1 B1
  *  |   B2 B2                  B2 B2
  *  --> B3 B3  ------------->  B3 B3
  *      B4 B4                  B4 B4
  *      B5 B5                  B5 B5
  *      B6 B6                  B6 B6
  *      B7 B7                  B7 B7
  * 
  * 坐标轴定义：
  * 左上角为(0, 0)点
  * 横向向右为X轴，取值范围：0~127
  * 纵向向下为Y轴，取值范围：0~63
  * 
  *       0             X轴           127 
  *      .------------------------------->
  *    0 |
  *      |
  *      |
  *      |
  *  Y轴 |
  *      |
  *      |
  *      |
  *   63 |
  *      v
  * 
  */


#define ERR_I2C_CHECK(x) if(x==false){return false;}

/*全局变量*********************/

/**
  * OLED显存数组
  * 所有的显示函数，都只是对此显存数组进行读写
  * 随后调用OLED_Update函数或OLED_UpdateArea函数
  * 才会将显存数组的数据发送到OLED硬件，进行显示
  */
uint8_t OLED_DisplayBuf[8][128];

/*********************全局变量*/

static void I2C_Init(void)
{
    i2c_config_t conf = {
        .mode = I2C_MODE_MASTER,
        .sda_io_num = GPIO_NUM_0,
        .scl_io_num = GPIO_NUM_2,
        .sda_pullup_en = 1,
        .scl_pullup_en = 1,
        .clk_stretch_tick = 300
    };
    ESP_ERROR_CHECK(i2c_driver_install(I2C_NUM_0, conf.mode));
    ESP_ERROR_CHECK(i2c_param_config(I2C_NUM_0, &conf));
}

static esp_err_t i2c_master_ssd1306_write(i2c_port_t i2c_num,
	uint8_t device_address,
	uint8_t reg_address,
	uint8_t *data,
	size_t data_len,
	uint8_t check
)
{
    int ret;
    i2c_cmd_handle_t cmd = i2c_cmd_link_create();
    i2c_master_start(cmd);
    i2c_master_write_byte(cmd, device_address << 1 | I2C_MASTER_WRITE, check);
    i2c_master_write_byte(cmd, reg_address, check);
    i2c_master_write(cmd, data, data_len, check);
    i2c_master_stop(cmd);
    ret = i2c_master_cmd_begin(i2c_num, cmd, 1000 / portTICK_RATE_MS);
    i2c_cmd_link_delete(cmd);
    return ret;
}

/**
  * 函    数：OLED引脚初始化
  * 参    数：无
  * 返 回 值：无
  * 说    明：当上层函数需要初始化时，此函数会被调用
  *           用户需要将SCL和SDA引脚初始化为开漏模式，并释放引脚
  */
void OLED_GPIO_Init(void)
{
  I2C_Init();
}


// 定义OLED常量
#define OLED_ADDR        0x3C       // 7位I2C地址
#define OLED_CMD_MODE    0x00       // 命令模式
#define OLED_DATA_MODE   0x40       // 数据模式

bool OLED_I2C_SendByte(uint8_t mode, uint8_t* data, size_t length)
{
    return !i2c_master_ssd1306_write(I2C_NUM_0, OLED_ADDR, mode, data, length, 0);
}

bool OLED_WriteCommand(uint8_t* Command,size_t Length)
{
    return OLED_I2C_SendByte(OLED_CMD_MODE, Command, Length);
}

bool OLED_WriteData(uint8_t *Data, size_t Count)
{
    return OLED_I2C_SendByte(OLED_DATA_MODE, Data, Count);
}

/*硬件配置*********************/

/**
  * 函    数：OLED初始化
  * 参    数：无
  * 返 回 值：无
  * 说    明：使用前，需要调用此初始化函数
  */
void OLED_Init(void)
{
    OLED_GPIO_Init();  // 先调用底层的端口初始化
    
    // 合并所有初始化命令到单个数组
    uint8_t init_commands[] = {
        // 设置显示开启/关闭
        0xAE,   // 关闭显示
        
        // 设置显示时钟分频比/振荡器频率
        0xD5, 0xF0, // 0x00~0xFF
        
        // 设置多路复用率
        0xA8, 0x3F, // 0x0E~0x3F
        
        // 设置显示偏移
        0xD3, 0x00, // 0x00~0x7F
        
        // 设置显示开始行
        0x40,       // 0x40~0x7F
        
        // 设置左右方向
        0xA1,       // 0xA1正常，0xA0左右反置
        
        // 设置上下方向
        0xC8,       // 0xC8正常，0xC0上下反置
        
        // 设置COM引脚硬件配置
        0xDA, 0x12,
        
        // 设置对比度
        0x81, 0xCF, // 0x00~0xFF
        
        // 设置预充电周期
        0xD9, 0xF1,
        
        // 设置VCOMH取消选择级别
        0xDB, 0x30,
        
        // 设置整个显示打开/关闭
        0xA4,       // 0xA4正常显示，0xA5全亮
        
        // 设置正常/反色显示
        0xA6,       // 0xA6正常，0xA7反色
        
        // 设置充电泵
        0x8D, 0x14,
        
        // 开启显示
        0xAF
    };
    
    // 一次性发送所有初始化命令
    OLED_WriteCommand(init_commands, sizeof(init_commands));
}

void OLED_Uninstall(void)
{
	i2c_driver_delete(I2C_NUM_0);
}


/**
  * 函    数：OLED设置显示光标位置
  * 参    数：Page 指定光标所在的页，范围：0~7
  * 参    数：X 指定光标所在的X轴坐标，范围：0~127
  * 返 回 值：无
  * 说    明：OLED默认的Y轴，只能8个Bit为一组写入，即1页等于8个Y轴坐标
  */
void OLED_SetCursor(uint8_t Page, uint8_t X)
{
    /* 如果使用此程序驱动1.3寸的OLED显示屏，则需要解除此注释 */
    /* 因为1.3寸的OLED驱动芯片（SH1106）有132列 */
    /* 屏幕的起始列接在了第2列，而不是第0列 */
    /* 所以需要将X加2，才能正常显示 */
    // X += 2;
    
    /* 合并所有光标设置命令到单个数组 */
    uint8_t cursor_commands[] = {
        0xB0 | Page,                 // 设置页位置
        0x10 | ((X & 0xF0) >> 4),    // 设置X位置高4位
        0x00 | (X & 0x0F)            // 设置X位置低4位
    };
    
    /* 一次性发送所有光标设置命令 */
    OLED_WriteCommand(cursor_commands, sizeof(cursor_commands));
}
/*********************硬件配置*/

/*功能函数*********************/

/**
  * 函    数：将OLED显存数组更新到OLED屏幕
  * 参    数：无
  * 返 回 值：无
  * 说    明：所有的显示函数，都只是对OLED显存数组进行读写
  *           随后调用OLED_Update函数或OLED_UpdateArea函数
  *           才会将显存数组的数据发送到OLED硬件，进行显示
  *           故调用显示函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_Update(void)
{
	uint8_t j;
	/*遍历每一页*/
	for (j = 0; j < 8; j ++)
	{
		/*设置光标位置为每一页的第一列*/
		OLED_SetCursor(j, 0);
		/*连续写入128个数据，将显存数组的数据写入到OLED硬件*/
		OLED_WriteData(OLED_DisplayBuf[j], 128);
	}
}

/**
  * 函    数：将OLED显存数组部分更新到OLED屏幕
  * 参    数：X 指定区域左上角的横坐标，范围：0~127
  * 参    数：Y 指定区域左上角的纵坐标，范围：0~63
  * 参    数：Width 指定区域的宽度，范围：0~128
  * 参    数：Height 指定区域的高度，范围：0~64
  * 返 回 值：无
  * 说    明：此函数会至少更新参数指定的区域
  *           如果更新区域Y轴只包含部分页，则同一页的剩余部分会跟随一起更新
  * 说    明：所有的显示函数，都只是对OLED显存数组进行读写
  *           随后调用OLED_Update函数或OLED_UpdateArea函数
  *           才会将显存数组的数据发送到OLED硬件，进行显示
  *           故调用显示函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_UpdateArea(uint8_t X, uint8_t Y, uint8_t Width, uint8_t Height)
{
	uint8_t j;
	
	/*参数检查，保证指定区域不会超出屏幕范围*/
	if (X > 127) {return;}
	if (Y > 63) {return;}
	if (X + Width > 128) {Width = 128 - X;}
	if (Y + Height > 64) {Height = 64 - Y;}
	
	/*遍历指定区域涉及的相关页*/
	/*(Y + Height - 1) / 8 + 1的目的是(Y + Height) / 8并向上取整*/
	for (j = Y / 8; j < (Y + Height - 1) / 8 + 1; j ++)
	{
		/*设置光标位置为相关页的指定列*/
		OLED_SetCursor(j, X);
		/*连续写入Width个数据，将显存数组的数据写入到OLED硬件*/
		OLED_WriteData(&OLED_DisplayBuf[j][X], Width);
	}
}

void OLED_UploadScreen(uint8_t* Data)
{
  for (uint8_t page = 0; page < 8; page++)
  { // 遍历8页
    OLED_SetCursor(page, 0); // 设置页地址 (列固定从0开始)
    OLED_WriteData(&Data[page * 128], 128); // 写入当前页的128字节
  }
}

/**
  * 函    数：将OLED显存数组全部清零
  * 参    数：无
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_Clear(void)
{
	uint8_t i, j;
	for (j = 0; j < 8; j ++)				//遍历8页
	{
		for (i = 0; i < 128; i ++)			//遍历128列
		{
			OLED_DisplayBuf[j][i] = 0x00;	//将显存数组数据全部清零
		}
	}
}

/**
  * 函    数：将OLED显存数组部分清零
  * 参    数：X 指定区域左上角的横坐标，范围：0~127
  * 参    数：Y 指定区域左上角的纵坐标，范围：0~63
  * 参    数：Width 指定区域的宽度，范围：0~128
  * 参    数：Height 指定区域的高度，范围：0~64
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_ClearArea(uint8_t X, uint8_t Y, uint8_t Width, uint8_t Height)
{
	uint8_t i, j;
	
	/*参数检查，保证指定区域不会超出屏幕范围*/
	if (X > 127) {return;}
	if (Y > 63) {return;}
	if (X + Width > 128) {Width = 128 - X;}
	if (Y + Height > 64) {Height = 64 - Y;}
	
	for (j = Y; j < Y + Height; j ++)		//遍历指定页
	{
		for (i = X; i < X + Width; i ++)	//遍历指定列
		{
			OLED_DisplayBuf[j / 8][i] &= ~(0x01 << (j % 8));	//将显存数组指定数据清零
		}
	}
}

/**
  * 函    数：OLED显示图像
  * 参    数：X 指定图像左上角的横坐标，范围：0~127
  * 参    数：Y 指定图像左上角的纵坐标，范围：0~63
  * 参    数：Width 指定图像的宽度，范围：0~128
  * 参    数：Height 指定图像的高度，范围：0~64
  * 参    数：Image 指定要显示的图像
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_ShowImage(uint8_t X, uint8_t Y, uint8_t Width, uint8_t Height, const uint8_t *Image)
{
	uint8_t i, j;
	
	/*参数检查，保证指定图像不会超出屏幕范围*/
	if (X > 127) {return;}
	if (Y > 63) {return;}
	
	/*将图像所在区域清空*/
	OLED_ClearArea(X, Y, Width, Height);
	
	/*遍历指定图像涉及的相关页*/
	/*(Height - 1) / 8 + 1的目的是Height / 8并向上取整*/
	for (j = 0; j < (Height - 1) / 8 + 1; j ++)
	{
		/*遍历指定图像涉及的相关列*/
		for (i = 0; i < Width; i ++)
		{
			/*超出边界，则跳过显示*/
			if (X + i > 127) {break;}
			if (Y / 8 + j > 7) {return;}
			
			/*显示图像在当前页的内容*/
			OLED_DisplayBuf[Y / 8 + j][X + i] |= Image[j * Width + i] << (Y % 8);
			
			/*超出边界，则跳过显示*/
			/*使用continue的目的是，下一页超出边界时，上一页的后续内容还需要继续显示*/
			if (Y / 8 + j + 1 > 7) {continue;}
			
			/*显示图像在下一页的内容*/
			OLED_DisplayBuf[Y / 8 + j + 1][X + i] |= Image[j * Width + i] >> (8 - Y % 8);
		}
	}
}

/**
  * 函    数：OLED显示一个字符
  * 参    数：X 指定字符左上角的横坐标，范围：0~127
  * 参    数：Y 指定字符左上角的纵坐标，范围：0~63
  * 参    数：Char 指定要显示的字符，范围：ASCII码可见字符
  * 参    数：FontSize 指定字体大小
  *           范围：OLED_6X8		宽6像素，高8像素
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_ShowChar(uint8_t X, uint8_t Y, char Char)
{
	/*将ASCII字模库OLED_F6x8的指定数据以6*8的图像格式显示*/
	OLED_ShowImage(X, Y, 6, 8, OLED_F6x8[Char - ' ']);
}

/**
  * 函    数：OLED显示字符串
  * 参    数：X 指定字符串左上角的横坐标，范围：0~127
  * 参    数：Y 指定字符串左上角的纵坐标，范围：0~63
  * 参    数：String 指定要显示的字符串，范围：ASCII码可见字符组成的字符串
  * 参    数：FontSize 指定字体大小
  *           范围：OLED_6X8		宽6像素，高8像素
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_ShowString(uint8_t X, uint8_t Y, char *String)
{
	uint8_t i;
	for (i = 0; String[i] != '\0'; i++)		//遍历字符串的每个字符
	{
		/*调用OLED_ShowChar函数，依次显示每个字符*/
		OLED_ShowChar(X + i * 6, Y, String[i]);
	}
}

/**
  * 函    数：OLED使用printf函数打印格式化字符串
  * 参    数：X 指定格式化字符串左上角的横坐标，范围：0~127
  * 参    数：Y 指定格式化字符串左上角的纵坐标，范围：0~63
  * 参    数：FontSize 指定字体大小
  *           范围：OLED_8X16		宽8像素，高16像素
  *                 OLED_6X8		宽6像素，高8像素
  * 参    数：format 指定要显示的格式化字符串，范围：ASCII码可见字符组成的字符串
  * 参    数：... 格式化字符串参数列表
  * 返 回 值：无
  * 说    明：调用此函数后，要想真正地呈现在屏幕上，还需调用更新函数
  */
void OLED_Printf(uint8_t X, uint8_t Y, char *format, ...)
{
	char String[30];						//定义字符数组
	va_list arg;							//定义可变参数列表数据类型的变量arg
	va_start(arg, format);					//从format开始，接收参数列表到arg变量
	vsprintf(String, format, arg);			//使用vsprintf打印格式化字符串和参数列表到字符数组中
	va_end(arg);							//结束变量arg
	OLED_ShowString(X, Y, String);//OLED显示字符数组（字符串）
}
/*****************江协科技|版权所有****************/
/*****************jiangxiekeji.com*****************/
