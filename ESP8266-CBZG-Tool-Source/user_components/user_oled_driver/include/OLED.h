#ifndef __OLED_H
#define __OLED_H


/*函数声明*********************/
/*初始化函数*/
extern void OLED_Init(void);
extern void OLED_Uninstall(void);

/*更新函数*/
extern void OLED_Update(void);
extern void OLED_UpdateArea(uint8_t X, uint8_t Y, uint8_t Width, uint8_t Height);
extern void OLED_UploadScreen(uint8_t* Data);

/*显存控制函数*/
extern void OLED_Clear(void);
extern void OLED_ClearArea(uint8_t X, uint8_t Y, uint8_t Width, uint8_t Height);
extern void OLED_Printf(uint8_t X, uint8_t Y, char *format, ...);
/*********************函数声明*/

#endif


/*****************江协科技|版权所有****************/
/*****************jiangxiekeji.com*****************/
