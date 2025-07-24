using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using ModNamespace;
using System.Drawing;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace WirelessSTM32ISP
{
    public partial class MainForm : Form
    {
        private IMod Module {  get; set; }
        DownloadProgram ProgObj;
        public MainForm(IMod mod)
        {
            InitializeComponent();
            Module = mod;
        }

        private void SelectBinButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog1.FileName;
                FilePath.Text = selectedFile;
                FilePath.ForeColor = Color.DeepSkyBlue;
            }
            this.Show();
            this.TopMost = true;
            this.TopMost = false;
        }

        private async void DownloadProgramButton_Click(object sender, EventArgs e)
        {
            DownloadProgramButton.Enabled = false;
            EraseFlashButton.Enabled = false;
            SelectBinButton.Enabled = false;
            DownloadStopButton.Enabled = false;
            if (FilePath.Text.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
            {
                ProgObj = new DownloadProgram(Module.stream,FilePath.Text,ProgressAdjust);
                ProgObj.StartDownload();
                DownloadStopButton.Enabled = true;
                while (!ProgObj.IsComplete)
                {
                    await Task.Delay(100);
                }
            }
            else
            {
                MessageBox.Show($"发生错误:请选择Bin文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ProgObj = null;
            DownloadProgramButton.Enabled = true;
            EraseFlashButton.Enabled = true;
            SelectBinButton.Enabled = true;
            DownloadStopButton.Enabled = false;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files != null && files.Length > 0)
                    {
                        if (files[0].EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
                        {
                            string path = files[0];
                            FilePath.Text = path;
                            FilePath.ForeColor = Color.DeepSkyBlue;
                        }
                        else
                        {
                            MessageBox.Show($"发生错误:仅支持Bin文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Cursor = System.Windows.Forms.Cursors.Arrow;
                    }
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                MessageBox.Show($"发生错误: 文件选择错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ProgressAdjust(string Status, int Progress)
        {
            if (StatusTextBox.InvokeRequired)
            {
                StatusTextBox.Invoke(new MethodInvoker(delegate
                {
                    StatusTextBox.Text = Status;
                }
                ));
            }

            if (DownloadProgress.InvokeRequired)
            {
                DownloadProgress.Invoke(new MethodInvoker(delegate
                {
                    DownloadProgress.Value = Progress;
                }
                ));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ProgObj != null)
            {
                ProgObj.StopDownload();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void DownloadStopButton_Click(object sender, EventArgs e)
        {
            ProgObj.StopDownload();
        }

        private async void EraseFlashButton_Click(object sender, EventArgs e)
        {
            DownloadProgramButton.Enabled = false;
            EraseFlashButton.Enabled = false;
            SelectBinButton.Enabled = false;
            DownloadStopButton.Enabled = false;
            ProgObj = new DownloadProgram(Module.stream, FilePath.Text, ProgressAdjust);
            ProgObj.StartErase();
            DownloadStopButton.Enabled = true;
            while (!ProgObj.IsComplete)
            {
                await Task.Delay(100);
            }
            ProgObj = null;
            DownloadProgramButton.Enabled = true;
            EraseFlashButton.Enabled = true;
            SelectBinButton.Enabled = true;
            DownloadStopButton.Enabled = false;
        }
    }

    public class DownloadProgram
    {
        private NetworkStream stream { get; set; }
        private string FilePath { get; set; }

        private Action<string, int> ProgressAdjust { get; set; }

        private Thread DownloadThread;

        private bool StopFlag = false;

        private long StartTime;

        private long EndTime;

        public bool IsComplete { get; private set; }

        public DownloadProgram(NetworkStream _stream, string file, Action<string, int> action)
        {
            stream = _stream;
            IsComplete = false;
            FilePath = file;
            ProgressAdjust = action;
            StopFlag = false;
        }

        public void StartDownload()
        {
            DownloadThread = new Thread(Download);
            DownloadThread.Start();
        }

        public void StartErase()
        {
            DownloadThread = new Thread(EraseFlash);
            DownloadThread.Start();
        }

        public void StopDownload()
        {
            IsComplete = true;
            StopFlag = true;
            DownloadThread.Join();
        }

        private byte[] ReadFile(FileStream file, long StartIndex, int Number)
        {
            byte[] buffer = new byte[Number];
            file.Seek(StartIndex, SeekOrigin.Begin);
            int bytesRead = file.Read(buffer, 0, Number);
            if (bytesRead < Number)
            {
                byte[] result = new byte[bytesRead];
                Array.Copy(buffer, result, bytesRead);
                return result;
            }

            return buffer;
        }

        public static string GenerateJson(long fileSize)
        {
            var data = new
            {
                FileSize = fileSize
            };

            return JsonSerializer.Serialize(data);
        }

        private async void Download()
        {
            StartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            byte[] Data = new byte[2048];
            byte[] RecvData = new byte[16];
            byte[] Command = Encoding.ASCII.GetBytes("{STM32ISPPROG}");
            long WriteSize = 0;
            long FileSize = 0;
            if (StopFlag)
            {
                return;
            }

            if (!File.Exists(FilePath))
            {
                IsComplete = true;
                StopFlag = true;
                MessageBox.Show($"发生错误: 文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (StopFlag)
            {
                return;
            }

            using (FileStream BinFileStream = File.OpenRead(FilePath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(FilePath);
                    FileSize = fileInfo.Length;
                    if (FileSize <= 0)
                    {
                        IsComplete = true;
                        StopFlag = true;
                        ProgressAdjust("终止下载", 0);
                        MessageBox.Show($"发生错误: 文件大小读取异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch
                {
                    IsComplete = true;
                    StopFlag = true;
                    ProgressAdjust("终止下载", 0);
                    MessageBox.Show($"发生错误: 文件无法读取", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (StopFlag)
                {
                    return;
                }

                try
                {
                    using (CancellationTokenSource cts = new CancellationTokenSource(5000))
                    {
                        ProgressAdjust("发送下载命令...", 0);
                        await stream.WriteAsync(Command, 0, Command.Length, cts.Token);

                        if (StopFlag)
                        {
                            return;
                        }

                        ProgressAdjust("等待设备响应...", 4);
                        int Num = await stream.ReadAsync(RecvData, 0, RecvData.Length, cts.Token);
                        if (Num != 1 || RecvData[0] != 'y')
                        {
                            IsComplete = true;
                            StopFlag = true;
                            ProgressAdjust($"终止下载，错误代码：{RecvData[0]:X2}", 0);
                            MessageBox.Show($"发生错误: 下载失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string JsonData = GenerateJson(FileSize);
                        byte[] JsonByte = Encoding.UTF8.GetBytes(JsonData);
                        ProgressAdjust("发送任务参数...", 6);
                        await stream.WriteAsync(JsonByte, 0, JsonByte.Length, cts.Token);

                        if (StopFlag)
                        {
                            return;
                        }
                    }

                    if (StopFlag)
                    {
                        return;
                    }

                    ProgressAdjust("开始下载程序", 10);

                    while (!StopFlag && (FileSize - WriteSize) > 0)
                    {
                        using (CancellationTokenSource cts = new CancellationTokenSource(15000))
                        {
                            int Num = await stream.ReadAsync(RecvData, 0, RecvData.Length, cts.Token);
                            if (Num != 1 || RecvData[0] != 'c')
                            {
                                IsComplete = true;
                                StopFlag = true;
                                ProgressAdjust($"终止下载，错误代码：{RecvData[0]:X2}", 0);
                                MessageBox.Show($"发生错误: 下载失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            for (int i = 0; i < 8; i++)
                            {
                                if (StopFlag)
                                {
                                    return;
                                }

                                byte[] data_bytes;

                                if (FileSize - WriteSize > 1280)
                                {
                                    data_bytes = ReadFile(BinFileStream, WriteSize, 1280);
                                    WriteSize += 1280;
                                }
                                else
                                {
                                    data_bytes = ReadFile(BinFileStream, WriteSize, (int)(FileSize - WriteSize));
                                    WriteSize = WriteSize + FileSize - WriteSize;
                                }

                                await stream.WriteAsync(data_bytes, 0, data_bytes.Length, cts.Token);

                                ProgressAdjust($"正在下载：{(WriteSize / 1024.0):F2} KB / {(FileSize / 1024.0):F2} KB", 10 + (int)(((float)WriteSize / (float)FileSize) * 80.0));
                            }
                        }
                    }

                    if (StopFlag)
                    {
                        return;
                    }

                    ProgressAdjust($"正在烧录：{(WriteSize / 1024.0):F2} KB / {(FileSize / 1024.0):F2} KB", 90);

                    while (!StopFlag)
                    {
                        try
                        {
                            using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                            {
                                int ReadNum = await stream.ReadAsync(RecvData, 0, RecvData.Length);
                                if (ReadNum != 1 || RecvData[0] != 'y')
                                {
                                    IsComplete = true;
                                    StopFlag = true;
                                    ProgressAdjust($"终止下载，错误代码：{RecvData[0]:X2}", 0);
                                    MessageBox.Show($"发生错误: 下载失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                EndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                long SpendTime = EndTime - StartTime;
                                if (SpendTime > int.MaxValue)
                                {
                                    ProgressAdjust($"烧录完成,用时：你干了什么？", 100);
                                }
                                else
                                {
                                    ProgressAdjust($"烧录完成：{WriteSize} Byte，用时：{SpendTime} ms", 100);
                                }
                                IsComplete = true;
                                return;
                            }
                        }
                        catch (OperationCanceledException)
                        {

                        }
                    }
                    ProgressAdjust($"终止下载", 0);
                }
                catch (OperationCanceledException)
                {
                    IsComplete = true;
                    StopFlag = true;
                    ProgressAdjust("终止下载", 0);
                    MessageBox.Show($"发生错误: 网络超时，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    IsComplete = true;
                    StopFlag = true;
                    ProgressAdjust("终止下载", 0);
                    MessageBox.Show($"发生错误: 网络连接断开，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SocketException)
                {
                    IsComplete = true;
                    StopFlag = true;
                    ProgressAdjust("终止下载", 0);
                    MessageBox.Show($"发生错误: 网络连接被重置，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    IsComplete = true;
                    StopFlag = true;
                    ProgressAdjust("终止下载", 0);
                    MessageBox.Show($"发生错误: 未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void EraseFlash()
        {
            StartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            byte[] Data = new byte[2048];
            byte[] RecvData = new byte[16];
            byte[] Command = Encoding.ASCII.GetBytes("{STM32ISPPROG}");
            long FileSize = 0;
            FileSize = 8;

            if (StopFlag)
            {
                return;
            }

            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(5000))
                {
                    ProgressAdjust("发送擦除命令...", 0);
                    await stream.WriteAsync(Command, 0, Command.Length, cts.Token);

                    if (StopFlag)
                    {
                        return;
                    }

                    ProgressAdjust("等待设备响应...", 20);
                    int Num = await stream.ReadAsync(RecvData, 0, RecvData.Length, cts.Token);
                    if (Num != 1 || RecvData[0] != 'y')
                    {
                        IsComplete = true;
                        StopFlag = true;
                        ProgressAdjust($"终止擦除，错误代码：{RecvData[0]:X2}", 0);
                        MessageBox.Show($"发生错误: 擦除失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string JsonData = GenerateJson(FileSize);
                    byte[] JsonByte = Encoding.UTF8.GetBytes(JsonData);
                    ProgressAdjust("发送任务参数...", 40);
                    await stream.WriteAsync(JsonByte, 0, JsonByte.Length, cts.Token);

                    if (StopFlag)
                    {
                        return;
                    }
                }

                if (StopFlag)
                {
                    return;
                }

                ProgressAdjust("开始擦除程序", 60);

                using (CancellationTokenSource cts = new CancellationTokenSource(15000))
                {
                    int Num = await stream.ReadAsync(RecvData, 0, RecvData.Length, cts.Token);
                    if (Num != 1 || RecvData[0] != 'c')
                    {
                        IsComplete = true;
                        StopFlag = true;
                        ProgressAdjust($"终止擦除，错误代码：{RecvData[0]:X2}", 0);
                        MessageBox.Show($"发生错误: 擦除失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (StopFlag)
                    {
                        return;
                    }

                    byte[] data_bytes = {0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};

                    await stream.WriteAsync(data_bytes, 0, data_bytes.Length, cts.Token);
                }

                if (StopFlag)
                {
                    return;
                }

                ProgressAdjust($"正在擦除", 90);

                while (!StopFlag)
                {
                    try
                    {
                        using (CancellationTokenSource cts = new CancellationTokenSource(3000))
                        {
                            int ReadNum = await stream.ReadAsync(RecvData, 0, RecvData.Length);
                            if (ReadNum != 1 || RecvData[0] != 'y')
                            {
                                IsComplete = true;
                                StopFlag = true;
                                ProgressAdjust($"终止擦除，错误代码：{RecvData[0]:X2}", 0);
                                MessageBox.Show($"发生错误: 擦除失败，请尝试重启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            EndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            long SpendTime = EndTime - StartTime;
                            if (SpendTime > int.MaxValue)
                            {
                                ProgressAdjust($"擦除完成,用时：你干了什么？", 100);
                            }
                            else
                            {
                                ProgressAdjust($"擦除完成，用时：{SpendTime} ms", 100);
                            }
                            IsComplete = true;
                            return;
                        }
                    }
                    catch (OperationCanceledException)
                    {

                    }
                }
                ProgressAdjust($"终止擦除", 0);
            }
            catch (OperationCanceledException)
            {
                IsComplete = true;
                StopFlag = true;
                ProgressAdjust("终止擦除", 0);
                MessageBox.Show($"发生错误: 网络超时，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                IsComplete = true;
                StopFlag = true;
                ProgressAdjust("终止擦除", 0);
                MessageBox.Show($"发生错误: 网络连接断开，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException)
            {
                IsComplete = true;
                StopFlag = true;
                ProgressAdjust("终止擦除", 0);
                MessageBox.Show($"发生错误: 网络连接被重置，请检查网络", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                IsComplete = true;
                StopFlag = true;
                ProgressAdjust("终止擦除", 0);
                MessageBox.Show($"发生错误: 未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
