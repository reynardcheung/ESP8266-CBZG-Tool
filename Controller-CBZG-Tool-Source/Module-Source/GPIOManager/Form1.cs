using ModNamespace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GPIOManager.JsonPack;

namespace GPIOManager
{
    public partial class Form1 : Form
    {
        private NetworkStream Stream { get; set; }
        private IMod Module {  get; set; }

        private readonly GPIO_Tool Tool;
        public Form1(NetworkStream _stream,IMod mod)
        {
            InitializeComponent();
            SystemStatus.StatusChanged += Controls_Enable_Change;
            Stream = _stream;
            Module = mod;
            PinMode_0.SelectedIndex = 0;
            PinRes_0.SelectedIndex = 0;
            PinLevel_0.SelectedIndex = 0;
            PinMode_2.SelectedIndex = 0;
            PinRes_2.SelectedIndex = 0;
            PinLevel_2.SelectedIndex = 0;
            Tool = new GPIO_Tool(Stream,AutoUpdataTimer, GetPWMConfig);
            Controls_Enable_Set();
        }

        private void Controls_Enable_Change(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => Controls_Enable_Set()));
            }
            else
            {
                Controls_Enable_Set();
            }
        }

        private void Controls_Enable_Set()
        {
            switch (SystemStatus.CurrentStatus)
            {
                case SystemStatus.Status.Disconnect:
                    Auto_PWMToDevice.Enabled = false;
                    GPIO0Group.Enabled = false;
                    GPIO2Group.Enabled = false;
                    PWMToDevice_0.Enabled = false;
                    PWMToDevice_2.Enabled = false;
                    break;
                case SystemStatus.Status.Connecting:
                    Auto_PWMToDevice.Enabled = false;
                    GPIO0Group.Enabled = false;
                    GPIO2Group.Enabled = false;
                    PWMToDevice_0.Enabled = false;
                    PWMToDevice_2.Enabled = false;
                    break;
                case SystemStatus.Status.Connected:
                    Auto_PWMToDevice.Enabled = false;
                    GPIO0Group.Enabled = true;
                    GPIO2Group.Enabled = true;
                    PWMToDevice_0.Enabled = false;
                    PWMToDevice_2.Enabled = false;
                    break;
                case SystemStatus.Status.AutoUpdata:
                    Auto_PWMToDevice.Enabled = false;
                    GPIO0Group.Enabled = false;
                    GPIO2Group.Enabled = false;
                    PWMToDevice_0.Enabled = false;
                    PWMToDevice_2.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private async void GPIOManagerSW_CheckedChanged(object sender, EventArgs e)
        {
            if (GPIOManagerSW.Checked)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Connecting);
                bool Ret = await Tool.StartDevice();
                if (Ret)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
                    SystemStatus.StatusChanged += Tool.Status_Changed;
                }
                else
                {
                    GPIOManagerSW.Checked = false;
                }
            }
            else
            {
                if(SystemStatus.CurrentStatus == SystemStatus.Status.Connected && GPIOManagerSW.Checked == false)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    SystemStatus.StatusChanged -= Tool.Status_Changed;
                }
                else
                {
                    if (GPIOManagerSW.Checked)
                    {
                        GPIOManagerSW.Checked = false;
                    }
                }
            }
        }

        private void Auto_PWMToDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (Auto_PWMToDevice.Checked)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.AutoUpdata);
                Tool.AutoUpdataSwitch(true);

            }
            else
            {
                if (SystemStatus.CurrentStatus == SystemStatus.Status.AutoUpdata && GPIOManagerSW.Checked == false)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
                    Tool.AutoUpdataSwitch(false);
                }
                else
                {
                    if (Auto_PWMToDevice.Checked)
                    {
                        Auto_PWMToDevice.Checked = false;
                    }
                }
            }
        }

        public string GetPWMConfig(int Number)
        {
            if(Number == 0)
            {

                var Config = new GPIOConfig
                {
                    GpioNum = 0,
                    GpioMode = new GPIOMode
                    {
                        Enable = 0,
                        Mode = 0,
                        PullUp = 0,
                        PullDown = 0,
                        Level = 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 1,
                        Duty = (int)(((float)PWMDuty_0.Value / 100.0f) * (int)PWMPeriod_0.Value),
                        Period = (int)PWMPeriod_0.Value,
                        Phase = (double)PWMPhase_0.Value
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);

                return jsonString;
            }
            else if(Number == 2)
            {
                var Config = new GPIOConfig
                {
                    GpioNum = 2,
                    GpioMode = new GPIOMode
                    {
                        Enable = 0,
                        Mode = 0,
                        PullUp = 0,
                        PullDown = 0,
                        Level = 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 1,
                        Duty = (int)(((float)PWMDuty_2.Value / 100.0f) * (int)PWMPeriod_2.Value),
                        Period = (int)PWMPeriod_2.Value,
                        Phase = (double)PWMPhase_2.Value
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);

                return jsonString;
            }
            else
            {
                return null;
            }
        }

        private async void GPIOModeUpdata_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if(SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return;
            }

            string name = button.Name;
            string[] parts = name.Split('_');
            int.TryParse(parts[parts.Length - 1], out int GPIONum);

            if(GPIONum == 0)
            {
                var Config = new GPIOConfig
                {
                    GpioNum = GPIONum,
                    GpioMode = new GPIOMode
                    {
                        Enable = 1,
                        Mode = PinMode_0.SelectedIndex,
                        PullUp = PinRes_0.SelectedIndex == 0 ? 1 : 0,
                        PullDown = PinRes_0.SelectedIndex == 1 ? 1 : 0,
                        Level = PinLevel_0.SelectedIndex == 0 ? 1 : 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 0,
                        Duty = 0,
                        Period = 0,
                        Phase = 0.0
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);
                bool Ret = await Tool.SendConfigToDevice(jsonString);
                if(Ret)
                {
                    MessageBox.Show($"GPIO设定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"发生错误：GPIO设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    Auto_PWMToDevice.Checked = false;
                    GPIOManagerSW.Checked = false;
                }
            }
            else
            {
                var Config = new GPIOConfig
                {
                    GpioNum = GPIONum,
                    GpioMode = new GPIOMode
                    {
                        Enable = 1,
                        Mode = PinMode_2.SelectedIndex,
                        PullUp = PinRes_2.SelectedIndex == 0 ? 1 : 0,
                        PullDown = PinRes_2.SelectedIndex == 1 ? 1 : 0,
                        Level = PinLevel_2.SelectedIndex == 0 ? 1 : 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 0,
                        Duty = 0,
                        Period = 0,
                        Phase = 0.0
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);
                bool Ret = await Tool.SendConfigToDevice(jsonString);
                if (Ret)
                {
                    MessageBox.Show($"GPIO设定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"发生错误：GPIO设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    Auto_PWMToDevice.Checked = false;
                    GPIOManagerSW.Checked = false;
                }
            }
        }

        private async void PWMModeUpdata_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return;
            }

            string name = button.Name;
            string[] parts = name.Split('_');
            int.TryParse(parts[parts.Length - 1], out int GPIONum);

            if (GPIONum == 0)
            {
                var Config = new GPIOConfig
                {
                    GpioNum = GPIONum,
                    GpioMode = new GPIOMode
                    {
                        Enable = 0,
                        Mode = 0,
                        PullUp = 0,
                        PullDown = 0,
                        Level = 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 1,
                        Duty = (int)(((float)PWMDuty_0.Value / 100.0f) * (int)PWMPeriod_0.Value),
                        Period = (int)PWMPeriod_0.Value,
                        Phase = (double)PWMPhase_0.Value
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);
                bool Ret = await Tool.SendConfigToDevice(jsonString);
                if (Ret)
                {
                    MessageBox.Show($"PWM设定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"发生错误：PWM设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    Auto_PWMToDevice.Checked = false;
                    GPIOManagerSW.Checked = false;
                }
            }
            else
            {
                var Config = new GPIOConfig
                {
                    GpioNum = GPIONum,
                    GpioMode = new GPIOMode
                    {
                        Enable = 0,
                        Mode = 0,
                        PullUp = 0,
                        PullDown = 0,
                        Level = 0
                    },
                    PwmMode = new PwmMode
                    {
                        Enable = 1,
                        Duty = (int)(((float)PWMDuty_2.Value / 100.0f) * (int)PWMPeriod_2.Value),
                        Period = (int)PWMPeriod_2.Value,
                        Phase = (double)PWMPhase_2.Value
                    }
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(Config);
                bool Ret = await Tool.SendConfigToDevice(jsonString);
                if (Ret)
                {
                    MessageBox.Show($"PWM设定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"发生错误：PWM设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    Auto_PWMToDevice.Checked = false;
                    GPIOManagerSW.Checked = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            Auto_PWMToDevice.Checked = false;
            GPIOManagerSW.Checked = false;
        }
    }

    public class SystemStatus : IDisposable
    {
        public enum Status
        {
            Disconnect,
            Connecting,
            Connected,
            AutoUpdata
        }

        private static EventHandler _statusChanged;
        private static readonly HashSet<EventHandler> _handlers = new HashSet<EventHandler>();
        private static Status _currentStatus = Status.Disconnect;
        private bool _disposed;

        public static event EventHandler StatusChanged
        {
            add
            {
                lock (_handlers)
                {
                    if (!_handlers.Contains(value))
                    {
                        _statusChanged += value;
                        _handlers.Add(value);
                    }
                }
            }
            remove
            {
                lock (_handlers)
                {
                    if (_handlers.Contains(value))
                    {
                        _statusChanged -= value;
                        _handlers.Remove(value);
                    }
                }
            }
        }

        public static Status CurrentStatus
        {
            get => _currentStatus;
            private set
            {
                if (_currentStatus != value)
                {
                    _currentStatus = value;
                    OnStatusChanged();
                }
            }
        }

        public static void ChangeStatus(Status newStatus)
        {
            CurrentStatus = newStatus;
        }

        private static void OnStatusChanged()
        {
            _statusChanged?.Invoke(null, EventArgs.Empty);
        }

        public static bool IsSubscribed(EventHandler handler)
        {
            lock (_handlers)
            {
                return _handlers.Contains(handler);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {

                lock (_handlers)
                {
                    foreach (var handler in _handlers)
                    {
                        _statusChanged -= handler;
                    }
                    _handlers.Clear();
                }
            }

            _disposed = true;
        }

        ~SystemStatus()
        {
            Dispose(false);
        }
    }

    public class JsonPack
    {
        public class GPIOConfig
        {
            [JsonPropertyName("GPIO_NUM")]
            public int GpioNum { get; set; }

            [JsonPropertyName("GPIO_MODE")]
            public GPIOMode GpioMode { get; set; }

            [JsonPropertyName("PWM_MODE")]
            public PwmMode PwmMode { get; set; }
        }

        public class GPIOMode
        {
            [JsonPropertyName("Enable")]
            public int Enable { get; set; }

            [JsonPropertyName("Mode")]
            public int Mode { get; set; }

            [JsonPropertyName("PullUp")]
            public int PullUp { get; set; }

            [JsonPropertyName("PUllDown")]
            public int PullDown { get; set; }

            [JsonPropertyName("Level")]
            public int Level { get; set; }
        }

        public class PwmMode
        {
            [JsonPropertyName("Enable")]
            public int Enable { get; set; }

            [JsonPropertyName("Duty")]
            public int Duty { get; set; }

            [JsonPropertyName("Period")]
            public int Period { get; set; }

            [JsonPropertyName("Phase")]
            public double Phase { get; set; }
        }
    }

    public class GPIO_Tool
    {
        private NetworkStream Stream { get; set; }
        private System.Windows.Forms.Timer AutoPWMUpdata_Timer { get; set; }

        private readonly Func<int,string> GetPWMConfig;

        public GPIO_Tool(NetworkStream stream, System.Windows.Forms.Timer timer, Func<int, string> getPWMConfig)
        {
            Stream = stream;
            AutoPWMUpdata_Timer = timer;
            GetPWMConfig = getPWMConfig;
        }

        public async Task<bool> StartDevice()
        {
            byte[] SendBytes = Encoding.UTF8.GetBytes("{GPIOManager}");
            byte[] RecvBytes = new byte[16];
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(SendBytes, 0, SendBytes.Length, cts.Token);
                    int Length = await Stream.ReadAsync(RecvBytes, 0, RecvBytes.Length, cts.Token);

                    if (Length == 1 && RecvBytes[0] == 'y')
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

            return false;
        }

        public async Task StopDevice()
        {
            byte[] SendBytes = Encoding.UTF8.GetBytes("{GPIOManager}");
            byte[] RecvBytes = new byte[16];
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(SendBytes, 0, SendBytes.Length, cts.Token);
                    int Length = await Stream.ReadAsync(RecvBytes, 0, RecvBytes.Length, cts.Token);
                    Console.WriteLine("停止");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("关闭时出现错误");
            }
            finally
            {

            }
        }

        public async void Status_Changed(object sender, EventArgs e)
        {
            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                if(AutoPWMUpdata_Timer.Enabled)
                {
                    AutoUpdataSwitch(false);
                }
                await StopDevice();
            }
        }

        public void AutoUpdataSwitch(bool Enable)
        {
            if( Enable )
            {
                AutoPWMUpdata_Timer.Tick += Timer_Tick;
                AutoPWMUpdata_Timer.Enabled = true;
            }
            else
            {
                AutoPWMUpdata_Timer.Enabled = false;
                AutoPWMUpdata_Timer.Tick -= Timer_Tick;
            }
        }

        public async void Timer_Tick(object sender, EventArgs e)
        {
            string Config_Pin_0 = GetPWMConfig(0);
            string Config_Pin_2 = GetPWMConfig(2);
            if(!await SendConfigToDevice(Config_Pin_0))
            {
                MessageBox.Show($"发生错误：PWM自动设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            
            if(!await SendConfigToDevice(Config_Pin_2))
            {
                MessageBox.Show($"发生错误：PWM自动设定失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        public async Task<bool> SendConfigToDevice(string JsonStr)
        {
            byte[] SendBytes = Encoding.UTF8.GetBytes(JsonStr);
            byte[] RecvBytes = new byte[16];
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(SendBytes, 0, SendBytes.Length, cts.Token);
                    int Length = await Stream.ReadAsync(RecvBytes, 0, RecvBytes.Length, cts.Token);

                    if (Length == 1 && RecvBytes[0] == 'y')
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
            return false;
        }
    }


}
