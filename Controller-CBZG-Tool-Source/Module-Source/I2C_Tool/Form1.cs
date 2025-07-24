using ModNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace I2C_Tool
{
    public partial class Form1 : Form
    {
        NetworkStream Stream { get; set; }
        IMod Module { get; set; }

        private readonly I2C_Tool_Class Tool = null;

        public Form1(NetworkStream _stream, IMod mod)
        {
            InitializeComponent();
            Stream = _stream;
            Module = mod;
            Tool = new I2C_Tool_Class(Stream);
            RegisterGridView.DoubleBufferedDataGirdView(true);
            ColorBox.AddColor("红", Color.Red);
            ColorBox.AddColor("绿", Color.Lime);
            ColorBox.AddColor("蓝", Color.DeepSkyBlue);
            ColorBox.AddColor("黄", Color.Yellow);
            ColorBox.AddColor("黑", Color.Black);
            ColorBox.AddColor("粉", Color.HotPink);
            ColorBox.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SystemStatus.StatusChanged += Controls_Enable_Change;
            SystemStatus.StatusChanged += AutoRegFlush_Close;
            SystemStatus.StatusChanged += IICToolSwitch_Close;

            Controls_Enable_Change(null,null);

            DeviceComboBox.DataSource = I2C_Device_Group.I2C_Devices;

            ListDeviceAddrComboBox.DataSource = I2C_Device_Group.I2C_Devices;

            DevComboBox.DataSource = I2C_Device_Group.I2C_Devices;

            DialogDeviceAddrComboBox.DataSource = I2C_Device_Group.I2C_Devices;

            WaveList.DataSource = Wave_Group.WaveUnits;
            WaveList.DisplayMember = "WaveName";
            WaveList.ValueMember = "WaveName";

            ColorBox.SelectedIndex = 0;

            I2C_Register_Group.LoadRegistersFromJson();

            RegisterGridView.DataSource = I2C_Register_Group.I2C_Registers;
            RegisterGridView.Invalidate();
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
            {
                await Tool.Stop_I2C_Tool();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemStatus.StatusChanged -= Controls_Enable_Change;
            SystemStatus.StatusChanged -= AutoRegFlush_Close;
            SystemStatus.StatusChanged -= IICToolSwitch_Close;
            foreach (WaveUnit Wave in Wave_Group.WaveUnits)
            {
                WavePictureBox1.RemoveWave(Wave);
            }
            WavePictureBox1.Dispose();
            Wave_Group.WaveUnits.Clear();
            I2C_Register_Group.I2C_Registers.Clear();
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
                    IICToolSwitch.Enabled = true;

                    DeviceScanButton.Enabled = false;
                    DeviceComboBox.Enabled = false;
                    ListDeviceAddrComboBox.Enabled = false;
                    ListBytesNumber.Enabled = false;
                    ListRegAddr.Enabled = false;
                    AutoRegFlush.Enabled = false;
                    FlushRegFrequent.Enabled = false;
                    AddRegToListButton.Enabled = false;
                    RegDeleteButton.Enabled = false;
                    ReadListButton.Enabled = false;
                    RegEditButton.Enabled = false;

                    DevComboBox.Enabled = false;
                    WaveList.Enabled = false;
                    WaveDataHighBit.Enabled = false;
                    RegHighByteEnable.Enabled = false;
                    WaveDataLowBit.Enabled = false;
                    RegLowByteEnable.Enabled = false;
                    AddWaveButton.Enabled = false;
                    DeleteWaveButton.Enabled = false;
                    NumberPerPixel.Enabled = false;
                    WaveTimeBase.Enabled = false;
                    ColorBox.Enabled = false;
                    WaveName.Enabled = false;

                    DialogDeviceAddrComboBox.Enabled = false;
                    DialogRegAddrTextBox.Enabled = false;
                    DialogReadBytesNumber.Enabled = false;
                    DialogWriteBytes.Enabled = false;
                    DialogReadRegButton.Enabled = false;
                    I2CWriteButton.Enabled = false;
                    ClearDialogMsgButton.Enabled = false;
                    break;
                case SystemStatus.Status.Connecting:
                    IICToolSwitch.Enabled = false;

                    DeviceScanButton.Enabled = false;
                    DeviceComboBox.Enabled = false;
                    ListDeviceAddrComboBox.Enabled = false;
                    ListBytesNumber.Enabled = false;
                    ListRegAddr.Enabled = false;
                    AutoRegFlush.Enabled = false;
                    FlushRegFrequent.Enabled = false;
                    AddRegToListButton.Enabled = false;
                    RegDeleteButton.Enabled = false;
                    ReadListButton.Enabled = false;
                    RegEditButton.Enabled = false;

                    DevComboBox.Enabled = false;
                    WaveList.Enabled = false;
                    WaveDataHighBit.Enabled = false;
                    RegHighByteEnable.Enabled = false;
                    WaveDataLowBit.Enabled = false;
                    RegLowByteEnable.Enabled = false;
                    AddWaveButton.Enabled = false;
                    DeleteWaveButton.Enabled = false;
                    NumberPerPixel.Enabled = false;
                    WaveTimeBase.Enabled = false;
                    ColorBox.Enabled = false;
                    WaveName.Enabled = false;

                    DialogDeviceAddrComboBox.Enabled = false;
                    DialogRegAddrTextBox.Enabled = false;
                    DialogReadBytesNumber.Enabled = false;
                    DialogWriteBytes.Enabled = false;
                    DialogReadRegButton.Enabled = false;
                    I2CWriteButton.Enabled = false;
                    ClearDialogMsgButton.Enabled = false;
                    break;
                case SystemStatus.Status.Connected:
                    IICToolSwitch.Enabled = true;

                    DeviceScanButton.Enabled = true;
                    DeviceComboBox.Enabled = true;
                    ListDeviceAddrComboBox.Enabled = true;
                    ListBytesNumber.Enabled = true;
                    ListRegAddr.Enabled = true;
                    AutoRegFlush.Enabled = true;
                    FlushRegFrequent.Enabled = true;
                    AddRegToListButton.Enabled = true;
                    RegDeleteButton.Enabled = true;
                    ReadListButton.Enabled = true;
                    RegEditButton.Enabled = true;

                    DevComboBox.Enabled = true;
                    WaveList.Enabled = true;
                    WaveDataHighBit.Enabled = true;
                    RegHighByteEnable.Enabled = true;
                    WaveDataLowBit.Enabled = true;
                    RegLowByteEnable.Enabled = true;
                    AddWaveButton.Enabled = true;
                    DeleteWaveButton.Enabled = true;
                    NumberPerPixel.Enabled = true;
                    WaveTimeBase.Enabled = true;
                    ColorBox.Enabled = true;
                    WaveName.Enabled = true;

                    DialogDeviceAddrComboBox.Enabled = true;
                    DialogRegAddrTextBox.Enabled = true;
                    DialogReadBytesNumber.Enabled = true;
                    DialogWriteBytes.Enabled = true;
                    DialogReadRegButton.Enabled = true;
                    I2CWriteButton.Enabled = true;
                    ClearDialogMsgButton.Enabled = true;
                    break;
                case SystemStatus.Status.AutoUpdata:
                    {
                        IICToolSwitch.Enabled = true;

                        DeviceScanButton.Enabled = true;
                        DeviceComboBox.Enabled = true;
                        ListDeviceAddrComboBox.Enabled = true;
                        ListBytesNumber.Enabled = true;
                        ListRegAddr.Enabled = true;
                        AutoRegFlush.Enabled = true;
                        FlushRegFrequent.Enabled = true;
                        AddRegToListButton.Enabled = true;
                        RegDeleteButton.Enabled = true;
                        ReadListButton.Enabled = true;
                        RegEditButton.Enabled = true;

                        DevComboBox.Enabled = true;
                        WaveList.Enabled = true;
                        WaveDataHighBit.Enabled = true;
                        RegHighByteEnable.Enabled = true;
                        WaveDataLowBit.Enabled = true;
                        RegLowByteEnable.Enabled = true;
                        AddWaveButton.Enabled = true;
                        DeleteWaveButton.Enabled = true;
                        NumberPerPixel.Enabled = true;
                        WaveTimeBase.Enabled = true;
                        ColorBox.Enabled = true;
                        WaveName.Enabled = true;

                        DialogDeviceAddrComboBox.Enabled = true;
                        DialogRegAddrTextBox.Enabled = true;
                        DialogReadBytesNumber.Enabled = true;
                        DialogWriteBytes.Enabled = true;
                        DialogReadRegButton.Enabled = true;
                        I2CWriteButton.Enabled = true;
                        ClearDialogMsgButton.Enabled = true;
                        break;
                    }
                default:
                    break;
            }
        }

        private async void DeviceScanButton_Click(object sender, EventArgs e)
        {
            await Tool.ScanDevice();
        }

        private void AddRegToListButton_Click(object sender, EventArgs e)
        {
            if(ListDeviceAddrComboBox.Text == "" || ListRegAddr.Text == "")
            {
                MessageBox.Show($"错误: 未输入设备地址或寄存器地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] DevAddr;
            byte[] RegAddr;
            try
            {
                DevAddr = HexConverter.ParseHexString(ListDeviceAddrComboBox.Text);
                RegAddr = HexConverter.ParseHexString(ListRegAddr.Text);
            }
            catch
            {
                MessageBox.Show($"错误: 设备地址或寄存器地址非有效Hex", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            I2C_Register Reg = new I2C_Register(DevAddr[0], RegAddr[0], (int)ListBytesNumber.Value);
            I2C_Register_Group.Add(Reg);
        }

        private void RegDeleteButton_Click(object sender, EventArgs e)
        {
            if (RegisterGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int RowIndex = RegisterGridView.SelectedRows[0].Index;
            I2C_Register_Group.Delete(RowIndex);
        }

        private void ReadListButton_Click(object sender, EventArgs e)
        {
            if (RegisterGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int RowIndex = RegisterGridView.SelectedRows[0].Index;
            ListDeviceAddrComboBox.Text = $"0x{I2C_Register_Group.I2C_Registers[RowIndex].DeviceAddress:X2}";
            ListRegAddr.Text = $"0x{I2C_Register_Group.I2C_Registers[RowIndex].RegisterAddress:X2}";
            ListBytesNumber.Value = I2C_Register_Group.I2C_Registers[RowIndex].RegisterNumber;
        }

        private void RegEditButton_Click(object sender, EventArgs e)
        {
            if (ListDeviceAddrComboBox.Text == "" || ListRegAddr.Text == "")
            {
                MessageBox.Show($"错误: 未输入设备地址或寄存器地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (RegisterGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show($"错误: 未选择修改项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int RowIndex = RegisterGridView.SelectedRows[0].Index;
            byte[] DevAddr;
            byte[] RegAddr;
            try
            {
                DevAddr = HexConverter.ParseHexString(ListDeviceAddrComboBox.Text);
                RegAddr = HexConverter.ParseHexString(ListRegAddr.Text);
            }
            catch
            {
                MessageBox.Show($"错误: 设备地址或寄存器地址非有效Hex", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            I2C_Register_Group.I2C_Registers[RowIndex].DeviceAddress = DevAddr[0];
            I2C_Register_Group.I2C_Registers[RowIndex].RegisterAddress = RegAddr[0];
            I2C_Register_Group.I2C_Registers[RowIndex].RegisterNumber = (int)ListBytesNumber.Value;

            RegisterGridView.Invalidate();
        }

        private void AutoRegFlush_Close(object sender, EventArgs e)
        {
            if(SystemStatus.CurrentStatus == SystemStatus.Status.Disconnect && AutoRegFlush.Checked == true)
            {
                AutoRegFlush.Checked = false;
            }
        }

        private void AutoRegFlush_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRegFlush.Checked == true)
            {
                if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
                {
                    return;
                }

                Tool.StartRegAutoFlush((int)FlushRegFrequent.Value);
                Tool.RegisterRefreshed += OnRegistersRefreshed;
                FlushRegFrequent.Enabled = false;
                SystemStatus.ChangeStatus(SystemStatus.Status.AutoUpdata);
            }
            else
            {
                Tool.StopRegAutoFlush();
                Tool.RegisterRefreshed -= OnRegistersRefreshed;
                FlushRegFrequent.Enabled = true;
                if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
                {

                }
                else
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
                }
            }
        }

        private void DialogReadRegButton_Click(object sender, EventArgs e)
        {
            if (DialogDeviceAddrComboBox.Text == "" || DialogRegAddrTextBox.Text == "")
            {
                MessageBox.Show($"错误: 未输入设备地址或寄存器地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] DevAddr;
            byte[] RegAddr;
            try
            {
                DevAddr = HexConverter.ParseHexString(DialogDeviceAddrComboBox.Text);
                RegAddr = HexConverter.ParseHexString(DialogRegAddrTextBox.Text);
            }
            catch
            {
                MessageBox.Show($"错误: 设备地址或寄存器地址非有效Hex", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<DataItem> Items = new List<DataItem>();
            DataItem Item = new DataItem(1, DevAddr[0], RegAddr[0], (int)DialogReadBytesNumber.Value, null);
            Items.Add(Item);
            List<DataItem> RecvItems = Tool.SendMsgToDevice(Items);

            DateTimeOffset Time = new DateTimeOffset(DateTime.Now);

            I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
            I2CMsgTextBox.SelectionLength = 0;
            I2CMsgTextBox.SelectionColor = Color.DeepSkyBlue;
            I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
            I2CMsgTextBox.AppendText($"{Time.DateTime} Dev:0x{DevAddr[0]:X2} Reg:0x{RegAddr[0]:X2} Num:{RecvItems[0].Size} [Read]--> \n");

            if(RecvItems[0].Value.Count != 0)
            {
                I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
                I2CMsgTextBox.SelectionColor = Color.Black;
                I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
                I2CMsgTextBox.AppendText($"{string.Join(" ", RecvItems[0].Value.Select(b => $"0x{b:X2}"))}" + "\n");
            }
            else
            {
                I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
                I2CMsgTextBox.SelectionColor = Color.Red;
                I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
                I2CMsgTextBox.AppendText($"错误" + "\n");
            }
        }

        private void I2CWriteButton_Click(object sender, EventArgs e)
        {
            if (DialogDeviceAddrComboBox.Text == "" || DialogRegAddrTextBox.Text == "" || DialogWriteBytes.Text == "")
            {
                MessageBox.Show($"错误: 未输入设备地址或寄存器地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] DevAddr;
            byte[] RegAddr;
            byte[] RegValue;
            try
            {
                DevAddr = HexConverter.ParseHexString(DialogDeviceAddrComboBox.Text);
                RegAddr = HexConverter.ParseHexString(DialogRegAddrTextBox.Text);
                RegValue = HexConverter.ParseHexString(DialogWriteBytes.Text);
            }
            catch
            {
                MessageBox.Show($"错误: 设备地址或寄存器地址非有效Hex", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<DataItem> Items = new List<DataItem>();
            DataItem Item = new DataItem(2, DevAddr[0], RegAddr[0], RegValue.Length, RegValue.ToList());
            Items.Add(Item);
            List<DataItem> RecvItems = Tool.SendMsgToDevice(Items);

            DateTimeOffset Time = new DateTimeOffset(DateTime.Now);

            I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
            I2CMsgTextBox.SelectionLength = 0;
            I2CMsgTextBox.SelectionColor = Color.DeepSkyBlue;
            I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
            I2CMsgTextBox.AppendText($"{Time.DateTime} Dev:0x{DevAddr[0]:X2} Reg:0x{RegAddr[0]:X2} Num:{RecvItems[0].Size} [Write]--> \n");

            if ((byte)RecvItems[0].Value[0] == 0)
            {
                I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
                I2CMsgTextBox.SelectionColor = Color.Black;
                I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
                I2CMsgTextBox.AppendText($"{string.Join(" ", RegValue.Select(b => $"0x{b:X2}"))}" + "\n");
            }
            else
            {
                I2CMsgTextBox.SelectionStart = I2CMsgTextBox.TextLength;
                I2CMsgTextBox.SelectionColor = Color.Red;
                I2CMsgTextBox.SelectionFont = new Font(I2CMsgTextBox.Font, FontStyle.Bold);
                I2CMsgTextBox.AppendText($"错误，代码：{(byte)RecvItems[0].Value[0]:X2}" + "\n");
            }
        }

        private void ClearDialogMsgButton_Click(object sender, EventArgs e)
        {
            I2CMsgTextBox.Clear();
        }

        private void ByteSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if(box == RegHighByteEnable)
            {
                WaveDataHighBit.Enabled = box.Checked;
            }
            else
            {
                WaveDataLowBit.Enabled = box.Checked;
            }
        }

        private void AddWaveButton_Click(object sender, EventArgs e) 
        {
            if(SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return;
            }

            if(DevComboBox.Text == "" || 
                WaveName.Text == "" || 
                WaveDataHighBit.Text == "" || 
                WaveDataLowBit.Text == "" ||
                ColorBox.SelectedColor == null
                )
            {
                MessageBox.Show($"错误: 请将波形信息填写完整", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WaveUnit Wave;

            if (RegHighByteEnable.Checked != false && RegLowByteEnable.Checked != false)
            {
                if((I2C_Register)WaveDataHighBit.SelectedItem == (I2C_Register)WaveDataLowBit.SelectedItem)
                {
                    MessageBox.Show($"错误: 高位寄存器和低位寄存器不可指向同一位置", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Wave = new WaveUnit(WaveName.Text,
                                    ColorBox.SelectedColor,
                                    (I2C_Register)WaveDataHighBit.SelectedItem,
                                    (I2C_Register)WaveDataLowBit.SelectedItem,
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed += value;
                                    },
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed -= value;
                                    }
                                    );
            }
            else if (RegHighByteEnable.Checked == false && RegLowByteEnable.Checked != false)
            {
                Wave = new WaveUnit(WaveName.Text,
                                    ColorBox.SelectedColor,
                                    null,
                                    (I2C_Register)WaveDataLowBit.SelectedItem,
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed += value;
                                    },
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed -= value;
                                    }
                                    );
            }
            else if (RegHighByteEnable.Checked != false && RegLowByteEnable.Checked == false)
            {
                Wave = new WaveUnit(WaveName.Text,
                                    ColorBox.SelectedColor,
                                    (I2C_Register)WaveDataHighBit.SelectedItem,
                                    null,
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed += value;
                                    },
                                    (value) =>
                                    {
                                        Tool.RegisterRefreshed -= value;
                                    }
                                    );

            }
            else
            {
                MessageBox.Show($"错误: 不可同时禁用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Wave_Group.WaveUnits.Add(Wave);

            MessageBox.Show($"波形：{WaveName.Text} 已添加完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 

        private void DeleteWaveButton_Click(object sender, EventArgs e)
        {
            if (WaveList.SelectedItem == null)
            {
                MessageBox.Show($"错误: 未选择要删除的波形", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WaveUnit Wave =  Wave_Group.WaveUnits.FirstOrDefault(r => r.WaveName == WaveList.Text);
            string Name = Wave.ToString();
            WavePictureBox1.RemoveWave(Wave);
            Wave_Group.WaveUnits.Remove(Wave);
            MessageBox.Show($"提示: 已删除波形 {Name}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RefreshWaveformComboBox();

        }

        private void DevComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DevComboBox.SelectedIndex == -1)
            {
                return;
            }

            lock (I2C_Register_Group.I2C_Registers)
            {
                RefreshRegisterBits();
            }
        }

        private void RefreshRegisterBits()
        {
            WaveDataHighBit.Items.Clear();
            WaveDataLowBit.Items.Clear();

            if(DevComboBox.SelectedItem != null)
            {
                var FilteredList = I2C_Register_Group.I2C_Registers.Where(
                    r => r.DeviceAddress == ((I2C_Device)DevComboBox.SelectedItem).DeviceAddress
                    ).ToList();

                foreach (var Filtered in FilteredList)
                {
                    WaveDataHighBit.Items.Add(Filtered);
                    WaveDataLowBit.Items.Add(Filtered);
                }

                if (WaveDataHighBit.Items.Count > 0)
                    WaveDataHighBit.SelectedIndex = 0;
                if (WaveDataLowBit.Items.Count > 0)
                    WaveDataLowBit.SelectedIndex = 0;
            }
        }

        private void RefreshWaveformComboBox()
        {

        }

        private void OnRegistersRefreshed(object sender, EventArgs e)
        {
            if (RegisterGridView.InvokeRequired)
            {
                RegisterGridView.Invoke(new MethodInvoker(() => RegisterGridView.Invalidate()));
            }
            else
            {
                RegisterGridView.Refresh();
            }
        }

        private void IICToolSwitch_Close(object sender, EventArgs e)
        {
            if (SystemStatus.CurrentStatus == SystemStatus.Status.Disconnect && IICToolSwitch.Checked == true)
            {
                IICToolSwitch.Checked = false;
            }
        }

        private async void IICToolSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (IICToolSwitch.Checked)
            {
                if(SystemStatus.CurrentStatus == SystemStatus.Status.Disconnect)
                {
                    if(await Tool.Start_I2C_Tool() == false)
                    {
                        MessageBox.Show("错误：设备启动失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IICToolSwitch.Checked = false;
                    }
                }
            }
            else
            {
                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    await Tool.Stop_I2C_Tool();
                }
            }
            
        }

        private void RegisterGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var col = RegisterGridView.Columns[e.ColumnIndex];

            if (col == null)
            {
                return;
            }

            string colName = col.Name;

            if (colName == "设备地址" || colName == "REG地址" || colName == "内容")
            {
                if (e.Value != null)
                {
                    if (e.Value is byte b)
                    {
                        e.Value = $"0x{b:X2}";
                        e.FormattingApplied = true;
                    }
                    else if (e.Value is byte[] bytes)
                    {
                        e.Value = string.Join(" ", bytes.Select(r => $"0x{r:X2}"));
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void NumberPerPixel_ValueChanged(object sender, EventArgs e)
        {
            WavePictureBox1.VerticalScale = (double)NumberPerPixel.Value;
        }

        private void WaveTimeBase_ValueChanged(object sender, EventArgs e)
        {
            WavePictureBox1.CurrentTimeBase = (double)WaveTimeBase.Value;
        }

        private void ColorBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void DebugTrackBar_Scroll(object sender, EventArgs e)
        //{
        //    byte[] Bytes = new byte[1];
        //    Bytes[0] = (byte)DebugTrackBar.Value;
        //    I2C_Register_Group.I2C_Registers[0].RegisterValue = Bytes;
        //    Wave_Group.WaveUnits[0].Debug();
        //}
    }

    public static class DoubleBufferDataGridView
    {
        public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, flag, null);
        }
    }

    public class ColorItem
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public override string ToString() => Name;
    }

    public class ColorComboBox : System.Windows.Forms.ComboBox
    {
        public ColorComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DrawItem += ColorComboBox_DrawItem;
        }

        public void AddColor(string name, Color color)
        {
            Items.Add(new ColorItem { Name = name, Color = color });
        }

        private void ColorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = (ColorItem)Items[e.Index];
            e.DrawBackground();

            var rect = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, 20, e.Bounds.Height - 4);
            using (var brush = new SolidBrush(item.Color))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            e.Graphics.DrawString(item.Name, e.Font, Brushes.Black, e.Bounds.Left + 25, e.Bounds.Top);
        }

        public Color SelectedColor
        {
            get => (SelectedItem as ColorItem)?.Color ?? Color.Empty;
            set
            {
                foreach (ColorItem item in Items)
                {
                    if (item.Color.ToArgb() == value.ToArgb())
                    {
                        SelectedItem = item;
                        return;
                    }
                }
            }
        }
    }

    public class WavePictureBox : PictureBox
    {
        private const int WIDTH = 500;
        private const int HEIGHT = 420;
        private const int ZERO_Y = HEIGHT / 2;

        private double _currentTimeBase = 1000;
        private double _verticalScale = 10;

        private readonly Dictionary<WaveUnit, Queue<DataPoint>> _waveData =
            new Dictionary<WaveUnit, Queue<DataPoint>>();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly object _renderLock = new object();

        private bool _isDragging;
        private int _dragX;
        private int _dragY;

        private Bitmap _bitmap;
        private Graphics _graphics;

        private double _lastRenderTime;

        public WavePictureBox()
        {
            Size = new Size(WIDTH, HEIGHT);
            BackColor = Color.White;

            InitializeBitmap();

            Wave_Group.WaveUnits.ListChanged += WaveUnits_ListChanged;

            foreach (var wave in Wave_Group.WaveUnits)
            {
                AddWaveUnit(wave);
            }

            _stopwatch.Start();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.AllPaintingInWmPaint,
                         true);
        }

        private void InitializeBitmap()
        {
            lock (_renderLock)
            {
                _bitmap?.Dispose();
                _graphics?.Dispose();

                _bitmap = new Bitmap(WIDTH, HEIGHT);
                _graphics = Graphics.FromImage(_bitmap);
                _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                _graphics.Clear(Color.White);
                UpdateImageSafely(_bitmap);
            }
        }

        #region 公共属性
        public double CurrentTimeBase
        {
            get => _currentTimeBase;
            set
            {
                if (Math.Abs(value - _currentTimeBase) < 0.01) return;

                _currentTimeBase = value;
                ResetData();
            }
        }

        public double VerticalScale
        {
            get => _verticalScale;
            set
            {
                if (Math.Abs(value - _verticalScale) < 0.01) return;

                _verticalScale = value;
                Render();
            }
        }
        #endregion

        #region 波形管理
        public void RemoveWave(WaveUnit Wave)
        {
            RemoveWaveUnit(Wave);
        }

        private void WaveUnits_ListChanged(object sender, ListChangedEventArgs e)
        {
            lock (_renderLock)
            {
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    AddWaveUnit(Wave_Group.WaveUnits[e.NewIndex]);
                }
                else if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    //RemoveWaveUnit(Wave_Group.WaveUnits[e.OldIndex]); Bug!
                }
                else if (e.ListChangedType == ListChangedType.Reset)
                {
                    foreach (var wave in _waveData.Keys.ToList())
                    {
                        RemoveWaveUnit(wave);
                    }
                }
            }
        }

        private void AddWaveUnit(WaveUnit wave)
        {
            lock (_renderLock)
            {
                if (_waveData.ContainsKey(wave)) return;

                _waveData[wave] = new Queue<DataPoint>();
                wave.ValueChanged += Wave_ValueChanged;
            }
        }

        private void RemoveWaveUnit(WaveUnit wave)
        {
            lock (_renderLock)
            {
                if (!_waveData.ContainsKey(wave)) return;

                wave.ValueChanged -= Wave_ValueChanged;
                _waveData.Remove(wave);
                Render();
            }
        }

        private void Wave_ValueChanged(object sender, EventArgs e)
        {
            var wave = (WaveUnit)sender;
            Queue<DataPoint> queue;

            lock (_renderLock)
            {
                if (!_waveData.TryGetValue(wave, out queue)) return;
            }

            double timeMs = _stopwatch.Elapsed.TotalMilliseconds;
            short? currentValue = wave.CurrentValue;

            lock (queue)
            {
                queue.Enqueue(new DataPoint(timeMs, currentValue));

                while (queue.Count > 50)
                {
                    queue.Dequeue();
                }
            }

            //限制渲染频率
            if ((timeMs - _lastRenderTime) > 33)
            {
                Render();
                _lastRenderTime = timeMs;
            }
        }
        #endregion

        #region 数据处理
        private void ResetData()
        {
            lock (_renderLock)
            {
                _stopwatch.Restart();
                foreach (var queue in _waveData.Values)
                {
                    lock (queue)
                    {
                        queue.Clear();
                    }
                }
                Render();
            }
        }
        #endregion

        #region 渲染绘制
        private void Render()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(Render));
                return;
            }

            lock (_renderLock)
            {
                try
                {
                    if (_graphics == null || _bitmap == null)
                    {
                        InitializeBitmap();
                    }

                    _graphics.Clear(Color.White);

                    DrawAxes();

                    foreach (var kvp in _waveData)
                    {
                        DrawWaveform(kvp.Key, kvp.Value);
                    }

                    if (_isDragging)
                    {
                        DrawDragLine();
                    }

                    UpdateImageSafely(_bitmap);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"渲染错误: {ex.Message}");
                }
            }
        }

        private void UpdateImageSafely(Image image)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Image>(UpdateImageSafely), image);
                return;
            }

            Bitmap temp = new Bitmap(image);
            Image old = this.Image;
            this.Image = temp;
            old?.Dispose();
        }

        private int GetTotalDataPoints()
        {
            int count = 0;
            foreach (var kvp in _waveData)
            {
                lock (kvp.Value)
                {
                    count += kvp.Value.Count;
                }
            }
            return count;
        }

        private void DrawAxes()
        {
            using (var axisPen = new Pen(Color.Black, 2))
            {

                _graphics.DrawLine(axisPen, 0, 0, 0, HEIGHT);

                _graphics.DrawLine(axisPen, 0, HEIGHT - 1, WIDTH, HEIGHT - 1);

                using (var zeroPen = new Pen(Color.LightGray))
                {
                    _graphics.DrawLine(zeroPen, 0, ZERO_Y, WIDTH, ZERO_Y);
                }

                using (var gridPen = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
                {
                    double maxValue = (HEIGHT / 2) * _verticalScale;
                    double minValue = -maxValue;

                    double valueRange = maxValue - minValue;
                    double step = CalculateOptimalStep(valueRange, 8);

                    for (double value = minValue; value <= maxValue; value += step)
                    {
                        float y = ZERO_Y - (float)(value / _verticalScale);

                        if (y < 0 || y >= HEIGHT) continue;

                        _graphics.DrawLine(gridPen, 0, y, WIDTH, y);

                        string valueText = FormatValue(value);
                        SizeF textSize = _graphics.MeasureString(valueText, Font);

                        _graphics.FillRectangle(Brushes.White, 5, y - textSize.Height / 2, textSize.Width + 5, textSize.Height);

                        _graphics.DrawString(valueText, Font, Brushes.Black, 5, y - textSize.Height / 2);
                    }

                    for (int x = 50; x < WIDTH; x += 50)
                    {
                        _graphics.DrawLine(gridPen, x, 0, x, HEIGHT);

                        if (x % 100 == 0)
                        {
                            double timeMs = (x * _currentTimeBase) / WIDTH;
                            string timeText = $"{timeMs:0}ms";
                            SizeF textSize = _graphics.MeasureString(timeText, Font);

                            float textX = Math.Max(0, Math.Min(WIDTH - textSize.Width, x - textSize.Width / 2));

                            _graphics.FillRectangle(Brushes.White, textX, HEIGHT - textSize.Height - 5,
                                                  textSize.Width, textSize.Height);

                            _graphics.DrawString(timeText, Font, Brushes.Black, textX, HEIGHT - textSize.Height - 15);
                        }
                    }
                }
            }

            _graphics.DrawString("时间 (ms)", Font, Brushes.Black, WIDTH / 2 - 30, HEIGHT - 25);
            _graphics.DrawString("值", Font, Brushes.Black, 10, 5);
        }

        private double CalculateOptimalStep(double range, int maxTicks)
        {
            double unroundedStep = range / maxTicks;
            double x = Math.Ceiling(Math.Log10(unroundedStep) - 1);
            double power = Math.Pow(10, x);
            return Math.Ceiling(unroundedStep / power) * power;
        }

        private string FormatValue(double value)
        {
            if (Math.Abs(value) >= 1000)
                return $"{value / 1000:0.#}k";

            if (Math.Abs(value) < 0.1)
                return $"{value:0.000}";

            if (Math.Abs(value) < 1)
                return $"{value:0.00}";

            return $"{value:0.#}";
        }

        private void DrawWaveform(WaveUnit wave, Queue<DataPoint> data)
        {
            if (data.Count == 0) return;

            DataPoint[] points;
            lock (data)
            {
                points = data.ToArray();
            }

            double minTime = points.Min(p => p.TimeMs);
            double maxTime = points.Max(p => p.TimeMs);
            double timeRange = maxTime - minTime;

            if (timeRange <= 0) timeRange = _currentTimeBase;

            using (var wavePen = new Pen(wave.WaveColor, 2))
            {
                PointF? lastPoint = null;

                for (int i = 0; i < points.Length; i++)
                {
                    var point = points[i];

                    if (!point.Value.HasValue)
                    {
                        lastPoint = null;
                        continue;
                    }

                    float x = CalculateX(point.TimeMs, minTime, timeRange);

                    float y = CalculateY(point.Value.Value);

                    x = Math.Max(0, Math.Min(WIDTH - 1, x));
                    y = Math.Max(0, Math.Min(HEIGHT - 1, y));

                    var currentPoint = new PointF(x, y);

                    _graphics.FillEllipse(Brushes.Red, x - 2, y - 2, 4, 4);

                    if (lastPoint.HasValue)
                    {
                        _graphics.DrawLine(wavePen, lastPoint.Value, currentPoint);
                    }

                    lastPoint = currentPoint;
                }
            }
        }

        private float CalculateX(double pointTime, double minTime, double timeRange)
        {
            double timePosition = (pointTime - minTime) / timeRange;

            return (float)(timePosition * WIDTH);
        }

        private float CalculateY(short value)
        {
            float pixelValue = (float)(value / _verticalScale);
            float y = ZERO_Y - pixelValue;

            if (y < 0) y = 0;
            else if (y >= HEIGHT) y = HEIGHT - 1;

            return y;
        }

        private void DrawDragLine()
        {
            using (var dragPen = new Pen(Color.Blue) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                _graphics.DrawLine(dragPen, _dragX, 0, _dragX, HEIGHT);

                DrawValueInfo(_dragX);
            }
        }

        private void DrawValueInfo(int xPos)
        {
            float infoY = 10;

            using (var backBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
            {
                int infoHeight = 15 * _waveData.Count;
                _graphics.FillRectangle(backBrush, xPos + 5, infoY, 150, infoHeight);
            }

            foreach (var kvp in _waveData)
            {
                var wave = kvp.Key;
                var data = kvp.Value;

                DataPoint[] points;
                lock (data)
                {
                    points = data.ToArray();
                }

                if (points.Length == 0) continue;

                double minTime = points.Min(p => p.TimeMs);
                double maxTime = points.Max(p => p.TimeMs);
                double timeRange = maxTime - minTime;
                if (timeRange <= 0) timeRange = _currentTimeBase;

                double pointTime = minTime + (xPos * timeRange) / WIDTH;

                DataPoint? nearest = null;
                double minDiff = double.MaxValue;

                foreach (var point in points)
                {
                    if (!point.Value.HasValue) continue;

                    double diff = Math.Abs(point.TimeMs - pointTime);
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        nearest = point;
                    }
                }

                if (nearest.HasValue && nearest.Value.Value.HasValue)
                {
                    string info = $"{wave.WaveName}: {nearest.Value.Value.Value}";
                    using (var brush = new SolidBrush(wave.WaveColor))
                    {
                        _graphics.DrawString(info, Font, brush, xPos + 5, infoY);
                    }
                    infoY += 20;
                }
            }
        }
        #endregion

        #region 鼠标事件处理
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragX = e.X;
                _dragY = e.Y;
                Render();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isDragging)
            {
                _dragX = e.X;
                _dragY = e.Y;
                Render();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isDragging)
            {
                _isDragging = false;
                Render();
            }
            base.OnMouseUp(e);
        }
        #endregion

        #region 辅助结构
        private readonly struct DataPoint
        {
            public double TimeMs { get; }
            public short? Value { get; }

            public DataPoint(double timeMs, short? value)
            {
                TimeMs = timeMs;
                Value = value;
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Wave_Group.WaveUnits.ListChanged -= WaveUnits_ListChanged;

                lock (_renderLock)
                {
                    foreach (var wave in _waveData.Keys.ToList())
                    {
                        wave.ValueChanged -= Wave_ValueChanged;
                    }

                    _graphics?.Dispose();
                    _bitmap?.Dispose();
                    _graphics = null;
                    _bitmap = null;
                }
            }
            base.Dispose(disposing);
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

            if (input.IndexOf("0x", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return ParseHexWithPrefix(input);
            }

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
                if (index < input.Length - 2 &&
                    input[index] == '0' &&
                    (input[index + 1] == 'x' || input[index + 1] == 'X') &&
                    IsHexChar(input[index + 2]))
                {
                    index += 2;

                    int end = index;
                    while (end < input.Length && IsHexChar(input[end])) end++;

                    string hexBlock = input.Substring(index, end - index);
                    index = end;

                    ProcessHexBlock(hexBlock, result);
                }
                else
                {
                    index++;
                }
            }

            return result.ToArray();
        }

        private static void ProcessHexBlock(string hexBlock, List<byte> result)
        {
            int charCount = hexBlock.Length;

            if (charCount % 2 != 0)
            {
                result.Add((byte)CharToNibble(hexBlock[0]));
                hexBlock = hexBlock.Substring(1);
                charCount = hexBlock.Length;
            }

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

    public class I2C_Register
    {
        public byte DeviceAddress { get; set; }

        public byte RegisterAddress { get; set; }

        public int RegisterNumber { get; set; }

        [JsonIgnore]
        private byte[] _registerValue;

        [JsonIgnore]
        public byte[] RegisterValue
        {
            get => _registerValue;
            set
            {
                _registerValue = value;
                RegisterValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RegisterValueChanged;

        public I2C_Register()
        {
            RegisterValue = Array.Empty<byte>();
        }

        public I2C_Register(byte DevAddr, byte RegAddr, int Number)
        {
            DeviceAddress = DevAddr;
            RegisterAddress = RegAddr;
            RegisterNumber = Number;
            RegisterValue = Array.Empty<byte>();
        }

        public override string ToString()
        {
            return $"0x{RegisterAddress:X2}";
        }
    }

    public class I2C_Device
    {
        public byte DeviceAddress { get; set; }

        public I2C_Device(byte DevAddr)
        {
            DeviceAddress = DevAddr;
        }

        public override string ToString()
        {
            return $"0x{DeviceAddress:X2}";
        }
    }

    public class I2C_Register_Group
    {
        public static BindingList<I2C_Register> I2C_Registers = new BindingList<I2C_Register>();

        public static void Add(I2C_Register Reg)
        {
            I2C_Registers.Add(Reg);
            SaveRegistersToJson();
        }

        public static void Delete(int index)
        {
            I2C_Registers.RemoveAt(index);
            SaveRegistersToJson();
        }

        [JsonIgnore]
        private static string FilePath
        {
            get
            {
                var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var dllDir = Path.GetDirectoryName(dllPath);
                return Path.Combine(dllDir, "i2c_registers.json");
            }
        }

        private static void SaveRegistersToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(I2C_Register_Group.I2C_Registers, options);

            File.WriteAllText(FilePath, jsonString);
        }

        public static void LoadRegistersFromJson()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("文件不存在： " + FilePath);
                return;
            }

            string jsonString = File.ReadAllText(FilePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var registers = JsonSerializer.Deserialize<BindingList<I2C_Register>>(jsonString, options);

                if (registers != null)
                {
                    I2C_Register_Group.I2C_Registers.Clear();
                    foreach (var reg in registers)
                    {
                        I2C_Register_Group.I2C_Registers.Add(reg);
                    }
                    Console.WriteLine("加载成功");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("反序列化失败：" + ex.Message);
            }
        }
    }

    public class I2C_Device_Group
    {
        public static BindingList<I2C_Device> I2C_Devices = new BindingList<I2C_Device>();
    }

    public class DataItem
    {
        public int CMD { get; set; }
        public byte Addr { get; set; }
        public byte Reg { get; set; }
        public int Size { get; set; }
        public List<byte> Value { get; set; }

        public DataItem(int cmd, byte addr, byte reg, int size, List<byte> value)
        {
            CMD = cmd;
            Addr = addr;
            Reg = reg;
            Size = size;
            if (value == null)
            {
                Value = new List<byte>();
            }
            else
            {
                Value = value;
            }
        }
    }

    public class I2C_Tool_Class
    {
        private NetworkStream Stream { get; set; }
        public event EventHandler RegisterRefreshed;
        private static readonly SemaphoreSlim DeviceSemaphore = new SemaphoreSlim(1, 1);
        private Thread RegAutoFlushThreadHandle;
        private bool ThreadStopFlag;

        public I2C_Tool_Class(NetworkStream stream)
        {
            Stream = stream;
        }

        public async Task<bool> Start_I2C_Tool()
        {
            byte[] CMD_Start = Encoding.ASCII.GetBytes("{I2CTOOL}");
            byte[] CMD_Recv = new byte[16];

            if(SystemStatus.CurrentStatus != SystemStatus.Status.Disconnect)
            {
                return false;
            }

            SystemStatus.ChangeStatus(SystemStatus.Status.Connecting);
            await DeviceSemaphore.WaitAsync();
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(CMD_Start, 0, CMD_Start.Length, cts.Token);
                    int Length = await Stream.ReadAsync(CMD_Recv, 0, CMD_Recv.Length, cts.Token);
                    if (Length != 1 && CMD_Recv[0] != 'y')
                    {
                        return false;
                    }
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
                    return true;
                }
            }
            catch (OperationCanceledException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DeviceSemaphore.Release();
            }

            return false;
        }
    
        public async Task Stop_I2C_Tool()
        {
            byte[] CMD_Start = Encoding.ASCII.GetBytes("{I2CTOOL}");
            byte[] CMD_Recv = new byte[16];

            StopRegAutoFlush();

            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return;
            }

            SystemStatus.ChangeStatus(SystemStatus.Status.Connecting);
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(CMD_Start, 0, CMD_Start.Length, cts.Token);
                    int Length = await Stream.ReadAsync(CMD_Recv, 0, CMD_Recv.Length, cts.Token);
                    if (Length != 1 && CMD_Recv[0] != 'y')
                    {
                        return;
                    }
                    return;
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("错误：关闭操作，响应超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：关闭操作，网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：关闭操作，网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：关闭操作，未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            return;
        }
    
        public async Task ScanDevice()
        {
            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return;
            }

            List<DataItem> Items = new List<DataItem>();
            DataItem Item = new DataItem(0,0,0,0,null);
            Items.Add(Item);
            List<DataItem> RecvItems = await SendMsgToDeviceAsync(Items);
            for (int i = 0; i < RecvItems.Count; i++)
            {
                I2C_Device dev = new I2C_Device(RecvItems[0].Value[i]);
                I2C_Device_Group.I2C_Devices.Add(dev);
            }
        }
    
        public List<DataItem> SendMsgToDevice(List<DataItem> item)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string CMD_Json = JsonSerializer.Serialize(item, options);
            byte[] CMD_Json_Bytes = Encoding.ASCII.GetBytes(CMD_Json);
            byte[] Json_Recv = new byte[2048 + CMD_Json_Bytes.Length];

            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return null;
            }

            DeviceSemaphore.Wait();
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    Stream.WriteAsync(CMD_Json_Bytes, 0, CMD_Json_Bytes.Length, cts.Token).Wait();
                    int Length = Stream.ReadAsync(Json_Recv, 0, Json_Recv.Length, cts.Token).Result;

                    if (Length < 0)
                    {
                        return null;
                    }

                    string jsonString = Encoding.ASCII.GetString(Json_Recv, 0, Length);

                    var result = JsonSerializer.Deserialize<List<DataItem>>(jsonString);
                    return result;
                }
            }
            catch (OperationCanceledException)
            {
                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException)
            {
                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (TimeoutException)
            {
                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                    MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                DeviceSemaphore.Release();
            }
            return null;
        }

        public async Task<List<DataItem>> SendMsgToDeviceAsync(List<DataItem> item)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string CMD_Json = JsonSerializer.Serialize(item, options);
            byte[] CMD_Json_Bytes = Encoding.ASCII.GetBytes(CMD_Json);
            byte[] Json_Recv = new byte[2048];

            if (SystemStatus.CurrentStatus < SystemStatus.Status.Connected)
            {
                return null;
            }

            await DeviceSemaphore.WaitAsync();
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await Stream.WriteAsync(CMD_Json_Bytes, 0, CMD_Json_Bytes.Length, cts.Token);
                    int Length = await Stream.ReadAsync(Json_Recv, 0, Json_Recv.Length, cts.Token);

                    if (Length < 0)
                    {
                        return null;
                    }

                    string jsonString = Encoding.ASCII.GetString(Json_Recv, 0, Length);

                    var result = JsonSerializer.Deserialize<List<DataItem>>(jsonString);
                    return result;
                }
            }
            catch (OperationCanceledException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TimeoutException)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DeviceSemaphore.Release();
            }
            return null;
        }

        public void StartRegAutoFlush(int FlushDelay)
        {
            ThreadStopFlag = false;
            RegAutoFlushThreadHandle = new Thread(() =>
            {
                RegAutoFlushThread(FlushDelay);
            });

            RegAutoFlushThreadHandle.Start();
        }

        public void StopRegAutoFlush()
        {
            if(!ThreadStopFlag)
            {
                ThreadStopFlag = true;
                if(RegAutoFlushThreadHandle != null && RegAutoFlushThreadHandle.ThreadState == System.Threading.ThreadState.Running)
                {
                    RegAutoFlushThreadHandle.Join();
                }
                RegAutoFlushThreadHandle = null;
            }
        }

        public void RegAutoFlushThread(int FlushDelay)
        {
            while (!ThreadStopFlag)
            {
                lock (I2C_Register_Group.I2C_Registers)
                {
                    List<DataItem> Items = new List<DataItem>();
                    foreach (I2C_Register Reg in I2C_Register_Group.I2C_Registers)
                    {
                        if(ThreadStopFlag)
                        {
                            return;
                        }
                        DataItem Item = new DataItem(1, Reg.DeviceAddress, Reg.RegisterAddress, Reg.RegisterNumber, null);
                        Items.Add(Item);
                    }

                    List<DataItem> RecvItems = SendMsgToDevice(Items);

                    if(RecvItems == null)
                    {
                        continue;
                    }

                    for (int i = 0; i < RecvItems.Count; i++)
                    {
                        if (ThreadStopFlag)
                        {
                            return;
                        }

                        DataItem item = RecvItems[i];
                        var reg = I2C_Register_Group.I2C_Registers.FirstOrDefault(
                            r => r.DeviceAddress == (byte)item.Addr && r.RegisterAddress == (byte)item.Reg);

                        if (reg != null)
                        {
                            byte[] bytes = item.Value.ToArray();
                            reg.RegisterValue = bytes;
                        }
                    }

                    RegisterRefreshed?.Invoke(this, EventArgs.Empty);
                }
                Thread.Sleep(FlushDelay);
            }
        }
    }

    public class WaveUnit
    {
        public string WaveName { get; set; }

        public Color WaveColor { get; set; }

        private I2C_Register High_8_bit {  get; set; }

        private I2C_Register Low_8_bit { get; set; }

        public short? CurrentValue;

        public event EventHandler ValueChanged;

        private readonly Action<EventHandler> RegisterRefreshEventRemove;

        public WaveUnit(string wave_name, 
            Color color,
            I2C_Register high_8_bit, 
            I2C_Register low_8_bit, 
            Action<EventHandler> RegisterRefreshEventAdd,
            Action<EventHandler> Remove
            )
        {
            WaveName = wave_name;
            WaveColor = color;
            High_8_bit = high_8_bit;
            Low_8_bit = low_8_bit;
            RegisterRefreshEventAdd(RefreshCompleted);
            RegisterRefreshEventRemove = Remove;
        }

        //public void Debug()
        //{
        //    RefreshCompleted(null, null);
        //}

        private void RefreshCompleted(object sender, EventArgs e)
        {
            short? result;
            if ((High_8_bit == null || High_8_bit.RegisterValue.Count() == 0) &&
                (Low_8_bit != null || Low_8_bit.RegisterValue.Count() != 0)
                )
            {
                result = (short)(0 | Low_8_bit.RegisterValue[0]);
            }
            else if ((High_8_bit != null || High_8_bit.RegisterValue.Count() != 0) &&
                (Low_8_bit == null || Low_8_bit.RegisterValue.Count() == 0)
                )
            {
                result = (short)((High_8_bit.RegisterValue[0] << 8) | 0);
            }
            else if ((High_8_bit != null || High_8_bit.RegisterValue.Count() != 0) &&
                (Low_8_bit != null || Low_8_bit.RegisterValue.Count() != 0))
            {
                result = (short)((High_8_bit.RegisterValue[0] << 8) | Low_8_bit.RegisterValue[0]);
            }
            else 
            {
                result = null;
            }


            CurrentValue = result;
            ValueChanged?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            RegisterRefreshEventRemove(RefreshCompleted);
        }
    }

    public class Wave_Group
    {
        public static readonly BindingList<WaveUnit> WaveUnits = new BindingList<WaveUnit>();
    }
}
