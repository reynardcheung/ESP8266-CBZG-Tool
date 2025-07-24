#include "user_heart_beat.h"

esp_timer_handle_t esp_timer_handle_struct;

static uint8_t ping_timeout_count = 0;
static EventGroupHandle_t s_ping_event_group;
static const int PING_DONE_BIT = BIT0;

void heart_beat_reset(void)
{
    #if CONFIG_HEART_BEAT_ENABLE
    esp_timer_stop(esp_timer_handle_struct);
    esp_err_t err = esp_timer_start_periodic(esp_timer_handle_struct,1000000 * CONFIG_HEART_BEAT_TIME);
    if (err != ESP_OK)
    {  
        force_restart("HeartBeat_Init");
    }
    #endif
}

void heart_beat_enable(bool enabled)
{
    if(enabled)
    {
        heart_beat_reset();
    }
    else
    {
        esp_timer_stop(esp_timer_handle_struct);
    }
}

void heart_beat_ping_success(esp_ping_handle_t hdl, void *args)
{
    ping_timeout_count = 0;
    heart_beat_reset();
    ESP_LOGI("HeartBeat","Ping success!");
}

void heart_beat_ping_timeout(esp_ping_handle_t hdl, void *args)
{
    if(ping_timeout_count > 3)
    {
        force_restart("HeartBeatTimeOut");
    }
    ping_timeout_count++;
    ESP_LOGI("HeartBeat","Ping timeout!");
}

void heart_beat_ping_end(esp_ping_handle_t hdl, void *args)
{
    xEventGroupSetBits(s_ping_event_group, PING_DONE_BIT);
}

void ping_once(const char *ip_str)
{
    esp_ping_config_t config = ESP_PING_DEFAULT_CONFIG();
    ip4_addr_t target_ip;
    ip4addr_aton(ip_str, &target_ip);
    config.target_addr = target_ip;
    config.count = 1;
    config.timeout_ms = 1000;

    esp_ping_callbacks_t cbs = {
        .cb_args = NULL,
        .on_ping_success = heart_beat_ping_success,
        .on_ping_timeout = heart_beat_ping_timeout,
        .on_ping_end = heart_beat_ping_end,
    };

    esp_ping_handle_t ping;
    esp_err_t ret = esp_ping_new_session(&config, &cbs, &ping);
    if (ret != ESP_OK) {
        ESP_LOGE("HeartBeat","esp_ping_new_session failed: %d", ret);
        return;
    }

    ret = esp_ping_start(ping);
    if (ret != ESP_OK)
    {
        ESP_LOGE("HeartBeat","esp_ping_start failed: %d", ret);
        esp_ping_delete_session(ping);
        return;
    }

    // 等待ping结束信号
    xEventGroupWaitBits(s_ping_event_group, PING_DONE_BIT, pdTRUE, pdFALSE, portMAX_DELAY);

    esp_ping_delete_session(ping);
}

void heart_beat_ping(void)
{
    s_ping_event_group = xEventGroupCreate();
    ping_once(globe_user_config.wifi_config.ip);
}

void heart_beat_callback(void *arg)
{
    #if CONFIG_HEART_BEAT_ENABLE
    heart_beat_ping();
    #endif
}

void force_restart(char* RestartReason)
{
    ESP_LOGI("System","%s",RestartReason);
    abort();
    esp_restart();
}



void ICACHE_FLASH_ATTR heart_beat_init(void)
{
    #if CONFIG_HEART_BEAT_ENABLE
    esp_err_t err;

    esp_timer_create_args_t esp_timer_create_args_struct;
    esp_timer_create_args_struct.name = "HeartBeat Timer";
    esp_timer_create_args_struct.callback = &heart_beat_callback;
    esp_timer_create_args_struct.arg = NULL;
    esp_timer_create_args_struct.dispatch_method = ESP_TIMER_TASK;

    esp_timer_init();

    err = esp_timer_create(&esp_timer_create_args_struct,&esp_timer_handle_struct);
    if (err != ESP_OK)
    {  
        force_restart("HeartBeat_Init");
    }  
    err = esp_timer_start_periodic(esp_timer_handle_struct,1000000 * CONFIG_HEART_BEAT_TIME);
    if (err != ESP_OK)
    {  
        force_restart("HeartBeat_Init");
    }

    heart_beat_reset();
    #endif
}