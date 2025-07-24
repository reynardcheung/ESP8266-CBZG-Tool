using ModNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SSD1306
{
    public partial class Form1 : Form
    {
        NetworkStream stream { get; set; }
        IMod module { get; set; }
        SendText SendTextObj;
        ConvertBitmap TextBitmap = new ConvertBitmap();
        Bitmap FileImageBitmap;
        private ArrayList GIFBitmap;

        public Form1(NetworkStream _stream, IMod mod)
        {
            InitializeComponent();
            stream = _stream;
            module = mod;
            GIFBitmap = new ArrayList();

            pixelGrid.PixelSize = 5;
            pixelGrid.Rows = 64;
            pixelGrid.Columns = 128;
            pixelGrid.Size = new Size(640, 320);
            pixelGrid.BorderStyle = BorderStyle.Fixed3D;

            this.Controls.Add(pixelGrid);

            SendTextObj = new SendText(this.UserTextBox);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pixelGrid.Clear();
            foreach (Bitmap bmp in GIFBitmap)
            {
                bmp.Dispose();
            }
            GIFBitmap.Clear();
            DeviceSwitchCheckBox.Checked = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            pixelGrid?.Dispose();
            BitPictureBox.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFonts();
            FontSizeComboBox.SelectedIndex = 3;
            SystemStatus.StatusChanged += ControlEnableManager;
            pixelGrid.DrawingCompleted += pixelGrid_DrawingCompleted;
            SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
        }

        private void ControlEnableManager(object sender,EventArgs e)
        {
            switch (SystemStatus.CurrentStatus)
            {
                case SystemStatus.Status.Disconnect:
                {
                    if(DeviceSwitchCheckBox.Checked)
                    {
                        DeviceSwitchCheckBox.Checked = false;
                    }
                    DeviceSwitchCheckBox.Enabled = true;
                    SerialSyncButton.Enabled = false;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = false;
                    ReadGIFButton.Enabled = false;
                    PixelClear.Enabled = false;
                    SendTextToScreenButton.Enabled = false;
                    UserTextBox.Enabled = false;
                    XNumericUpDown.Enabled = false;
                    YNumericUpDown.Enabled = false;
                    FontSizeComboBox.Enabled = false;
                    FontNameComboBox.Enabled = false;
                    thresholdTrackBar.Enabled = false;
                    BinarizationtrackBar.Enabled = false;
                    pixelGrid.Enabled = false;
                    break;
                }
                case SystemStatus.Status.Connecting:
                {
                    DeviceSwitchCheckBox.Enabled = false;
                    SerialSyncButton.Enabled = false;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = false;
                    ReadGIFButton.Enabled = false;
                    PixelClear.Enabled = false;
                    SendTextToScreenButton.Enabled = false;
                    UserTextBox.Enabled = false;
                    XNumericUpDown.Enabled = false;
                    YNumericUpDown.Enabled = false;
                    FontSizeComboBox.Enabled = false;
                    FontNameComboBox.Enabled = false;
                    thresholdTrackBar.Enabled = false;
                    BinarizationtrackBar.Enabled = false;
                    pixelGrid.Enabled = false;
                    break;
                }
                case SystemStatus.Status.Connected:
                {
                    DeviceSwitchCheckBox.Enabled = true;
                    SerialSyncButton.Enabled = true;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = true;
                    ReadGIFButton.Enabled = true;
                    PixelClear.Enabled = true;
                    SendTextToScreenButton.Enabled = true;
                    UserTextBox.Enabled = true;
                    XNumericUpDown.Enabled = true;
                    YNumericUpDown.Enabled = true;
                    FontSizeComboBox.Enabled = true;
                    FontNameComboBox.Enabled = true;
                    thresholdTrackBar.Enabled = true;
                    BinarizationtrackBar.Enabled = true;
                    pixelGrid.Enabled = true;
                    break;
                }
                case SystemStatus.Status.SerialSyncOff:
                {
                    DeviceSwitchCheckBox.Enabled = true;
                    SerialSyncButton.Enabled = true;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = true;
                    ReadGIFButton.Enabled = true;
                    PixelClear.Enabled = true;
                    SendTextToScreenButton.Enabled = true;
                    UserTextBox.Enabled = true;
                    XNumericUpDown.Enabled = true;
                    YNumericUpDown.Enabled = true;
                    FontSizeComboBox.Enabled = true;
                    FontNameComboBox.Enabled = true;
                    thresholdTrackBar.Enabled = true;
                    BinarizationtrackBar.Enabled = true;
                    pixelGrid.Enabled = true;
                    break;
                }
                case SystemStatus.Status.SerialSyncConnecting:
                {
                    DeviceSwitchCheckBox.Enabled = false;
                    SerialSyncButton.Enabled = false;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = false;
                    ReadGIFButton.Enabled = false;
                    PixelClear.Enabled = false;
                    SendTextToScreenButton.Enabled = false;
                    UserTextBox.Enabled = false;
                    XNumericUpDown.Enabled = false;
                    YNumericUpDown.Enabled = false;
                    FontSizeComboBox.Enabled = false;
                    FontNameComboBox.Enabled = false;
                    thresholdTrackBar.Enabled = false;
                    BinarizationtrackBar.Enabled = false;
                    pixelGrid.Enabled = false;
                    break;
                }
                case SystemStatus.Status.SerialSyncConnected:
                {
                    DeviceSwitchCheckBox.Enabled = true;
                    SerialSyncButton.Enabled = false;
                    CloseSerialSyncButton.Enabled = true;
                    ReadPictureButton.Enabled = false;
                    ReadGIFButton.Enabled = false;
                    PixelClear.Enabled = false;
                    SendTextToScreenButton.Enabled = false;
                    UserTextBox.Enabled = false;
                    XNumericUpDown.Enabled = false;
                    YNumericUpDown.Enabled = false;
                    FontSizeComboBox.Enabled = false;
                    FontNameComboBox.Enabled = false;
                    thresholdTrackBar.Enabled = false;
                    BinarizationtrackBar.Enabled = false;
                    pixelGrid.Enabled = false;
                    break;
                }
                case SystemStatus.Status.ShowGIFMode:
                {
                    DeviceSwitchCheckBox.Enabled = true;
                    SerialSyncButton.Enabled = false;
                    CloseSerialSyncButton.Enabled = false;
                    ReadPictureButton.Enabled = false;
                    ReadGIFButton.Enabled = false;
                    PixelClear.Enabled = true;
                    SendTextToScreenButton.Enabled = false;
                    UserTextBox.Enabled = false;
                    XNumericUpDown.Enabled = false;
                    YNumericUpDown.Enabled = false;
                    FontSizeComboBox.Enabled = false;
                    FontNameComboBox.Enabled = false;
                    thresholdTrackBar.Enabled = false;
                    BinarizationtrackBar.Enabled = false;
                    pixelGrid.Enabled = false;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        private void LoadFonts()
        {
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            int defaultIndex = -1;

            for (int i = 0; i < installedFonts.Families.Length; i++)
            {
                string fontName = installedFonts.Families[i].Name;
                FontNameComboBox.Items.Add(fontName);
                if (fontName.Equals("宋体", StringComparison.OrdinalIgnoreCase))
                {
                    defaultIndex = i;
                }
            }

            if (defaultIndex != -1)
            {
                FontNameComboBox.SelectedIndex = defaultIndex;
            }
            else
            {
                if (FontNameComboBox.Items.Count > 0)
                {
                    FontNameComboBox.SelectedIndex = 0;
                }
            }
        }

        private void PixelClear_Click(object sender, EventArgs e)
        {
            pixelGrid.Clear();
            if(SystemStatus.CurrentStatus == SystemStatus.Status.ShowGIFMode)
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
            }
            GIFBitmap.Clear();

        }

        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FontNameComboBox.SelectedIndex >= 0 && FontSizeComboBox.SelectedIndex >= 0)
            {
                SendTextObj.TextFontChanged((string)FontNameComboBox.SelectedItem, int.Parse((string)FontSizeComboBox.SelectedItem));
            }
        }

        private void FontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FontNameComboBox.SelectedIndex >= 0 && FontSizeComboBox.SelectedIndex >= 0)
            {
                SendTextObj.TextFontChanged((string)FontNameComboBox.SelectedItem, int.Parse((string)FontSizeComboBox.SelectedItem));
            }
        }

        private void SendTextToScreenButton_Click(object sender, EventArgs e)
        {
            if (FontNameComboBox.SelectedIndex >= 0 && FontSizeComboBox.SelectedIndex >= 0)
            {
                try
                {
                    if(UserTextBox.Text != "")
                    {
                        TextBinPictureUpdata();
                    }
                    else
                    {
                        BitPictureBox.SetImage(null);
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show($"加载图像失败: {ex.Message}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("发生错误：请选择字体或字号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NumericUpDown_Click(object sender, EventArgs e)
        {
            NumericUpDown obj = (NumericUpDown)sender;
            if (obj.Text == "")
            {
                obj.Value = 0;
            }
            BitPictureBox.SetPosition((int)XNumericUpDown.Value, (int)YNumericUpDown.Value);
        }

        private void thresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            thresholdLabel.Text = $"缩放 {thresholdTrackBar.Value} %";
            if(BitPictureBox.BitmapSource == TextBitmap.PreviewBitmap)
            {
                TextBinPictureUpdata();
            }
            else
            {
                BitPictureBox.ZoomFactor = thresholdTrackBar.Value / 100f;
            }
        }

        private void BinarizationtrackBar_Scroll(object sender, EventArgs e)
        {
            BinarizationLabel.Text = $"二值化 {BinarizationtrackBar.Value}";
            TextBitmap.Threshold = thresholdTrackBar.Value;
            BitPictureBox.Threshold = BinarizationtrackBar.Value;
        }

        private void BitPictureBox_LocationChanged(object sender, EventArgs e)
        {
            BinarizedPictureBox Obj = (BinarizedPictureBox)sender;
            try
            {
                XNumericUpDown.Value = Obj.Location.X;
                YNumericUpDown.Value = Obj.Location.Y;
            }
            catch
            {
                Obj.SetPosition(0, 0);
                Obj.StopMoving();
                MessageBox.Show("发生错误：移太远看不到了，或是图片太大", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BitPictureBox_DoubleClick(object sender, EventArgs e)
        {
            Bitmap img = ResizeImage(BitPictureBox.BitmapSource,
                        BitPictureBox.Size.Width / 5,
                        BitPictureBox.Size.Height / 5);
            await pixelGrid.ShowBitmap(img, 
                BitPictureBox.Location.X / 5, 
                BitPictureBox.Location.Y / 5);
            BitPictureBox.SetImage(null);
        }

        private async void ReadPictureButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片 (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;
                MessageBox.Show("警告：处理图片需要一定时间，请稍等", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await Task.Run(() => {
                    using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                    {
                        Image img = Image.FromStream(stream);
                        Invoke((MethodInvoker)(() => {
                            BitPictureBox.Image = img;
                            FileImageBitmap = new Bitmap(img);
                            BinPictureUpdata(FileImageBitmap);
                        }));
                    }
                });
            }
        }

        public Bitmap ResizeImage(Bitmap originalImage, int newWidth, int newHeight)
        {
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            return resizedImage;
        }

        private void BinPictureUpdata(Bitmap img)
        {
            BitPictureBox.SetImage(null);
            BitPictureBox.ZoomFactor = 5 * thresholdTrackBar.Value / 100f;
            BitPictureBox.SetImage(img);
        }

        private void TextBinPictureUpdata()
        {
            TextBitmap.Threshold = BinarizationtrackBar.Value;
            TextBitmap.Scale = thresholdTrackBar.Value / 100f;
            TextBitmap.ConvertTextToBitmap(UserTextBox.Text, UserTextBox.Font);
            BitPictureBox.SetImage(null);
            BitPictureBox.ZoomFactor = 5 * thresholdTrackBar.Value / 100f;
            BitPictureBox.SetImage(TextBitmap.PreviewBitmap);
        }

        private async void DeviceSwitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox DeviceSwitch = (CheckBox)sender;
            byte[] Cmd = Encoding.ASCII.GetBytes("{SSD1306}");
            byte[] SyncScreenCmd = Encoding.ASCII.GetBytes("{_ScreenSyncMode}");
            byte[] Recv = new byte[16];
            if(DeviceSwitch.Checked)
            {
                try
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connecting);
                    using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                    {
                        await stream.WriteAsync(Cmd, 0, Cmd.Length, cts.Token);
                        int Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                        if (Length != 1 || Recv[0] != 'y')
                        {
                            MessageBox.Show("错误：设备连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                            return;
                        }
                        await stream.WriteAsync(SyncScreenCmd, 0, SyncScreenCmd.Length, cts.Token);
                        Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                        if (Length != 1 || Recv[0] != 'y')
                        {
                            MessageBox.Show("错误：设备连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                            return;
                        }
                        SystemStatus.ChangeStatus(SystemStatus.Status.Connected);
                    }
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (IOException)
                {
                    MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (Exception)
                {
                    MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
            }
            else 
            {
                try
                {
                    SystemStatus.ChangeStatus(SystemStatus.Status.Connecting);
                    using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                    {
                        await stream.WriteAsync(Cmd, 0, Cmd.Length, cts.Token);
                        int Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                        if (Length != 1 || Recv[0] != 'y')
                        {
                            MessageBox.Show("错误：设备断开错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                            return;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (IOException)
                {
                    MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                catch (Exception)
                {
                    MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                }
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        private async Task pixelGrid_DrawingCompleted(object sender, EventArgs e)
        {
            try
            {
                await stream.WriteAsync(pixelGrid.GetSSD1306Data(), 0, 1024);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        private async Task pixelGrid_PixelChanged(object sender, EventArgs e)
        {
            try
            {
                await stream.WriteAsync(pixelGrid.GetSSD1306Data(), 0, 1024);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        private async void SerialSyncButton_Click(object sender, EventArgs e)
        {
            byte[] SyncSerialCmd = Encoding.ASCII.GetBytes("{_SerialSyncMode}");
            byte[] Recv = new byte[16];
            try
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.SerialSyncConnecting);
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await stream.WriteAsync(SyncSerialCmd, 0, SyncSerialCmd.Length, cts.Token);
                    int Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                    if (Length != 1 || Recv[0] != 'y')
                    {
                        MessageBox.Show("错误：设备连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                        return;
                    }
                    SystemStatus.ChangeStatus(SystemStatus.Status.SerialSyncConnected);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        private async void CloseSerialSyncButton_Click(object sender, EventArgs e)
        {
            CheckBox DeviceSwitch = (CheckBox)sender;
            byte[] Cmd = Encoding.ASCII.GetBytes("{SSD1306}");
            byte[] SyncScreenCmd = Encoding.ASCII.GetBytes("{_ScreenSyncMode}");
            byte[] Recv = new byte[16];
            try
            {
                SystemStatus.ChangeStatus(SystemStatus.Status.SerialSyncConnecting);
                using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                {
                    await stream.WriteAsync(Cmd, 0, Cmd.Length, cts.Token);
                    int Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                    if (Length != 1 || Recv[0] != 'y')
                    {
                        MessageBox.Show("错误：设备连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                        return;
                    }
                    await stream.WriteAsync(SyncScreenCmd, 0, SyncScreenCmd.Length, cts.Token);
                    Length = await stream.ReadAsync(Recv, 0, Recv.Length, cts.Token);
                    if (Length != 1 || Recv[0] != 'y')
                    {
                        MessageBox.Show("错误：设备连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
                        return;
                    }
                    SystemStatus.ChangeStatus(SystemStatus.Status.SerialSyncOff);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("错误：设备通信超时，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (IOException)
            {
                MessageBox.Show("错误：网络I/O错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("错误：网络超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
            catch (Exception)
            {
                MessageBox.Show("错误：未知异常错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemStatus.ChangeStatus(SystemStatus.Status.Disconnect);
            }
        }

        private void ReadGIFButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "动图 (*.gif)|*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int FrameDelay;
                string FilePath = openFileDialog1.FileName;
                MessageBox.Show("警告：自行二值化后最佳", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                using (Image gifImage = Image.FromFile(FilePath))
                {
                    if(gifImage.Width != 128 || gifImage.Height != 64)
                    {
                        MessageBox.Show("警告：仅支持分辨率为128*64的GIF", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    FrameDimension dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
                    int frameCount = gifImage.GetFrameCount(dimension);
                    for (int i = 0; i < frameCount; i++)
                    {
                        gifImage.SelectActiveFrame(dimension, i);

                        Bitmap frame = new Bitmap(gifImage);
                        GIFBitmap.Add(frame);
                    }

                    PropertyItem delayProp = gifImage.GetPropertyItem(0x5100);
                    FrameDelay = BitConverter.ToInt32(delayProp.Value, 0) * 10;
                }
                SystemStatus.ChangeStatus(SystemStatus.Status.ShowGIFMode);
                pixelGrid.ShowGIF(GIFBitmap, FrameDelay);
            }
        }
    }

    public class SystemStatus : IDisposable
    {
        public enum Status
        {
            Disconnect,
            Connecting,
            Connected,
            SerialSyncOff,
            SerialSyncConnecting,
            SerialSyncConnected,
            ShowGIFMode
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
    public class PixelGridEditor : Panel
    {
        public int PixelSize { get; set; } = 5;
        public int Rows { get; set; } = 64;
        public int Columns { get; set; } = 128;
        public bool[,] PixelData { get; private set; }

        private Bitmap bufferBitmap;
        private Graphics bufferGraphics;
        private bool isDrawing = false;
        private Point lastDrawPoint;
        private bool currentDrawMode;
        private Rectangle lastDrawRegion;
        private bool[,] initialDrawState;
        private bool GIFPlay = false;
        private Thread GIFPlayTheard;

        public event Func<object,EventArgs,Task> PixelChanged;
        public event Func<object,EventArgs,Task> DrawingCompleted;

        public PixelGridEditor()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer, true);

            this.MouseDown += PixelGridEditor_MouseDown;
            this.MouseMove += PixelGridEditor_MouseMove;
            this.MouseUp += PixelGridEditor_MouseUp;
            this.Resize += PixelGridEditor_Resize;
        }

        private void InitializeData()
        {
            PixelData = new bool[Rows, Columns];
            initialDrawState = new bool[Rows, Columns];
            UpdateSize();
        }

        private void UpdateSize()
        {
            this.Size = new Size(Columns * PixelSize, Rows * PixelSize);
            InitializeBuffer();
        }

        private void InitializeBuffer()
        {
            bufferGraphics?.Dispose();
            bufferBitmap?.Dispose();

            if (this.Width > 0 && this.Height > 0)
            {
                bufferBitmap = new Bitmap(this.Width, this.Height);
                bufferGraphics = Graphics.FromImage(bufferBitmap);
                RedrawFullBuffer();
            }
        }

        public async Task ShowBitmap(Bitmap bitmap, int X, int Y)
        {
            if (bitmap == null) return;
            int startX = Math.Max(X, 0);
            int startY = Math.Max(Y, 0);
            int endX = Math.Min(X + bitmap.Width - 1, Columns - 1);
            int endY = Math.Min(Y + bitmap.Height - 1, Rows - 1);
            if (startX > endX || startY > endY) return;

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    int bitmapX = x - X;
                    int bitmapY = y - Y;

                    Color color = bitmap.GetPixel(bitmapX, bitmapY);
                    bool newValue = !(color.R == 0 && color.G == 0 && color.B == 0);

                    PixelData[y, x] = newValue;
                }
            }

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    UpdateBuffer(x, y);
                }
            }

            Rectangle updateRegion = new Rectangle(
                startX * PixelSize,
                startY * PixelSize,
                (endX - startX + 1) * PixelSize,
                (endY - startY + 1) * PixelSize
            );

            this.Invalidate(updateRegion);

            if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
            {
                if (PixelChanged != null)
                {
                    await PixelChanged?.Invoke(this, new PixelChangedEventArgs(0, 0, false));
                }

                if (DrawingCompleted != null)
                {
                    await DrawingCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void GIFShowBitmap(Bitmap bitmap, int X, int Y)
        {
            if (bitmap == null) return;

            int startX = Math.Max(X, 0);
            int startY = Math.Max(Y, 0);
            int endX = Math.Min(X + bitmap.Width - 1, Columns - 1);
            int endY = Math.Min(Y + bitmap.Height - 1, Rows - 1);

            if (startX > endX || startY > endY) return;

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    int bitmapX = x - X;
                    int bitmapY = y - Y;

                    Color color = bitmap.GetPixel(bitmapX, bitmapY);
                    bool newValue = !(color.R == 0 && color.G == 0 && color.B == 0);

                    PixelData[y, x] = newValue;
                }
            }

            if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
            {
                if (PixelChanged != null)
                {
                    PixelChanged?.Invoke(this, new PixelChangedEventArgs(0, 0, false));
                }

                if (DrawingCompleted != null)
                {
                    DrawingCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void GIFPlayThread(ArrayList GIFSource, int FrameDelay)
        {
            while (GIFPlay)
            {
                foreach (Bitmap bmp in GIFSource)
                {
                    if(GIFPlay == false || SystemStatus.CurrentStatus != SystemStatus.Status.ShowGIFMode)
                    {
                        GIFPlay = false;
                        for (int i = 0; i < PixelData.GetLength(0); i++)
                        {
                            for (int j = 0; j < PixelData.GetLength(1); j++)
                            {
                                PixelData[i, j] = false;
                            }
                        }
                        break;
                    }
                    GIFShowBitmap(bmp, 0, 0);
                    Task.Delay(FrameDelay).Wait();
                }
            }
        }

        public void ShowGIF(ArrayList GIFSource,int FrameDelay)
        {
            GIFPlay = true;
            GIFPlayTheard = new Thread(() =>
            {
                GIFPlayThread(GIFSource, FrameDelay);
            }
            );
            GIFPlayTheard.Start();
        }

        public async void Clear()
        {
            if(GIFPlay == true)
            {
                GIFPlay = false;
                GIFPlayTheard.Join();
            }

            Array.Clear(PixelData, 0, PixelData.Length);
            RedrawFullBuffer();

            if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
            {
                if (PixelChanged != null)
                {
                    await PixelChanged?.Invoke(this, new PixelChangedEventArgs(0, 0, false));
                }

                if (DrawingCompleted != null)
                {
                    await DrawingCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void RedrawFullBuffer()
        {
            if (bufferGraphics == null) return;

            bufferGraphics.Clear(Color.Black);
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (PixelData[y, x])
                    {
                        bufferGraphics.FillRectangle(Brushes.Cyan,
                            x * PixelSize, y * PixelSize, PixelSize, PixelSize);
                    }
                }
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bufferBitmap != null)
            {
                e.Graphics.DrawImage(bufferBitmap, 0, 0);
            }
        }

        private (int x, int y) GetPixelPosition(int mouseX, int mouseY)
        {
            int x = PixelGridEditor.Clamp(mouseX / PixelSize, 0, Columns - 1);
            int y = PixelGridEditor.Clamp(mouseY / PixelSize, 0, Rows - 1);
            return (x, y);
        }

        private void PixelGridEditor_MouseDown(object sender, MouseEventArgs e)
        {
            var (x, y) = GetPixelPosition(e.X, e.Y);

            Array.Copy(PixelData, initialDrawState, PixelData.Length);

            currentDrawMode = !PixelData[y, x];

            SetPixel(x, y, currentDrawMode);

            lastDrawPoint = new Point(x, y);
            lastDrawRegion = new Rectangle(x, y, 1, 1);

            isDrawing = true;
        }

        private void PixelGridEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            var (currentX, currentY) = GetPixelPosition(e.X, e.Y);
            Point currentPoint = new Point(currentX, currentY);

            if (currentPoint == lastDrawPoint) return;

            int minX = Math.Min(currentX, lastDrawPoint.X);
            int maxX = Math.Max(currentX, lastDrawPoint.X);
            int minY = Math.Min(currentY, lastDrawPoint.Y);
            int maxY = Math.Max(currentY, lastDrawPoint.Y);

            RestoreRegion(minX, minY, maxX, maxY);

            DrawLine(lastDrawPoint, currentPoint, currentDrawMode);

            lastDrawPoint = currentPoint;

            lastDrawRegion = Rectangle.Union(lastDrawRegion,
                new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1));
        }

        private void RestoreRegion(int minX, int minY, int maxX, int maxY)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (PixelData[y, x] != initialDrawState[y, x])
                    {
                        PixelData[y, x] = initialDrawState[y, x];
                        UpdateBuffer(x, y);
                    }
                }
            }
        }

        private async void PixelGridEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            isDrawing = false;

            if (e.Button == MouseButtons.Left)
            {
                var (x, y) = GetPixelPosition(e.X, e.Y);
                if (x != lastDrawPoint.X || y != lastDrawPoint.Y)
                {
                    PixelGridEditor_MouseMove(sender, e);
                }
            }

            if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
            {
                if (DrawingCompleted != null)
                {
                    await DrawingCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void DrawLine(Point start, Point end, bool state)
        {
            int dx = Math.Abs(end.X - start.X);
            int dy = Math.Abs(end.Y - start.Y);
            int sx = start.X < end.X ? 1 : -1;
            int sy = start.Y < end.Y ? 1 : -1;
            int err = dx - dy;

            Point current = start;

            while (true)
            {
                SetPixel(current.X, current.Y, state);

                if (current.X == end.X && current.Y == end.Y) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    current.X += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    current.Y += sy;
                }
            }
        }

        private async void SetPixel(int x, int y, bool state)
        {
            if (x < 0 || x >= Columns || y < 0 || y >= Rows)
                return;

            if (PixelData[y, x] != state)
            {
                PixelData[y, x] = state;

                UpdateBuffer(x, y);

                if (SystemStatus.CurrentStatus >= SystemStatus.Status.Connected)
                {
                    if(PixelChanged != null)
                    {
                        await PixelChanged?.Invoke(this, new PixelChangedEventArgs(x, y, state));
                    }
                }
            }
        }

        private void UpdateBuffer(int x, int y)
        {
            if (bufferGraphics == null) return;

            Rectangle rect = new Rectangle(
                x * PixelSize,
                y * PixelSize,
                PixelSize,
                PixelSize);

            bufferGraphics.FillRectangle(
                PixelData[y, x] ? Brushes.Cyan : Brushes.Black,
                rect);

            this.Invalidate(rect);
        }

        private void PixelGridEditor_Resize(object sender, EventArgs e)
        {
            InitializeBuffer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bufferGraphics?.Dispose();
                bufferBitmap?.Dispose();
            }
            base.Dispose(disposing);
        }
        public byte[] GetPixelData()
        {
            int byteWidth = (Columns + 7) / 8;
            byte[] result = new byte[byteWidth * Rows];

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (PixelData[y, x])
                    {
                        int byteIndex = y * byteWidth + x / 8;
                        int bitIndex = 7 - (x % 8);
                        result[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }
            }

            return result;
        }

        public byte[] GetSSD1306Data()
        {
            int pages = Rows / 8;
            byte[] result = new byte[Columns * pages];

            for (int page = 0; page < pages; page++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    byte value = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int y = page * 8 + bit;
                        if (PixelData[y, x])
                        {
                            value |= (byte)(1 << bit);
                        }
                    }
                    result[page * Columns + x] = value;
                }
            }
            return result;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
    public class PixelChangedEventArgs : EventArgs
    {
        public int X { get; }
        public int Y { get; }
        public bool State { get; }

        public PixelChangedEventArgs(int x, int y, bool state)
        {
            X = x;
            Y = y;
            State = state;
        }
    }

    public class BinarizedPictureBox : PictureBox
    {
        private bool _isMoving = false;
        private Point _lastMousePosition;
        private Bitmap _originalImage;
        private bool _isBinarized = true;
        private int _threshold = 128;
        private float _zoomFactor = 1.0f;
        
        public Bitmap BitmapSource { get; private set; }

        public BinarizedPictureBox()
        {
            this.BackColor = Color.Transparent;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.MouseDown += (s,e) => StartMoving();
            this.MouseUp += (s, e) => StopMoving();
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public int Threshold
        {
            get => _threshold;
            set
            {
                _threshold = (int)BinarizedPictureBox.Clamp(value, 0, 255);
                if (_originalImage != null)
                    ApplyProcessing();
            }
        }

        public bool IsBinarized
        {
            get => _isBinarized;
            set
            {
                _isBinarized = value;
                if (_originalImage != null)
                    ApplyProcessing();
            }
        }

        public float ZoomFactor
        {
            get => _zoomFactor;
            set
            {
                _zoomFactor = BinarizedPictureBox.Clamp(value, 0.1f, 10.0f);
                if (_originalImage != null)
                    ApplyProcessing();
            }
        }

        public void SetImage(Bitmap image)
        {
            _originalImage?.Dispose();
            _originalImage = image;
            BitmapSource = image;

            if (image == null)
            {
                this.Image = null;
                this.Visible = false;
                StopMoving();
                return;
            }
            ApplyProcessing();
        }

        public void SetPosition(int X, int Y)
        {
            if (Parent != null)
            {
                Location = new Point(
                    X,
                    Y);
            }
        }

        private void ApplyProcessing()
        {
            if (_originalImage == null) return;

            int newWidth = (int)(_originalImage.Width * _zoomFactor);
            int newHeight = (int)(_originalImage.Height * _zoomFactor);
            Bitmap scaledImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(_originalImage, 0, 0, newWidth, newHeight);
            }

            Bitmap processedImage = _isBinarized ?
                BinarizeImage(scaledImage) :
                scaledImage;

            this.Image?.Dispose();
            this.Image = processedImage;
            this.Visible = true;
            this.Size = new Size(processedImage.Width, processedImage.Height);
        }

        private Bitmap BinarizeImage(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    int intensity = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    Color newColor = intensity > _threshold ? Color.White : Color.Black;
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }

        private void StartMoving()
        {
            if (_isMoving) return;

            _isMoving = true;
            _lastMousePosition = Control.MousePosition;
            Application.Idle += FollowMouse;
        }

        public void StopMoving()
        {
            if (!_isMoving) return;

            _isMoving = false;
            Application.Idle -= FollowMouse;
        }

        //private void ResetPosition()
        //{
        //    if (Parent != null)
        //    {
        //        Location = new Point(
        //            (Parent.Width - Width) / 2,
        //            (Parent.Height - Height) / 2);
        //    }
        //    StopMoving();
        //}

        private void FollowMouse(object sender, EventArgs e)
        {
            if (!_isMoving || Parent == null) return;

            Point currentMouse = Control.MousePosition;
            if (currentMouse == _lastMousePosition) return;

            Point parentPoint = Parent.PointToClient(currentMouse);

            int targetX = parentPoint.X - Width / 2;
            int targetY = parentPoint.Y - Height / 2;

            targetX = (int)(Math.Round(targetX / 5.0) * 5);
            targetY = (int)(Math.Round(targetY / 5.0) * 5);

            Location = new Point(targetX, targetY);

            _lastMousePosition = currentMouse;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        protected override void Dispose(bool disposing)
        {
            StopMoving();
            _originalImage?.Dispose();
            base.Dispose(disposing);
        }
    }

    public class SendText
    {
        TextBox TextBoxObj { get; set; }

        Font TextFont;

        public SendText(TextBox Obj)
        {
            TextBoxObj = Obj;
        }

        public void TextFontChanged(string FontName, int Size)
        {
            Font newFont = new Font(FontName, Size);
            if (TextFont != null)
            {
                TextFont.Dispose();
            }
            TextFont = newFont;
            TextBoxObj.Font = TextFont;
        }

        public void Dispose()
        {
            if (TextFont != null)
            {
                TextFont.Dispose();
            }
        }
    }

    public class ConvertBitmap : IDisposable
    {
        private bool _disposed = false;
        public Bitmap PreviewBitmap { get; private set; }
        public int[,] PixelMatrix { get; private set; }
        public int Threshold { get; set; } = 128;
        public bool Invert { get; set; } = false;
        public Size TargetSize { get; set; } = Size.Empty;
        public float Scale { get; set; } = 1;


        public void ConvertTextToBitmap(string text, Font font)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("输入文本不能为空");
            }

            DisposeBitmap();
            PixelMatrix = null;

            using (Bitmap tempBmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(tempBmp))
            {
                SizeF textSize = g.MeasureString(text, font);

                int width = (int)Math.Ceiling(textSize.Width);
                int height = (int)Math.Ceiling(textSize.Height);

                width = Math.Max(width, 1);
                height = Math.Max(height, 1);

                using (Bitmap textBmp = new Bitmap(width, height))
                using (Graphics textG = Graphics.FromImage(textBmp))
                {
                    textG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    textG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    textG.Clear(Color.White);
                    textG.DrawString(text, font, Brushes.Black, PointF.Empty);
                    Bitmap processedBmp = textBmp;
                    if (!TargetSize.IsEmpty && (TargetSize.Width > 0 && TargetSize.Height > 0))
                    {
                        processedBmp = new Bitmap(textBmp, TargetSize);
                    }

                    ProcessBitmap(processedBmp);

                    if (processedBmp != textBmp)
                    {
                        processedBmp.Dispose();
                    }
                }
            }
        }

        private void ProcessBitmap(Bitmap sourceBmp)
        {
            int width = sourceBmp.Width;
            int height = sourceBmp.Height;

            PixelMatrix = new int[height, width];

            BitmapData bmpData = sourceBmp.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                byte[] pixelBuffer = new byte[bmpData.Stride * bmpData.Height];
                Marshal.Copy(bmpData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int index = y * bmpData.Stride + x * 4;

                        byte blue = pixelBuffer[index];
                        byte green = pixelBuffer[index + 1];
                        byte red = pixelBuffer[index + 2];

                        int gray = (red * 299 + green * 587 + blue * 114) / 1000;

                        int value = (gray < Threshold) ? 1 : 0;

                        if (Invert) value = 1 - value;

                        PixelMatrix[y, x] = value;
                    }
                }
            }
            finally
            {
                sourceBmp.UnlockBits(bmpData);
            }

            CreatePreviewBitmap(width, height);
        }

        private void CreatePreviewBitmap(int width, int height)
        {
            DisposeBitmap();
            PreviewBitmap = new Bitmap((int)(width * Scale), (int)(height * Scale));

            using (Graphics g = Graphics.FromImage(PreviewBitmap))
            {
                g.Clear(Color.Black);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (PixelMatrix[y, x] == 1)
                        {
                            g.FillRectangle(Brushes.White,
                                           x * Scale,
                                           y * Scale,
                                           Scale,
                                           Scale);
                        }
                    }
                }
            }
        }

        private void DisposeBitmap()
        {
            if (PreviewBitmap != null)
            {
                PreviewBitmap.Dispose();
                PreviewBitmap = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DisposeBitmap();
                    PixelMatrix = null;
                }
                _disposed = true;
            }
        }

        ~ConvertBitmap()
        {
            Dispose(false);
        }
    }
}