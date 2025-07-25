# ChangBuZhangGong Toolbox - ESP01S Embedded Wireless Debugging Tool  

## Project Overview  
**ChangBuZhangGong Toolbox** is an open-source embedded development and debugging tool based on the ESP01S module. It solves the problems of expensive traditional wireless debuggers and complex manufacturing processes. Through wireless TCP communication, it enables multiple debugging functions, significantly lowering the barrier to embedded development.  

## License  
This project is licensed under the **BSD 3-Clause** License.  

## Firmware Download Link  
[https://wwvs.lanzoue.com/ivpft31scg1c]  
Password: arql  

## Core Features  
- **Web-based Network Configuration**: Quickly configure WiFi parameters in AP mode  
- **Device Control**: Remote sleep, reboot, and parameter configuration  
- **GPIO Control**: Remote GPIO status control (PWM unavailable temporarily)  
- **OLED Display**: Wireless control of 0.96-inch OLED screens  
- **Serial Passthrough**: Remote serial data interaction  
- **STM32 Programming**: Wireless firmware flashing for STM32  
- **I2C Communication**: Remote I2C device debugging  
- **TCP Communication**: Stable and reliable data transmission  

## Hardware Requirements  
| Component | Specification |  
|-----------|---------------|  
| Main Module | ESP01S |  
| Programmer | USB-to-TTL Serial Module (e.g. CH340/CP2102) |  
| Power Supply | 3.3V/500mA stable power source |  

## Development Environment Setup  
```properties  
# Clone ESP8266 RTOS SDK v3.4  
git clone https://github.com/espressif/ESP8266_RTOS_SDK.git  

```properties
make menuconfig
```

### Set Path: Component config → Log output → Default log verbosity → Info
### Set Output: UART for console output → UART0

## Flashing Guide
1. Connect ESP01S to serial tool (TX/RX/3V3/GND)  
2. Use Espressif's official flashing tool  
3 .Recommended to erase Flash before flashing  
Critical settings:  
5. Flash Size: Select according to module (typically 1MB)  
5.1 Flash Mode: QIO  
5.2 SPI SPEED: 40MHz  
5.3 Baud Rate: ≤1Mbps  

## Usage Instructions
For most users, pre-built firmware is recommended:  
Module enters AP mode automatically if no WiFi connection within 1 minute  
Connect to WiFi: CBZG_Tool_AP  
Access configuration page: http://192.168.4.1  
### Configure:  
WiFi SSID/Password (2.4GHz only)  
Server IP and Port  

## Project Structure  
├── main/ # Main application  
├── user_components/ # Shared libraries  
│ ├── user_wifi_sta_init/ # WiFi connection  
│ ├── user_web_config/ # Web-based configuration  
│ └── user_flash_rw/ # NVS storage  
│ ...  
├── module_components/ # Functional modules  
│ ├── gpio_manager/ # GPIO control  
│ ├── SSD1306/ # OLED debugging  
│ └── I2C_Tool/ # I2C debugging  
│ ...  
└── build/ # Compilation output  

## Troubleshooting
###WiFi Connection Failure  
Confirm correct SSID/password  
Ensure 2.4GHz network  
Check signal strength  

### PWM Function Abnormal
PWM currently unavailable (cause unknown despite research)  

### Potential Memory Leak in Host Software
Under investigation  
Report Other Issues  

### Development Roadmap
Fix GPIO PWM functionality  
Add more modules  
Optimize program stability  
