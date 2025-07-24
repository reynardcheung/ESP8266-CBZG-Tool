#include "user_flash_rw.h"
#include "esp_err.h"

user_config_t default_user_config =
{
    .uart_config = {
        .baud_rate           = (int)CONFIG_UART0_BAUD_RATE,
        .data_bits           = (uart_word_length_t )CONFIG_UART0_DATA_BITS,
        .parity              = (uart_parity_t)CONFIG_UART0_PARITY,
        .stop_bits           = (uart_stop_bits_t)CONFIG_UART0_STOP_BITS,
        .flow_ctrl           = UART_HW_FLOWCTRL_DISABLE,
        .rx_flow_ctrl_thresh = 0,
    },
    .wifi_config = {
    .ssid = CONFIG_WIFI_SSID,
    .password = CONFIG_WIFI_PASSWORD,
#ifdef CONFIG_IPV4
    .ip = CONFIG_IPV4_ADDR,
#elif CONFIG_IPV6
    .ip = CONFIG_IPV6_ADDR,
#endif
    .port = CONFIG_PORT,
    .udp_port = CONFIG_UDP_PORT,
    .channel = CONFIG_WIFI_CHANNEL
    },
    .system_config = {
        .Author = "CBZG"
    }
};

user_config_t globe_user_config;

void ICACHE_FLASH_ATTR esp_flash_rw_init(bool* FLAG)
{
    ESP_ERROR_CHECK(nvs_flash_init());
    if(!read_config())
    {
        *FLAG = false;
    }
    else
    {
        *FLAG = true;
    }
}

void ICACHE_FLASH_ATTR reset_config(void)
{
    save_config(&default_user_config);
    read_config();
    ESP_LOGI("CONFIG","Reboot Device To Reset Config");
    vTaskSuspendAll();
}

bool ICACHE_FLASH_ATTR read_config()
{  
    user_config_t *config = &globe_user_config;  
    nvs_handle_t handle;  
    size_t required_size;  
    
    esp_err_t err = nvs_open("storage", NVS_READONLY, &handle);  
    if (err != ESP_OK)
    {
        return false;   
    }  

    err = nvs_get_blob(handle, "user_config", NULL, &required_size);  
    if (err != ESP_OK)
    {  
        nvs_close(handle);
        return false;   
    }  

    if (required_size == sizeof(user_config_t))
    {  
        err = nvs_get_blob(handle, "user_config", config, &required_size);  
        if (err != ESP_OK)
        {  
            nvs_close(handle);  
            return false;   
        }  
    }
    else
    {  
        nvs_close(handle);  
        return false;  
    }  
    
    nvs_close(handle);   
    return true;   
}  

bool ICACHE_FLASH_ATTR save_config(const user_config_t *config)
{  
    nvs_handle_t handle;  
    esp_err_t err = nvs_open("storage", NVS_READWRITE, &handle);  
    
    if (err != ESP_OK)
    {  
        return false;   
    }  

    err = nvs_set_blob(handle, "user_config", config, sizeof(user_config_t));  
    if (err != ESP_OK)
    {  
        nvs_close(handle);  
        return false;   
    }  

    err = nvs_commit(handle);  
    nvs_close(handle);  
    return (err == ESP_OK);  
}  