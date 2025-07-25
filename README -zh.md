# 长不着弓工具箱 - ESP01S嵌入式无线调试工具

## 项目概述
**长不着弓工具箱**是一款基于ESP01S模块的开源嵌入式开发调试工具，解决了传统无线调试器价格昂贵、制作复杂的问题。通过无线TCP通信实现多种调试功能，大幅降低嵌入式开发调试门槛。

## 许可证
本项目采用 **BSD 3-Clause** 许可证。

## 程序下载链接
https://wwvs.lanzoue.com/b02u3fkyja
密码:arql

## 核心功能
- **网页配网**：AP模式快速配置WiFi参数
- **设备控制**：远程休眠、重启、参数配置
- **GPIO控制**：远程操作GPIO状态（PWM功能暂不可用）
- **OLED显示**：无线控制0.96英寸OLED
- **串口透传**：远程串口数据交互
- **STM32烧录**：无线刷写STM32固件
- **I2C通信**：远程I2C设备调试
- **TCP通信**：稳定可靠的数据传输

## 硬件要求
| 组件 | 规格 |
|------|------|
| 主控模块 | ESP01S |
| 烧录工具 | USB转TTL串口模块 (如CH340/CP2102) |
| 电源 | 3.3V/500mA稳定电源 |

## 开发环境搭建
# 克隆 ESP8266 RTOS SDK v3.4
```properties
git clone https://github.com/espressif/ESP8266_RTOS_SDK.git
```

## 配置项目
```properties
make menuconfig
```
### 设置路径: Component config → Log output → Default log verbosity → Info
### 设置输出: UART for console output → UART0

## 烧录指南
连接ESP01S到串口工具 (TX/RX/3V3/GND) 
使用乐鑫官方烧录工具 
建议先擦除Flash后再进行烧录 
关键烧录设置： 
Flash Size: 根据你的模块Flash大小选择，市面通常1MB 
Flash Mode: QIO 
SPI SPEED: 40MHz 
波特率: ≤1Mbps 

## 使用说明
对于大多数人来说，建议使用打包好的工具：   
首次配置或超过1分钟无法连接WIFI模块进入AP模式  
连接WiFi: CBZG_Tool_AP  
访问配置页面: http://192.168.4.1  
填写配置信息：  
WiFi SSID和密码（仅支持2.4GHz网络）  
服务器IP和端口  

## 项目结构
├── main/                   # 主应用程序  
├── user_components/        # 公共库  
│   ├── user_wifi_sta_init/ # WiFi连接  
│   ├── user_web_config/    # 网页配网  
│   └── user_flash_rw/      # NVS保存  
│   ...  
├── module_components/      # 功能模块  
│   ├── gpio_manager/       # GPIO控制  
│   ├── SSD1306/            # OLED调试  
│   └── I2C_Tool/           # I2C调试  
│   ...  
└── build/                  # 编译输出  

## 故障排除
### 无法连接WiFi
1.确认SSID/密码正确  
2.确保为2.4GHz网络  
3.检查信号强度  
### PWM功能异常
PWM功能暂时无法使用，原因未知，查询多方资料，查看例程无果  
### 上位机可能存在的内存泄漏
正在查明原因  
### 其他问题请告知我

## 开发计划
1.修复GPIO PWM功能  
2.增加更多模块  
3.优化程序，增强稳定性  
