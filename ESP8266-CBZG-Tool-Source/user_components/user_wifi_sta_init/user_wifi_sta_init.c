#include "user_wifi_sta_init.h"

#define ESP_WIFI_SSID       globe_user_config.wifi_config.ssid
#define ESP_WIFI_PASSWORD   globe_user_config.wifi_config.password
#define ESP_WIFI_CHANNEL    globe_user_config.wifi_config.channel
#define ESP_MAXIMUM_RETRY   CONFIG_WIFI_MAX_CONNECT_RETRY

static EventGroupHandle_t s_wifi_event_group;
static bool* WIFI_CONNECT_FLAG = NULL;

#define WIFI_CONNECTED_BIT BIT0
#define WIFI_FAIL_BIT      BIT1

static const char *TAG = "wifi station";



static void event_handler(void* arg, esp_event_base_t event_base,
                                int32_t event_id, void* event_data)
{
    static int s_retry_num = 0;
    ESP_LOGI(TAG, "Connect Times:%d", s_retry_num);
    if (event_base == WIFI_EVENT && event_id == WIFI_EVENT_STA_START)
    {
        esp_wifi_connect();
    }
    else if (event_base == WIFI_EVENT && event_id == WIFI_EVENT_STA_DISCONNECTED)
    {
        if (s_retry_num < ESP_MAXIMUM_RETRY)
        {
            esp_wifi_connect();
            s_retry_num++;
            ESP_LOGI(TAG, "retry to connect to the AP");
        }
        else
        {
            xEventGroupSetBits(s_wifi_event_group, WIFI_FAIL_BIT);
            start_web_config();
        }
        ESP_LOGI(TAG,"connect to the AP fail");
    }
    else if (event_base == IP_EVENT && event_id == IP_EVENT_STA_GOT_IP)
    {
        ip_event_got_ip_t* event = (ip_event_got_ip_t*) event_data;
        ESP_LOGI(TAG, "got ip:%s",
                ip4addr_ntoa(&event->ip_info.ip));
        s_retry_num = 0;
        *WIFI_CONNECT_FLAG = true;
        xEventGroupSetBits(s_wifi_event_group, WIFI_CONNECTED_BIT);
    }
} 

void wifi_init_sta(bool* FLAG)
{
    WIFI_CONNECT_FLAG = FLAG;
    s_wifi_event_group = xEventGroupCreate();

    ESP_ERROR_CHECK(esp_event_loop_create_default());

    wifi_init_config_t cfg = WIFI_INIT_CONFIG_DEFAULT();
    ESP_ERROR_CHECK(esp_wifi_init(&cfg));

    esp_wifi_set_ps(WIFI_PS_NONE);

    ESP_ERROR_CHECK(esp_event_handler_register(WIFI_EVENT, ESP_EVENT_ANY_ID, &event_handler, NULL));
    ESP_ERROR_CHECK(esp_event_handler_register(IP_EVENT, IP_EVENT_STA_GOT_IP, &event_handler, NULL));

    wifi_config_t wifi_config;

    memset(&wifi_config,0,sizeof(wifi_config_t));
    memcpy(wifi_config.sta.ssid,ESP_WIFI_SSID,sizeof(wifi_config.sta.ssid));
    memcpy(wifi_config.sta.password,ESP_WIFI_PASSWORD,sizeof(wifi_config.sta.password));
    wifi_config.sta.channel = ESP_WIFI_CHANNEL;
    
    if (strlen((char *)wifi_config.sta.password)) 
    {
        wifi_config.sta.threshold.authmode = WIFI_AUTH_WPA2_PSK;
    }
    //esp_wifi_set_promiscuous_rx_cb
    ESP_ERROR_CHECK(esp_wifi_set_mode(WIFI_MODE_STA));
    ESP_ERROR_CHECK(esp_wifi_set_config(ESP_IF_WIFI_STA, &wifi_config));
    ESP_ERROR_CHECK(esp_wifi_set_bandwidth(ESP_IF_WIFI_STA,WIFI_BW_HT40));
    ESP_ERROR_CHECK(esp_wifi_start());
    ESP_LOGI(TAG, "wifi_init_sta finished.");
    
    while(esp_wifi_connect() != ESP_OK);

    EventBits_t bits = xEventGroupWaitBits(
            s_wifi_event_group,
            WIFI_CONNECTED_BIT | WIFI_FAIL_BIT,
            pdFALSE,
            pdFALSE,
            portMAX_DELAY
            );

    if (bits & WIFI_CONNECTED_BIT)
    {
        ESP_LOGI(TAG, "connected to ap SSID:%s password:%s",
                 ESP_WIFI_SSID, ESP_WIFI_PASSWORD);
    }
    else if (bits & WIFI_FAIL_BIT)
    {
        ESP_LOGI(TAG, "Failed to connect to SSID:%s, password:%s",
                 ESP_WIFI_SSID, ESP_WIFI_PASSWORD);
    }
    else
    {
        ESP_LOGE(TAG, "UNEXPECTED EVENT");
        esp_restart();
    }

    //ESP_ERROR_CHECK(esp_event_handler_unregister(IP_EVENT, IP_EVENT_STA_GOT_IP, &event_handler));
    //ESP_ERROR_CHECK(esp_event_handler_unregister(WIFI_EVENT, ESP_EVENT_ANY_ID, &event_handler));
    //vEventGroupDelete(s_wifi_event_group);
}