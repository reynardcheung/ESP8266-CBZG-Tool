#include "user_web_config.h"
#include "user_flash_rw.h"
#include "esp_err.h"

#define TAG "WEB_CONFIG"
#define MIN(x,y) x > y ? y : x

static const char CONFIG_PAGE[] = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>CBZG Tool WiFi配置</title><style>*{box-sizing:border-box;font-family:'Segoe UI',Tahoma,sans-serif}body{background:linear-gradient(135deg,#6a11cb 0%,#2575fc 100%);margin:0;padding:20px;min-height:100vh;display:flex;justify-content:center;align-items:center}.container{background:rgba(255,255,255,0.9);border-radius:16px;box-shadow:0 10px 30px rgba(0,0,0,0.2);width:100%;max-width:450px;padding:40px;text-align:center}h1{color:#2c3e50;margin-top:0;font-weight:600}.logo{width:80px;margin-bottom:20px}.form-group{margin-bottom:25px;text-align:left}label{display:block;margin-bottom:8px;color:#34495e;font-weight:500}input{width:100%;padding:14px;border:2px solid#ddd;border-radius:10px;font-size:16px;transition:border 0.3s}input:focus{border-color:#3498db;outline:none;box-shadow:0 0 0 3px rgba(52,152,219,0.2)}button{background:linear-gradient(to right,#3498db,#2c3e50);color:white;border:none;padding:16px;width:100%;border-radius:10px;cursor:pointer;font-size:18px;font-weight:600;letter-spacing:1px;transition:transform 0.2s,box-shadow 0.2s;box-shadow:0 4px 15px rgba(0,0,0,0.2)}button:hover{transform:translateY(-2px);box-shadow:0 6px 20px rgba(0,0,0,0.25)}button:active{transform:translateY(0)}.status{margin-top:25px;padding:15px;border-radius:10px;font-weight:500;display:none}.success{background-color:#d4edda;color:#155724}.error{background-color:#f8d7da;color:#721c24}@media(max-width:480px){.container{padding:25px}}</style></head><body><div class=\"container\"><svg viewBox=\"0 0 64 64\" width=\"48\" height=\"48\" style=\"display:inline-block;\"><g id=\"Layer_90\" data-name=\"Layer 90\"><path d=\"m59.15 22.26a1.47 1.47 0 0 1 -1.06-.44 37 37 0 0 0 -52.18 0 1.5 1.5 0 0 1 -2.12-2.12 39.93 39.93 0 0 1 56.42 0 1.49 1.49 0 0 1 0 2.12 1.45 1.45 0 0 1 -1.06.44z\" fill=\"#000000\" style=\"fill: rgb(77, 243, 255);\"></path><path d=\"m51.65 29.77a1.53 1.53 0 0 1 -1.07-.44 26.29 26.29 0 0 0 -37.16 0 1.5 1.5 0 0 1 -2.13-2.12 29.32 29.32 0 0 1 41.42 0 1.51 1.51 0 0 1 0 2.12 1.53 1.53 0 0 1 -1.06.44z\" fill=\"#000000\" style=\"fill: rgb(77, 243, 255);\"></path><path d=\"m44.14 37.27a1.47 1.47 0 0 1 -1.06-.44 15.69 15.69 0 0 0 -22.16 0 1.5 1.5 0 0 1 -2.12-2.12 18.69 18.69 0 0 1 26.4 0 1.49 1.49 0 0 1 0 2.12 1.47 1.47 0 0 1 -1.06.44z\" fill=\"#000000\" style=\"fill: rgb(77, 243, 255);\"></path><path d=\"m32 56a8 8 0 1 1 5.69-13.74 8 8 0 0 1 -5.69 13.74zm0-13.1a5.06 5.06 0 1 0 3.57 1.48 5.05 5.05 0 0 0 -3.57-1.52z\" fill=\"#000000\" style=\"fill: rgb(77, 243, 255);\"></path></g></svg><h1>WiFi配置</h1><form id=\"config-form\" onsubmit=\"return false;\"><div class=\"form-group\"><label for=\"ssid\">WiFi名称</label><input type=\"text\" maxlength=32 id=\"ssid\" name=\"ssid\" required placeholder=\"输入您的WiFi名称\"></div><div class=\"form-group\"><label for=\"password\">WiFi密码</label><input type=\"password\" maxlength=16 id=\"password\" name=\"password\" required placeholder=\"输入WiFi密码\"></div><div class=\"form-group\"><label for=\"ip\">服务器地址</label><input type=\"text\" maxlength=16 id=\"ip\" name=\"ip\" required placeholder=\"输入服务器地址\"></div><div class=\"form-group\"><label for=\"port\">服务器端口</label><input type=\"number\" max=\"65535\" min=\"0\" id=\"port\" name=\"port\" required placeholder=\"输入服务器端口\"></div><button type=\"submit\">连接</button></form><div id=\"status\" class=\"status\"></div></div><script>document.addEventListener('DOMContentLoaded', function() { document.getElementById('config-form').addEventListener('submit', function(e) { e.preventDefault(); const ssid = document.getElementById('ssid').value; const password = document.getElementById('password').value; const ip = document.getElementById('ip').value; const port = parseInt(document.getElementById('port').value, 10); const statusDiv = document.getElementById('status'); statusDiv.style.display = 'none';console.log(\"提交数据:\", {ssid, password, ip, port}); fetch('/configure', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ ssid, password, ip, port }) }) .then(response => { console.log(\"响应状态:\", response.status); return response.json(); }) .then(data => { console.log(\"响应数据:\", data); statusDiv.className = 'status ' + (data.success ? 'success' : 'error'); statusDiv.textContent = data.message; statusDiv.style.display = 'block'; if (data.success) { document.getElementById('config-form').reset(); let count = 15; const timer = setInterval(() => { statusDiv.textContent = `${data.message} 设备将在${count}秒后重启...`; if (count-- <= 0) { clearInterval(timer); } }, 1000); } }) .catch(error => { console.error(\"请求错误:\", error); statusDiv.className = 'status error'; statusDiv.textContent = '网络错误，请重试'; statusDiv.style.display = 'block'; }); });});</script></body></html>";

user_config_t config =
{
    .uart_config = {
        .baud_rate           = (int)CONFIG_UART0_BAUD_RATE,
        .data_bits           = (uart_word_length_t )CONFIG_UART0_DATA_BITS,      //8位
        .parity              = (uart_parity_t)CONFIG_UART0_PARITY,            //偶校验
        .stop_bits           = (uart_stop_bits_t)CONFIG_UART0_STOP_BITS,      //停止位1位
        .flow_ctrl           = UART_HW_FLOWCTRL_DISABLE,    //硬件流关闭
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

static esp_err_t config_page_handler(httpd_req_t *req)
{
    httpd_resp_set_type(req, "text/html");
    return httpd_resp_send(req, CONFIG_PAGE, strlen(CONFIG_PAGE));
}

static esp_err_t config_save_handler(httpd_req_t *req)
{
    char content[512];
    int ret, remaining = req->content_len;
    ESP_LOGI(TAG, "Req Method: %d", req->method);

    if ((ret = httpd_req_recv(req, content, MIN(remaining, sizeof(content)))) <= 0)
    {
        ESP_LOGE(TAG, "Http Recv Failed");
        if (ret == HTTPD_SOCK_ERR_TIMEOUT)
        {
            httpd_resp_send_408(req);
        }
        return ESP_FAIL;
    }
    content[ret] = '\0';
    ESP_LOGI(TAG, "cJSON Parse:%s",content);

    cJSON *root = cJSON_Parse(content);
    if (!root)
    {
        ESP_LOGE(TAG, "Http Recv Json Is Not Vaild");
        const char *error_ptr = cJSON_GetErrorPtr();
        ESP_LOGE(TAG, "JSON Error: %s", error_ptr ? error_ptr : "Unknown Error");
        httpd_resp_set_status   (req, HTTPD_400);
        httpd_resp_set_type     (req, HTTPD_TYPE_TEXT);
        httpd_resp_send  (req, "Invalid content", strlen("Invalid content"));
        return ESP_FAIL;
    }
    ESP_LOGI(TAG, "cJSON Get Item");

    cJSON *ssid = cJSON_GetObjectItem(root, "ssid");
    cJSON *password = cJSON_GetObjectItem(root, "password");
    cJSON *ip = cJSON_GetObjectItem(root, "ip");
    cJSON *port = cJSON_GetObjectItem(root, "port");

    if (!cJSON_IsString(ssid) || !cJSON_IsString(password) || 
        !cJSON_IsString(ip) || !cJSON_IsNumber(port))
    {
        ESP_LOGE(TAG, "Param Is Not Vaild");
        cJSON_Delete(root);
        httpd_resp_set_status   (req, HTTPD_400);
        httpd_resp_set_type     (req, HTTPD_TYPE_TEXT);
        httpd_resp_send  (req, "Invalid content", strlen("Invalid content"));
        return ESP_FAIL;
    }

    strncpy(config.wifi_config.ssid, ssid->valuestring, sizeof(config.wifi_config.ssid) - 1);
    strncpy(config.wifi_config.password, password->valuestring, sizeof(config.wifi_config.password) - 1);
    strncpy(config.wifi_config.ip, ip->valuestring, sizeof(config.wifi_config.ip) - 1);
    config.wifi_config.port = (uint16_t)port->valueint;

    config.wifi_config.ssid[sizeof(config.wifi_config.ssid) - 1] = '\0';
    config.wifi_config.password[sizeof(config.wifi_config.password) - 1] = '\0';
    config.wifi_config.ip[sizeof(config.wifi_config.ip) - 1] = '\0';

    cJSON_Delete(root);
    ESP_LOGI(TAG, "Saving To NVS");

    if (!save_config(&config))
    {
        ESP_LOGE(TAG, "Config Saved Failed");
        httpd_resp_set_status   (req, HTTPD_500);
        httpd_resp_set_type     (req, HTTPD_TYPE_TEXT);
        httpd_resp_send  (req, "Failed to save the config", strlen("Failed to save the config"));
        return ESP_FAIL;
    }
    ESP_LOGI(TAG, "Config Saved Successed");

    cJSON *resp = cJSON_CreateObject();
    cJSON_AddBoolToObject(resp, "success", true);
    cJSON_AddStringToObject(resp, "message", "The configuration is saved and the device is about to restart");
    
    const char *resp_str = cJSON_Print(resp);
    httpd_resp_set_type(req, "application/json");
    httpd_resp_send(req, resp_str, strlen(resp_str));
    
    cJSON_Delete(resp);
    free((void*)resp_str);

    vTaskDelay(pdMS_TO_TICKS(3000));
    esp_restart();

    return ESP_OK;
}

static httpd_uri_t config_page_uri = {
    .uri       = "/",
    .method    = HTTP_GET,
    .handler   = config_page_handler,
    .user_ctx  = NULL
};

static httpd_uri_t config_save_uri = {
    .uri       = "/configure",
    .method    = HTTP_POST,
    .handler   = config_save_handler,
    .user_ctx  = NULL
};

httpd_handle_t start_webserver(void) {
    httpd_config_t config = HTTPD_DEFAULT_CONFIG();
    httpd_handle_t server = NULL;

    if (httpd_start(&server, &config) == ESP_OK) {
        httpd_register_uri_handler(server, &config_page_uri);
        httpd_register_uri_handler(server, &config_save_uri);
        ESP_LOGI(TAG, "Web Server Start Success");
    }
    return server;
}

void setup_ap_mode(void) {
    ESP_ERROR_CHECK(esp_event_loop_create_default());
    wifi_init_config_t cfg = WIFI_INIT_CONFIG_DEFAULT();
    esp_wifi_init(&cfg);
    
    wifi_config_t ap_config = {
        .ap = {
            .ssid = "CBZG_Tool_AP",
            .ssid_len = 0,
            .max_connection = 4,
            .password = "",
            .authmode = WIFI_AUTH_OPEN
        }
    };
    
    esp_wifi_set_mode(WIFI_MODE_AP);
    esp_wifi_set_config(ESP_IF_WIFI_AP, &ap_config);
    esp_wifi_start();
    ESP_LOGI(TAG, "AP Mode: SSID=CBZG_Tool_AP");
}

void start_web_config()
{
    esp_err_t ret = nvs_flash_init();
    if (ret == ESP_ERR_NVS_NO_FREE_PAGES || ret == ESP_ERR_NVS_NEW_VERSION_FOUND) {
        ESP_ERROR_CHECK(nvs_flash_erase());
        ret = nvs_flash_init();
    }
    ESP_ERROR_CHECK(ret);
    setup_ap_mode();
    start_webserver();
}

