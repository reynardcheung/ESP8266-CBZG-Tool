using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using static WirelessUart.TCPCOMHandle;
using System.Diagnostics;
using ModNamespace;

namespace WirelessUart
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private COMForwarding COMForwardingObj;
        private TCPCOMHandle TCPCOMHandleObj;
        private ConditionOutputController ConditionOutputControllerObj;
        private int RXCount = 0;
        private int TXCount = 0;

        private SystemConnectStatus NetworkStaus = new SystemConnectStatus();


        public Form1(NetworkStream _stream ,IMod mod)
        {
            InitializeComponent();
            stream = _stream;
        }

        private void ControlsEnableController(SystemConnectStatus.SysStatus Status)
        {
            switch (Status)
            {
                case SystemConnectStatus.SysStatus.Disconnected: 
                { 
                        UartSendButton.Enabled = false;
                        UartHexSendButton.Enabled = false;
                        UartFIleSendButton.Enabled=false;
                        OnWirelessUartButton.Enabled = true;
                        OffWirelessUartButton.Enabled = false;
                        COMComboBox.Enabled = false;
                        COMForwardButton.Enabled = false;
                        SerialPinSyncCheckBox.Enabled = false;
                        OffCOMForwardButton.Enabled = false;
                        ConditionOutputCheckBox.Enabled = false;
                        ConditionOutputCheckBox.Checked = false;
                        QuickSend1.Enabled = false;
                        QuickSend2.Enabled = false;
                        QuickSend3.Enabled = false;
                        QuickSend4.Enabled = false;
                        QuickSend5.Enabled = false;
                        QuickSend6.Enabled = false;
                        QuickSend7.Enabled = false;
                        QuickSend8.Enabled = false;
                        QuickSend9.Enabled = false;
                        ConnectStatus.Text = "无连接";
                        ComForwardStatus.Text = "停止转发";
                        break; 
                }
                case SystemConnectStatus.SysStatus.Connecting: 
                {
                        UartSendButton.Enabled = false;
                        UartHexSendButton.Enabled = false;
                        UartFIleSendButton.Enabled = false;
                        OnWirelessUartButton.Enabled = false;
                        OffWirelessUartButton.Enabled = false;
                        COMComboBox.Enabled = false;
                        COMForwardButton.Enabled = false;
                        SerialPinSyncCheckBox.Enabled = false;
                        OffCOMForwardButton.Enabled = false;
                        ConditionOutputCheckBox.Enabled = false;
                        ConditionOutputCheckBox.Checked = false;
                        QuickSend1.Enabled = false;
                        QuickSend2.Enabled = false;
                        QuickSend3.Enabled = false;
                        QuickSend4.Enabled = false;
                        QuickSend5.Enabled = false;
                        QuickSend6.Enabled = false;
                        QuickSend7.Enabled = false;
                        QuickSend8.Enabled = false;
                        QuickSend9.Enabled = false;
                        ConnectStatus.Text = "连接中...";
                        ComForwardStatus.Text = "停止转发";
                        break;
                }
                case SystemConnectStatus.SysStatus.Connected:
                {
                        UartSendButton.Enabled = true;
                        UartHexSendButton.Enabled = true;
                        UartFIleSendButton.Enabled = true;
                        OnWirelessUartButton.Enabled = false;
                        OffWirelessUartButton.Enabled = true;
                        COMComboBox.Enabled = true;
                        COMForwardButton.Enabled = true;
                        SerialPinSyncCheckBox.Enabled = false;
                        OffCOMForwardButton.Enabled = false;
                        ConditionOutputCheckBox.Enabled = true;
                        QuickSend1.Enabled = true;
                        QuickSend2.Enabled = true;
                        QuickSend3.Enabled = true;
                        QuickSend4.Enabled = true;
                        QuickSend5.Enabled = true;
                        QuickSend6.Enabled = true;
                        QuickSend7.Enabled = true;
                        QuickSend8.Enabled = true;
                        QuickSend9.Enabled = true;
                        ConnectStatus.Text = "连接成功";
                        ComForwardStatus.Text = "停止转发";
                        break;
                }
                case SystemConnectStatus.SysStatus.OffCOMForward:
                {
                        UartSendButton.Enabled = true;
                        UartHexSendButton.Enabled = true;
                        UartFIleSendButton.Enabled = true;
                        OnWirelessUartButton.Enabled = false;
                        OffWirelessUartButton.Enabled = true;
                        COMComboBox.Enabled = true;
                        COMForwardButton.Enabled = true;
                        SerialPinSyncCheckBox.Enabled = false;
                        OffCOMForwardButton.Enabled = false;
                        ConditionOutputCheckBox.Enabled = true;
                        QuickSend1.Enabled = true;
                        QuickSend2.Enabled = true;
                        QuickSend3.Enabled = true;
                        QuickSend4.Enabled = true;
                        QuickSend5.Enabled = true;
                        QuickSend6.Enabled = true;
                        QuickSend7.Enabled = true;
                        QuickSend8.Enabled = true;
                        QuickSend9.Enabled = true;
                        ConnectStatus.Text = "连接成功";
                        ComForwardStatus.Text = "停止转发";
                        break;
                }
                case SystemConnectStatus.SysStatus.COMForwarding:
                {
                        UartSendButton.Enabled = true;
                        UartHexSendButton.Enabled = true;
                        UartFIleSendButton.Enabled = true;
                        OnWirelessUartButton.Enabled = false;
                        OffWirelessUartButton.Enabled = true;
                        COMComboBox.Enabled = false;
                        COMForwardButton.Enabled = false;
                        SerialPinSyncCheckBox.Enabled = false;
                        OffCOMForwardButton.Enabled = false;
                        ConditionOutputCheckBox.Enabled = true;
                        QuickSend1.Enabled = true;
                        QuickSend2.Enabled = true;
                        QuickSend3.Enabled = true;
                        QuickSend4.Enabled = true;
                        QuickSend5.Enabled = true;
                        QuickSend6.Enabled = true;
                        QuickSend7.Enabled = true;
                        QuickSend8.Enabled = true;
                        QuickSend9.Enabled = true;
                        ConnectStatus.Text = "连接成功";
                        ComForwardStatus.Text = "正在连接";
                        break;
                }
                case SystemConnectStatus.SysStatus.OnCOMForward:
                {
                        UartSendButton.Enabled = true;
                        UartHexSendButton.Enabled = true;
                        UartFIleSendButton.Enabled = true;
                        OnWirelessUartButton.Enabled = false;
                        OffWirelessUartButton.Enabled = true;
                        COMComboBox.Enabled = false;
                        COMForwardButton.Enabled = false;
                        SerialPinSyncCheckBox.Enabled = true;
                        OffCOMForwardButton.Enabled = true;
                        ConditionOutputCheckBox.Enabled = true;
                        QuickSend1.Enabled = true;
                        QuickSend2.Enabled = true;
                        QuickSend3.Enabled = true;
                        QuickSend4.Enabled = true;
                        QuickSend5.Enabled = true;
                        QuickSend6.Enabled = true;
                        QuickSend7.Enabled = true;
                        QuickSend8.Enabled = true;
                        QuickSend9.Enabled = true;
                        ConnectStatus.Text = "连接成功";
                        ComForwardStatus.Text = "正在转发";
                        break;
                }
            }
        }

        public void LoadAvailablePorts()
        {
            // 清空现有项  
            this.COMComboBox.Items.Clear();

            // 获取可用串口  
            string[] ports = SerialPort.GetPortNames();
            
            if(ports.Length == 0 ) 
            { 
                MessageBox.Show("警告: 无可用端口", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // 将每个串口添加到ComboBox中  
            foreach (string port in ports)
            {
                COMComboBox.Items.Add(port);
            }

            // 可选：选择第一个串口  
            if (COMComboBox.Items.Count > 0)
            {
                COMComboBox.SelectedIndex = 0;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.Disconnected;
            ConditionOutputDataBase.LoadFromFile();
            ConditionOutputListShowFlush();
        }

        public void Stop()
        {
            NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.Disconnected;
            NetworkStaus.UnsubscribeEvent(ControlsEnableController);
        }

        private async void OnWirelessUartButton_Click(object sender, EventArgs e)
        {
            NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.Connecting;
            MessageBox.Show($"ESP-01S的波特率请在Settings模块设置", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            TCPCOMHandleObj = new TCPCOMHandle(stream, NetworkStaus);
            if (await TCPCOMHandleObj.StartTCPCOM() == true)
            {
                TCPCOMHandleObj.SubscribeEvent(RXrichTextBox1_TextChange);
                TCPCOMHandleObj.SubscribeEvent(RXHEXrichTextBox_TextChange);
                TCPCOMHandleObj.SubscribeEvent(CounterTextBox_TextChange);
                NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.Connected;
            }
            else
            {
                MessageBox.Show($"无法连接到设备，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.Disconnected;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailablePorts();
        }

        private void OffWirelessUartButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void COMComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            LoadAvailablePorts();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            stream = null;
            COMForwardingObj = null;
            TCPCOMHandleObj = null;
            ConditionOutputControllerObj = null;
            NetworkStaus = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConditionOutputListShowFlush();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 45;
            dataGridView1.Columns[2].FillWeight = 45;

            LoadConfigToQuickSend();

            NetworkStaus.SubscribeEvent(ControlsEnableController);
        }

        private void ConditionOutputListShowFlush()
        {
            // 绑定数据源
            dataGridView1.DataSource = ConditionOutputDataBase.DataBase;

            // 设置只显示 InputValue 和 OutputValue 两列
            dataGridView1.Columns["Priority"].Visible = true;
            dataGridView1.Columns["InputValue"].Visible = true;
            dataGridView1.Columns["OutputValue"].Visible = true;

            // 隐藏不想显示的列
            dataGridView1.Columns["InputHexValue"].Visible = false;
            dataGridView1.Columns["OutputHexValue"].Visible = false;
            dataGridView1.Columns["IsInputHex"].Visible = false;
            dataGridView1.Columns["IsOutputHex"].Visible = false;
        }

        public void RXrichTextBox1_TextChange(NetworkEventArgs args)
        {
            if (RXrichTextBox.InvokeRequired)
            {
                RXrichTextBox.Invoke(new MethodInvoker(delegate
                {
                    if(args.IsRxDir)
                    {
                        RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                        RXrichTextBox.SelectionLength = 0;
                        RXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                        RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXrichTextBox.AppendText($"{args.Time} [Recv]--> \n");

                        RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                        RXrichTextBox.SelectionColor = Color.HotPink;
                        RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXrichTextBox.AppendText($"{Encoding.UTF8.GetString(args.Data, 0, args.DataCount)}\n");
                    }
                    else
                    {
                        RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                        RXrichTextBox.SelectionLength = 0;
                        RXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                        RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXrichTextBox.AppendText($"{args.Time} [Send]--> \n");

                        RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                        RXrichTextBox.SelectionColor = Color.Tomato;
                        RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXrichTextBox.AppendText($"{Encoding.UTF8.GetString(args.Data, 0, args.DataCount)}\n");
                    }
                }));
            }
            else
            {
                if (args.IsRxDir)
                {
                    RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                    RXrichTextBox.SelectionLength = 0;
                    RXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                    RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXrichTextBox.AppendText($"{args.Time} [Recv]--> \n");

                    RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                    RXrichTextBox.SelectionColor = Color.HotPink;
                    RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXrichTextBox.AppendText($"{Encoding.UTF8.GetString(args.Data, 0, args.DataCount)}\n");
                }
                else
                {
                    RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                    RXrichTextBox.SelectionLength = 0;
                    RXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                    RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXrichTextBox.AppendText($"{args.Time} [Send]--> \n");

                    RXrichTextBox.SelectionStart = RXrichTextBox.TextLength;
                    RXrichTextBox.SelectionColor = Color.Tomato;
                    RXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXrichTextBox.AppendText($"{Encoding.UTF8.GetString(args.Data, 0, args.DataCount)}\n");
                }
            }
        }

        public void RXHEXrichTextBox_TextChange(NetworkEventArgs args)
        {
            if (RXHEXrichTextBox.InvokeRequired)
            {
                RXHEXrichTextBox.Invoke(new MethodInvoker(delegate
                {
                    if(args.IsRxDir)
                    {
                        RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                        RXHEXrichTextBox.SelectionLength = 0;
                        RXHEXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                        RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXHEXrichTextBox.AppendText($"{args.Time} [Recv]--> \n");

                        RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                        RXHEXrichTextBox.SelectionColor = Color.HotPink;
                        string hexString = string.Join(" ", args.Data.Take(args.DataCount).Select(b => $"0x{b:X2}"));
                        RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXHEXrichTextBox.AppendText(hexString + "\n");
                    }
                    else
                    {
                        RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                        RXHEXrichTextBox.SelectionLength = 0;
                        RXHEXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                        RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXHEXrichTextBox.AppendText($"{args.Time} [Send]--> \n");

                        RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                        RXHEXrichTextBox.SelectionColor = Color.Tomato;
                        string hexString = string.Join(" ", args.Data.Take(args.DataCount).Select(b => $"0x{b:X2}"));
                        RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                        RXHEXrichTextBox.AppendText(hexString + "\n");
                    }
                }));
            }
            else
            {
                if (args.IsRxDir)
                {
                    RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                    RXHEXrichTextBox.SelectionLength = 0;
                    RXHEXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                    RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXHEXrichTextBox.AppendText($"{args.Time} [Recv]--> \n");

                    RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                    RXHEXrichTextBox.SelectionColor = Color.HotPink;
                    string hexString = string.Join(" ", args.Data.Take(args.DataCount).Select(b => $"0x{b:X2}"));
                    RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXHEXrichTextBox.AppendText(hexString + "\n");
                }
                else
                {
                    RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                    RXHEXrichTextBox.SelectionLength = 0;
                    RXHEXrichTextBox.SelectionColor = Color.DeepSkyBlue;
                    RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXHEXrichTextBox.AppendText($"{args.Time} [Send]--> \n");

                    RXHEXrichTextBox.SelectionStart = RXHEXrichTextBox.TextLength;
                    RXHEXrichTextBox.SelectionColor = Color.Tomato;
                    string hexString = string.Join(" ", args.Data.Take(args.DataCount).Select(b => $"0x{b:X2}"));
                    RXHEXrichTextBox.SelectionFont = new Font(RXrichTextBox.Font, FontStyle.Bold);
                    RXHEXrichTextBox.AppendText(hexString + "\n");
                }
            }
        }

        public void CounterTextBox_TextChange(NetworkEventArgs args)
        {
            if(args.IsRxDir)
            {
                RXCount += args.DataCount;
            }
            else
            {
                TXCount += args.DataCount;
            }

            if (CounterTextBox.InvokeRequired)
            {
                CounterTextBox.Invoke(new MethodInvoker(delegate
                {
                    CounterTextBox.Text = $"RX:{RXCount}\r\nTX:{TXCount}";
                }));
            }
            else
            {
                CounterTextBox.Text = $"RX:{RXCount}\r\nTX:{TXCount}";
            }
        }

        private void AddIntoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConditionTextBox.Text) == true || string.IsNullOrEmpty(OutputTextBox.Text) == true)
            {
                MessageBox.Show($"错误: 不允许空条件或空输出", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConditionOutput obj = new ConditionOutput(ConditionHexCheckBox.Checked,ConditionTextBox.Text,OutputHexCheckBox.Checked,OutputTextBox.Text);
            try
            {
                ConditionOutputDataBase.Add(obj);
            }
            catch (Exception)
            {
                MessageBox.Show($"错误: 不允许条件重复", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConditionOutputListShowFlush();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // 先判断是否有选中行
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 至少选择一行", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 因为要删除集合中的对象，避免修改集合时出现问题，先缓存要删除的对象列表
            List<ConditionOutput> itemsToRemove = new List<ConditionOutput>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                // 绑定的对象在 DataBoundItem 属性中
                if (row.DataBoundItem is ConditionOutput item)
                {
                    itemsToRemove.Add(item);
                }
            }

            // 从数据库集合删除
            foreach (var item in itemsToRemove)
            {
                ConditionOutputDataBase.DataBase.Remove(item);
            }

            ConditionOutputListShowFlush();
        }

        private void ConditionOutputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(ConditionOutputCheckBox.Checked)
            {
                ConditionOutputDelay.Enabled = false;
                AddIntoButton.Enabled = false;
                DeleteButton.Enabled = false;
                EditConditionButton.Enabled = false;
                ConditionOutputControllerObj = new ConditionOutputController(TCPCOMHandleObj,NetworkStaus,ConditionOutputDelay);
                ConditionOutputControllerObj.StartConditionOutput();
            }
            else
            {
                ConditionOutputControllerObj.StopConditionOutput();
                ConditionOutputDelay.Enabled = true;
                AddIntoButton.Enabled = true;
                DeleteButton.Enabled = true;
                EditConditionButton.Enabled = true;
            }
        }

        private void COMForwardButton_Click(object sender, EventArgs e)
        {
            if(COMComboBox.SelectedItem == null)
            {
                MessageBox.Show($"错误: 未选择串口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.COMForwarding;
            COMForwardingObj = new COMForwarding((string)COMComboBox.SelectedItem, TCPCOMHandleObj,NetworkStaus);
            if(COMForwardingObj.StartComForward())
            {
                NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.OnCOMForward;
            }
            else
            {
                NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.OffCOMForward;
            }
        }

        private async void UartSendButton_Click(object sender, EventArgs e)
        {
            if ((int)NetworkStaus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await TCPCOMHandleObj.TCPTextSend(SendRichTextBox.Text);
        }

        private async void UartHexSendButton_Click(object sender, EventArgs e)
        {
            if ((int)NetworkStaus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var bytes = HexConverter.ParseHexString(SendRichTextBox.Text);
                await TCPCOMHandleObj.TCPHexSend(bytes);
            }
            catch
            {
                MessageBox.Show($"错误: 字符串转HEX不合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            RXrichTextBox.Clear();
            RXHEXrichTextBox.Clear();
        }

        private void DataClear_Click(object sender, EventArgs e)
        {
            RXCount = 0;
            TXCount = 0;
            CounterTextBox.Text = $"RX:{RXCount}\r\nTX:{TXCount}";
        }

        private void ConditionOutputDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void UartFIleSendButton_Click(object sender, EventArgs e)
        {
            if ((int)NetworkStaus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var FilePath = openFileDialog1.FileName;
                    Stream FileStream = openFileDialog1.OpenFile();

                    byte[] FileData = new byte[2048];
                    int Count = 1;
                    while (Count > 0) 
                    {
                        Count = FileStream.Read(FileData, 0, FileData.Length);
                        if(Count > 0)
                        {
                            if ((int)NetworkStaus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
                            {
                                MessageBox.Show($"发生错误: 连接终止", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            TCPCOMHandleObj.TCPHexSend(FileData, Count);
                        }
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"安全错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OffCOMForwardButton_Click(object sender, EventArgs e)
        {
            COMForwardingObj.StopComForward();
            COMForwardingObj = null;
            NetworkStaus.CurrentStatus = SystemConnectStatus.SysStatus.OffCOMForward;
        }

        private async void QuickSendButton_Click(object sender, EventArgs e)
        {
            if ((int)NetworkStaus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Button QuickSendButton = (Button)sender;
            string buttonName = QuickSendButton.Name;
            if (buttonName.StartsWith("QuickSend"))
            {
                if (int.TryParse(buttonName.Substring("QuickSend".Length), out int index))
                {
                    if (index >= 1 && index <= 9)
                    {
                        TextBox currentTextBox = null;
                        switch (index)
                        {
                            case 1: currentTextBox = QuickSendTextBox1; break;
                            case 2: currentTextBox = QuickSendTextBox2; break;
                            case 3: currentTextBox = QuickSendTextBox3; break;
                            case 4: currentTextBox = QuickSendTextBox4; break;
                            case 5: currentTextBox = QuickSendTextBox5; break;
                            case 6: currentTextBox = QuickSendTextBox6; break;
                            case 7: currentTextBox = QuickSendTextBox7; break;
                            case 8: currentTextBox = QuickSendTextBox8; break;
                            case 9: currentTextBox = QuickSendTextBox9; break;
                        }

                        if (currentTextBox != null)
                        {
                            CheckBox currentCheckBox = null;

                            switch (index)
                            {
                                case 1: currentCheckBox = QuickSendHexCheckBox1; break;
                                case 2: currentCheckBox = QuickSendHexCheckBox2; break;
                                case 3: currentCheckBox = QuickSendHexCheckBox3; break;
                                case 4: currentCheckBox = QuickSendHexCheckBox4; break;
                                case 5: currentCheckBox = QuickSendHexCheckBox5; break;
                                case 6: currentCheckBox = QuickSendHexCheckBox6; break;
                                case 7: currentCheckBox = QuickSendHexCheckBox7; break;
                                case 8: currentCheckBox = QuickSendHexCheckBox8; break;
                                case 9: currentCheckBox = QuickSendHexCheckBox9; break;
                            }

                            if (currentCheckBox.Checked)
                            {
                                try
                                {
                                    var bytes = HexConverter.ParseHexString(currentTextBox.Text);
                                    await TCPCOMHandleObj.TCPHexSend(bytes);
                                }
                                catch
                                {
                                    MessageBox.Show($"错误: 字符串转HEX不合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                await TCPCOMHandleObj.TCPTextSend(currentTextBox.Text);
                            }
                        }
                    }
                }
            }
        }

        [DataContract]
        public class QuickSendConfig
        {
            [DataMember]
            public Dictionary<int, QuickSendItem> Items { get; set; } = new Dictionary<int, QuickSendItem>();
        }

        [DataContract]
        public class QuickSendItem
        {
            [DataMember]
            public string Text { get; set; }
            [DataMember]
            public bool IsHex { get; set; }
        }

        private static string FilePath
        {
            get
            {
                var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var dllDir = Path.GetDirectoryName(dllPath);
                return Path.Combine(dllDir, "QuickSend.json");
            }
        }

        private void SaveConfigAsync(QuickSendConfig config)
        {
            var serializer = new DataContractJsonSerializer(typeof(QuickSendConfig));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, config);
                string json = Encoding.UTF8.GetString(ms.ToArray());
                File.WriteAllText(FilePath, json);
            }
        }

        private QuickSendConfig LoadConfig()
        {
            if (!File.Exists(FilePath))
            {
                return new QuickSendConfig();
            }

            string json = File.ReadAllText(FilePath);
            var serializer = new DataContractJsonSerializer(typeof(QuickSendConfig));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (QuickSendConfig)serializer.ReadObject(ms);
            }
        }

        private void LoadConfigToQuickSend()
        {
            var config = LoadConfig();

            // 遍历所有可能的1-9编号
            for (int i = 1; i <= 9; i++)
            {
                if (config.Items.ContainsKey(i))
                {
                    var item = config.Items[i];

                    // 根据编号找到对应的TextBox，并设置内容
                    if (this.Controls.Find($"QuickSendTextBox{i}", true)
                                    .FirstOrDefault() is TextBox textBox)
                    {
                        textBox.Text = item.Text;
                    }

                    // 找到对应的CheckBox，并设置选中状态
                    if (this.Controls.Find($"QuickSendHexCheckBox{i}", true)
                                    .FirstOrDefault() is CheckBox checkBox)
                    {
                        checkBox.Checked = item.IsHex;
                    }
                }
            }
        }

        private void QuickSendTextBox_TextChanged(object sender, EventArgs e)
        {
            var config = LoadConfig();

            if (!(sender is TextBox txtBox)) return;

            int index = 0;
            string name = txtBox.Name;
            if (name.StartsWith("QuickSendTextBox"))
            {
                int.TryParse(name.Substring("QuickSendTextBox".Length), out index);
            }

            if (index >= 1 && index <= 9)
            {
                if (!config.Items.ContainsKey(index))
                {
                    config.Items[index] = new QuickSendItem();
                }
                config.Items[index].Text = txtBox.Text;
                SaveConfigAsync(config);
            }
        }

        private void QuickSendHexCheckBox_Changed(object sender, EventArgs e)
        {
            var config = LoadConfig();

            if (!(sender is CheckBox chkBox)) return;

            int index = 0;
            string name = chkBox.Name;
            if (name.StartsWith("QuickSendHexCheckBox"))
            {
                int.TryParse(name.Substring("QuickSendHexCheckBox".Length), out index);
            }

            if (index >= 1 && index <= 9)
            {
                if (!config.Items.ContainsKey(index))
                {
                    config.Items[index] = new QuickSendItem();
                }
                config.Items[index].IsHex = chkBox.Checked;
                SaveConfigAsync(config);
            }
        }


        private void EditConditionButton_Click(object sender, EventArgs e)
        {
            // 先判断是否有选中行
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rowIndex = dataGridView1.SelectedRows[0].Index;
            ConditionOutput obj = new ConditionOutput(ConditionHexCheckBox.Checked, ConditionTextBox.Text, OutputHexCheckBox.Checked, OutputTextBox.Text)
            {
                Priority = ConditionOutputDataBase.DataBase[rowIndex].Priority
            };
            ConditionOutputDataBase.DataBase[rowIndex] = obj;
            ConditionOutputListShowFlush();
        }

        private void ReadConditionButton_Click(object sender, EventArgs e)
        {
            // 先判断是否有选中行
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rowIndex = dataGridView1.SelectedRows[0].Index;

            ConditionHexCheckBox.Checked = ConditionOutputDataBase.DataBase[rowIndex].IsInputHex;
            ConditionTextBox.Text = ConditionOutputDataBase.DataBase[rowIndex].InputValue;
            OutputHexCheckBox.Checked = ConditionOutputDataBase.DataBase[rowIndex].IsOutputHex;
            OutputTextBox.Text = ConditionOutputDataBase.DataBase[rowIndex].OutputValue;
        }

        private void TextSaveButton_Click(object sender, EventArgs e)
        {
            // 设置文件类型过滤器，比如文本文件
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // 设置默认文件名（可选）
            saveFileDialog1.FileName = "SaveFile.txt";

            // 弹出保存对话框
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string filePath = saveFileDialog1.FileName;

                // 示例：将一些文本保存到文件
                string content = RXrichTextBox.Text;
                File.WriteAllText(filePath, content);
            }
        }

        private void HexSaveButton_Click(object sender, EventArgs e)
        {
            // 设置文件类型过滤器，比如文本文件
            saveFileDialog1.Filter = "Text files (*.bin)|*.bin|All files (*.*)|*.*";

            // 设置默认文件名（可选）
            saveFileDialog1.FileName = "SaveFile.bin";

            // 弹出保存对话框
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string filePath = saveFileDialog1.FileName;

                // 示例：将一些文本保存到文件
                string content = RXrichTextBox.Text;
                File.WriteAllText(filePath, content);
            }
        }

        private void ConditionUpButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rowIndex = dataGridView1.SelectedRows[0].Index;
            int upIndex = rowIndex - 1;

            if (upIndex < 0)
            {
                MessageBox.Show("错误: 已达最高优先级", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 交换 Priority
            ConditionOutput obj = ConditionOutputDataBase.DataBase[rowIndex];
            ConditionOutput obj_up = ConditionOutputDataBase.DataBase[upIndex];

            (obj_up.Priority, obj.Priority) = (obj.Priority, obj_up.Priority);

            // 交换数据源中的对象位置，实现顺序的调换
            ConditionOutput tmp = ConditionOutputDataBase.DataBase[upIndex];
            ConditionOutputDataBase.DataBase[upIndex] = obj;
            ConditionOutputDataBase.DataBase[rowIndex] = tmp;

            // 刷新显示
            dataGridView1.ClearSelection(); // 先清除所有选择
            dataGridView1.Rows[upIndex].Selected = true;
            ConditionOutputDataBase.SaveToFile();
            ConditionOutputListShowFlush();
        }

        private void ConditionDownButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rowIndex = dataGridView1.SelectedRows[0].Index;
            int downIndex = rowIndex + 1;

            if (downIndex >= ConditionOutputDataBase.DataBase.Count)
            {
                MessageBox.Show("错误: 已达最低优先级", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 交换 Priority
            ConditionOutput obj = ConditionOutputDataBase.DataBase[rowIndex];
            ConditionOutput obj_down = ConditionOutputDataBase.DataBase[downIndex];

            (obj_down.Priority, obj.Priority) = (obj.Priority, obj_down.Priority);

            // 交换在数据源中的位置
            ConditionOutput tmp = ConditionOutputDataBase.DataBase[downIndex];
            ConditionOutputDataBase.DataBase[downIndex] = obj;
            ConditionOutputDataBase.DataBase[rowIndex] = tmp;

            // 刷新显示
            dataGridView1.ClearSelection(); // 先清除所有选择
            dataGridView1.Rows[downIndex].Selected = true;
            ConditionOutputDataBase.SaveToFile();
            ConditionOutputListShowFlush();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void SerialPinSyncCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SerialPinSyncCheckBox.Checked)
            {
                COMForwardingObj.StartSyncPin();
            }
            else
            {
                COMForwardingObj.StopSyncPin();
            }
        }
    }

    public class SystemConnectStatus
    {
        public enum SysStatus
        {
            Disconnected = 0,
            Connecting = 1,
            Connected = 2,
            OffCOMForward = 3,
            COMForwarding = 4,
            OnCOMForward = 5
        };

        private bool ContainsDelegate(Delegate del, Delegate target)
        {
            if (del == null) return false;
            foreach (var d in del.GetInvocationList())
            {
                if (d == target)
                    return true;
            }
            return false;
        }

        public delegate void StatusChangedHandler(object sender, SysStatus e);

        public event StatusChangedHandler StatusChanged_;

        public event StatusChangedHandler StatusChanged
        {
            add
            {
                if (StatusChanged_ == null || !ContainsDelegate(StatusChanged_, value))
                {
                    StatusChanged_ += value; 
                }
            }
            remove
            {
                if (StatusChanged_ != null || ContainsDelegate(StatusChanged_, value))
                {
                    StatusChanged_ -= value;
                }
            }
        }

        protected virtual void OnStatusChange(SysStatus e)
        {
            try
            {
                StatusChanged_?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SubscribeEvent(Action<SysStatus> Action)
        {
            StatusChanged += (sender, e) =>
            {
                Action(e);
            };
        }

        public void UnsubscribeEvent(Action<SysStatus> Action)
        {
            StatusChanged -= (sender, e) =>
            {
                Action(e);
            };
        }

        private SysStatus currentStatus;

        public SysStatus CurrentStatus
        {
            get
            {
                return currentStatus;
            }
            set
            {
                if (currentStatus != value)
                {
                    currentStatus = value;
                    OnStatusChange(currentStatus);
                }
            }
        }

        public SystemConnectStatus()
        {
            CurrentStatus = SysStatus.Disconnected;
        }

    }

    public class HexConverter
    {
        public static byte[] ParseHexString(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<byte>();

            // 检测是否包含 "0x" 或 "0X" 前缀格式
            if (input.IndexOf("0x", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return ParseHexWithPrefix(input);
            }

            // 标准十六进制格式处理
            return ParseStandardHex(input);
        }

        private static byte[] ParseStandardHex(string input)
        {
            var hexChars = input.Where(IsHexChar).ToArray();
            int charCount = hexChars.Length;

            if (charCount == 0)
                return Array.Empty<byte>();
            if (charCount % 2 != 0)
                throw new FormatException("Hexadecimal string must have an even number of digits");

            byte[] result = new byte[charCount / 2];
            for (int i = 0; i < result.Length; i++)
            {
                int highNibble = CharToNibble(hexChars[2 * i]);
                int lowNibble = CharToNibble(hexChars[2 * i + 1]);
                result[i] = (byte)((highNibble << 4) | lowNibble);
            }

            return result;
        }

        private static byte[] ParseHexWithPrefix(string input)
        {
            List<byte> result = new List<byte>();
            int index = 0;

            while (index < input.Length)
            {
                // 检查 "0x" 或 "0X" 前缀
                if (index < input.Length - 2 &&
                    input[index] == '0' &&
                    (input[index + 1] == 'x' || input[index + 1] == 'X') &&
                    IsHexChar(input[index + 2]))
                {
                    index += 2; // 跳过 "0x" 前缀

                    // 收集连续的十六进制字符
                    int end = index;
                    while (end < input.Length && IsHexChar(input[end])) end++;

                    string hexBlock = input.Substring(index, end - index);
                    index = end; // 更新索引

                    // 处理十六进制块
                    ProcessHexBlock(hexBlock, result);
                }
                else
                {
                    index++; // 跳过无效字符
                }
            }

            return result.ToArray();
        }

        private static void ProcessHexBlock(string hexBlock, List<byte> result)
        {
            int charCount = hexBlock.Length;

            // 处理奇数长度的情况
            if (charCount % 2 != 0)
            {
                // 尝试将第一个字符作为高位（前面补0）
                result.Add((byte)CharToNibble(hexBlock[0]));
                hexBlock = hexBlock.Substring(1);
                charCount = hexBlock.Length;
            }

            // 处理剩余字符
            for (int i = 0; i < charCount; i += 2)
            {
                int highNibble = CharToNibble(hexBlock[i]);
                int lowNibble = CharToNibble(hexBlock[i + 1]);
                result.Add((byte)((highNibble << 4) | lowNibble));
            }
        }

        private static bool IsHexChar(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'A' && c <= 'F') ||
                   (c >= 'a' && c <= 'f');
        }

        private static int CharToNibble(char c)
        {
            if (c >= '0' && c <= '9')
                return c - '0';
            else if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;
            else if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;
            else
                throw new FormatException($"Invalid hex character: {c}");
        }
    }

    public class NetworkEventArgs : EventArgs
    {
        public string Time { get; private set; }
        public Byte[] Data { get; private set; }
        public int DataCount { get;private set; }
        public bool IsRxDir { get; private set; }
        public NetworkEventArgs(Byte[] data, int Number,bool RxDir)
        {
            DateTime now = DateTime.Now;
            Time = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Data = data;
            DataCount = Number;
            IsRxDir = RxDir;
        }
    }

    public class TCPCOMHandle
    {
        private readonly Stream TCPStream;

        private readonly SystemConnectStatus SystemStatus;
        
        private volatile bool NetworkSwitch = false;

        private Thread ReceiveThread;

        private readonly CancellationTokenSource ReceiveThreadCTS = new CancellationTokenSource();

        public delegate void MsgReceivedHandler(object sender, NetworkEventArgs e);

        public event MsgReceivedHandler MsgReceivedEvent_;



        private bool ContainsDelegate(Delegate del, Delegate target)
        {
            if (del == null) return false;
            foreach (var d in del.GetInvocationList())
            {
                if (d == target)
                    return true;
            }
            return false;
        }

        public event MsgReceivedHandler MsgReceivedEvent
        {
            add
            {
                if (MsgReceivedEvent_ == null || !ContainsDelegate(MsgReceivedEvent_, value))
                {
                    MsgReceivedEvent_ += value;
                }
            }
            remove
            {
                if (MsgReceivedEvent_ != null || ContainsDelegate(MsgReceivedEvent_, value))
                {
                    MsgReceivedEvent_ -= value;
                }
            }
        }

        public TCPCOMHandle(Stream stream,SystemConnectStatus status)
        {
            TCPStream = stream;
            SystemStatus = status;
        }

        private static long GetCurrentTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        protected virtual void OnMessageReceived(NetworkEventArgs e)
        {
            try
            {
                MsgReceivedEvent_?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SubscribeEvent(Action<NetworkEventArgs> Action)
        {
            MsgReceivedEvent += (sender, e) =>
            {
                Action(e);
            };
        }

        public void UnsubscribeEvent(Action<NetworkEventArgs> Action)
        {
            MsgReceivedEvent -= (sender, e) =>
            {
                Action(e);
            };
        }


        public async Task<bool> StartTCPCOM()
        {
            string Command = "{ESPWUART}";
            byte[] CommandBytes = Encoding.UTF8.GetBytes(Command);
            byte[] ReadBytes = new byte[256];

            try
            {
                await TCPStream.WriteAsync(CommandBytes, 0, CommandBytes.Length);
            }
            catch
            {
                return false;
            }

            int timeoutMilliseconds = 3000;
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    await TCPStream.ReadAsync(ReadBytes, 0, 1, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }

            if (ReadBytes[0] == 'y')
            {
                NetworkSwitch = true;
                ReceiveThread = new Thread(new ThreadStart(TCPReceiveThread));
                ReceiveThread.Start();
                SystemStatus.SubscribeEvent(StatusChangeResponse);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void StopTCPCOM()
        {
            if(NetworkSwitch)
            {
                NetworkSwitch = false;
                MsgReceivedEvent_ = null;
                string Command = "{ESPWUART}";
                byte[] CommandBytes = Encoding.UTF8.GetBytes(Command);
                SystemStatus.UnsubscribeEvent(StatusChangeResponse);
                SystemStatus.CurrentStatus = SystemConnectStatus.SysStatus.Disconnected;
                ReceiveThreadCTS.Cancel();
                ReceiveThread.Join();
                try
                {
                    await TCPStream.WriteAsync(CommandBytes, 0, CommandBytes.Length);
                }
                catch
                {

                }
            }
        }

        public void StatusChangeResponse(SystemConnectStatus.SysStatus status)
        {
            if((int)status < (int)SystemConnectStatus.SysStatus.Connected)
            {
                StopTCPCOM();
            }
        }

        public async Task TCPTextSend(string TextData)
        {
            if((int)SystemStatus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NetworkEventArgs Args = new NetworkEventArgs(ASCIIEncoding.UTF8.GetBytes(TextData), TextData.Length, false);

            int timeoutMilliseconds = 3000;
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    await TCPStream.WriteAsync(Args.Data, 0, Args.DataCount);
                    Console.WriteLine($"Timestamp:{GetCurrentTimestamp()}");
                    OnMessageReceived(Args);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        public async Task TCPHexSend(Byte[] Data)
        {
            if ((int)SystemStatus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NetworkEventArgs Args = new NetworkEventArgs(Data, Data.Length, false);

            int timeoutMilliseconds = 3000;
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    await TCPStream.WriteAsync(Args.Data, 0, Args.DataCount);
                    OnMessageReceived(Args);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        public async Task TCPHexSendNoFeedBack(Byte[] Data)
        {
            if ((int)SystemStatus.CurrentStatus < (int)SystemConnectStatus.SysStatus.Connected)
            {
                MessageBox.Show($"发生错误: 未连接设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NetworkEventArgs Args = new NetworkEventArgs(Data, Data.Length, false);

            int timeoutMilliseconds = 3000;
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    await TCPStream.WriteAsync(Args.Data, 0, Args.DataCount);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        public async void TCPHexSend(Byte[] Data,int WriteNumber)
        {
            NetworkEventArgs Args = new NetworkEventArgs(Data, WriteNumber, false);

            int timeoutMilliseconds = 3000;
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    await TCPStream.WriteAsync(Args.Data, 0, WriteNumber);
                    OnMessageReceived(Args);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        private async void TCPReceiveThread()
        {
            byte[] Data = new byte[1024];
            NetworkEventArgs args;
            int DataCount;
            try
            {
                while (NetworkSwitch)
                {
                    DataCount = await TCPStream.ReadAsync(Data,0, Data.Length, ReceiveThreadCTS.Token);
                    args = new NetworkEventArgs(Data, DataCount,true);
                    OnMessageReceived(args);
                }
            }
            catch
            {
                StopTCPCOM();
            }
        }
    }

    public class COMForwarding
    {
        private readonly TCPCOMHandle TCPCOM;
        private readonly SystemConnectStatus SystemStatus;
        private SerialPort COM;
        private volatile bool isRunning = false;
        public COMForwarding(string com,TCPCOMHandle TCPClass, SystemConnectStatus status)
        {
            SystemStatus = status;
            COM = new SerialPort
            {
                PortName = com,
                BaudRate = 2000000,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None
            };
            COM.DataReceived += SerialPort_DataReceived;
            TCPCOM = TCPClass;
            try
            {
                COM.Open();
            }
            catch (UnauthorizedAccessException)
            {
                COM = null;
                TCPCOM = null;
                MessageBox.Show($"错误: 端口被占用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool StartComForward()
        {
            if(TCPCOM != null && COM != null)
            {
                isRunning = true;
                TCPCOM.SubscribeEvent(NetToCom);
                SystemStatus.SubscribeEvent(StatusChangeResponse);
                return true;
            }
            return false;
        }

        public void StopComForward()
        {
            if (isRunning)
            {
                isRunning = false;
                TCPCOM.UnsubscribeEvent(NetToCom);
                SystemStatus.UnsubscribeEvent(StatusChangeResponse);
                COM.Dispose();
                COM = null;
            }
        }

        public bool StartSyncPin()
        {
            if (TCPCOM != null && COM != null)
            {
                COM.PinChanged += Port_PinChanged;
                return true;
            }
            return false;
        }

        public bool StopSyncPin()
        {
            try
            {
                if(COM != null)
                {
                    COM.PinChanged -= Port_PinChanged;
                }
            }
            catch
            {

            }
            return true;
        }

        private async void Port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPort Port = (SerialPort)sender;
            int CTSPin;
            int DTRPin;
            switch (e.EventType)
            {
                case SerialPinChange.CtsChanged:
                    CTSPin = Port.CtsHolding ? 1 : 0;
                    DTRPin = Port.DsrHolding ? 1 : 0;
                    Console.WriteLine("CTS状态发生变化");
                    break;
                case SerialPinChange.DsrChanged:
                    CTSPin = Port.CtsHolding ? 1 : 0;
                    DTRPin = Port.DsrHolding ? 1 : 0;
                    Console.WriteLine("DSR状态发生变化");
                    break;
                default:
                    return;
            }

            try
            {
                byte[] SyncData = Encoding.ASCII.GetBytes($"GPIOADJUST{CTSPin}{DTRPin}");
                await TCPCOM.TCPHexSendNoFeedBack(SyncData);
            }
            catch
            {
                
            }
        }

        public void StatusChangeResponse(SystemConnectStatus.SysStatus status)
        {
            if((int)status < (int)SystemConnectStatus.SysStatus.OnCOMForward)
            {
                StopComForward();
                StopSyncPin();
            }
        }

        private static long GetCurrentTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        private async void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine($"SerialRecvTimestamp:{GetCurrentTimestamp()}");
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting(); // 读取所有可用数据
            if (isRunning)
            {
                try
                {
                    await TCPCOM.TCPTextSend(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"COM:{ex}");
                }
            }
        }

        public Task WriteAsync(byte[] data,int index,int count)
        {
            return Task.Run(() =>
            {
                // 这是同步写方法
                COM.Write(data, index, count);
            });
        }

        private async void NetToCom(NetworkEventArgs args)
        {
            if(isRunning)
            {
                try
                {
                    if(args.IsRxDir)
                    {
                        await WriteAsync(args.Data, 0, args.DataCount);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"COM:{e}");
                }
            }
        }
    }

    public class ConditionOutputDataBase
    {
        public static BindingList<ConditionOutput> DataBase = new BindingList<ConditionOutput>();

        private static string FilePath
        {
            get
            {
                var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var dllDir = Path.GetDirectoryName(dllPath);
                return Path.Combine(dllDir, "ConditionDatabase.json");
            }
        }

        public static void SaveToFile()
        {
            try
            {
                using (FileStream fs = File.Create(FilePath))
                {
                    var settings = new DataContractJsonSerializerSettings
                    {
                        UseSimpleDictionaryFormat = true
                    };

                    var serializer = new DataContractJsonSerializer(
                        typeof(BindingList<ConditionOutput>),
                        settings
                    );

                    serializer.WriteObject(fs, DataBase);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"保存失败: {ex.Message}");
            }
        }

        public static void LoadFromFile()
        {
            if (!File.Exists(FilePath)) return;

            try
            {
                using (FileStream fs = File.OpenRead(FilePath))
                {
                    var settings = new DataContractJsonSerializerSettings
                    {
                        UseSimpleDictionaryFormat = true
                    };

                    var serializer = new DataContractJsonSerializer(
                        typeof(BindingList<ConditionOutput>),
                        settings
                    );

                    DataBase = (BindingList<ConditionOutput>)serializer.ReadObject(fs);
                }

                foreach (var item in DataBase)
                {
                    item.RebuildHexValues();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"加载失败: {ex.Message}");
                DataBase = new BindingList<ConditionOutput>();
            }
        }

        public static void Add(ConditionOutput ConditionOutputObj)
        {
            // 检查是否已存在相同模式
            bool exists = DataBase.Any(item =>
                item.InputHexValue.SequenceEqual(ConditionOutputObj.InputHexValue));

            if (exists)
            {
                throw new Exception("重复条件: " +
                    BitConverter.ToString(ConditionOutputObj.InputHexValue));
            }

            if (ConditionOutputObj.Priority == null)
            {
                ConditionOutputObj.Priority = DataBase.Count;
            }
            DataBase.Add(ConditionOutputObj);
            SaveToFile();
        }

        public static ConditionOutput Search(byte[] Bytes,int Length)
        {
            // 1. 按长度降序->优先级降序排序
            var sortedList = DataBase
                .OrderByDescending(item => item.InputHexValue.Length)
                .ThenByDescending(item => item.Priority ?? int.MinValue)
                .ToList();

            // 2. 使用精确匹配算法
            foreach (var item in sortedList)
            {
                if (IsExactMatch(Bytes,Length,item.InputHexValue))  // 改为精确匹配
                {
                    return item;
                }
            }
            return null;
        }

        // 新增精确匹配方法
        private static bool IsExactMatch(byte[] Source,int SourceLength, byte[] Pattern)
        {
            if (Source == null || Pattern.Length == 0)
                return true;

            if (SourceLength != Pattern.Length)
                return false;

            for (int i = 0; i < SourceLength; i++)
            {
                if (Source[i] != Pattern[i])
                    return false;
            }
            return true;
        }
    }

    [DataContract]
    public class ConditionOutput
    {
        [DataMember]
        public int? Priority { get; set; }
        [DataMember]
        public string InputValue { get; set; }
        [DataMember]
        public string OutputValue { get; set; }
        [IgnoreDataMember]
        public byte[] InputHexValue { get; set; }
        [IgnoreDataMember]
        public byte[] OutputHexValue { get; set; }
        [DataMember]
        public bool IsInputHex { get; set; }
        [DataMember]
        public bool IsOutputHex { get; set; }

        public ConditionOutput()
        {

        }

        public ConditionOutput(bool InputIsBytes, string Input, bool OutputIsBytes, string Output)
        {
            Priority = null;

            IsInputHex = InputIsBytes;
            try
            {
                if (InputIsBytes)
                {
                    InputValue = Input;
                    InputHexValue = HexConverter.ParseHexString(Input);
                }
                else
                {
                    InputValue = Input;
                    InputHexValue = Encoding.ASCII.GetBytes(Input);
                }
            }
            catch
            {
                MessageBox.Show($"错误: 字符串转HEX不合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            IsOutputHex = OutputIsBytes;
            try
            {
                if (OutputIsBytes)
                {
                    OutputValue = Output;
                    OutputHexValue = HexConverter.ParseHexString(Output);
                }
                else
                {
                    OutputValue = Output;
                    OutputHexValue = Encoding.ASCII.GetBytes(Output);
                }
            }
            catch
            {
                MessageBox.Show($"错误: 字符串转HEX不合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RebuildHexValues()
        {
            try
            {
                if (IsInputHex)
                {
                    InputHexValue = HexConverter.ParseHexString(InputValue);
                }
                else
                {
                    InputHexValue = Encoding.ASCII.GetBytes(InputValue);
                }

                if (IsOutputHex)
                {
                    OutputHexValue = HexConverter.ParseHexString(OutputValue);
                }
                else
                {
                    OutputHexValue = Encoding.ASCII.GetBytes(OutputValue);
                }
            }
            catch
            {
                MessageBox.Show($"错误: 字符串转HEX不合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ConditionOutputController
    {
        private readonly TCPCOMHandle TCPCOM;
        private readonly SystemConnectStatus SystemStatus;
        private bool ConditionOutputSwitch = false;
        private readonly TextBox Delay;
        private long FrontTimestamp = 0;

        public ConditionOutputController(TCPCOMHandle TCPClass, SystemConnectStatus status,TextBox DelayTextBox)
        {
            TCPCOM = TCPClass;
            SystemStatus = status;
            Delay = DelayTextBox;
        }

        public void StartConditionOutput()
        {
            if(!ConditionOutputSwitch)
            {
                ConditionOutputSwitch = true;
                TCPCOM.SubscribeEvent(ReceiveMessageDetect);
                SystemStatus.SubscribeEvent(StatusChangeResponse);
            }
        }

        public void StopConditionOutput()
        {
            if (ConditionOutputSwitch)
            {
                ConditionOutputSwitch = false;
                TCPCOM.UnsubscribeEvent(ReceiveMessageDetect);
                SystemStatus.UnsubscribeEvent(StatusChangeResponse);
            }
        }

        public void StatusChangeResponse(SystemConnectStatus.SysStatus status)
        {
            if((int)status < (int)SystemConnectStatus.SysStatus.Connected)
            {
                StopConditionOutput();
            }
        }

        public async void ReceiveMessageDetect(NetworkEventArgs args)
        {
            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - FrontTimestamp < int.Parse(Delay.Text))
            {
                return;
            }

            if (ConditionOutputSwitch && args.IsRxDir)
            {
                ConditionOutput output = ConditionOutputDataBase.Search(args.Data,args.DataCount);
                if(output != null)
                {
                    await TCPCOM.TCPHexSend(output.OutputHexValue);
                    FrontTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                }
            }
        }
    }
}
