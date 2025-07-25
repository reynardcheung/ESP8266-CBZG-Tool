/* 
 * Copyright (c) 2025, 长不着弓
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain this notice.
 * 2. Redistributions in binary form must reproduce this notice in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of 长不着弓 nor the names of its contributors may be used
 *    to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DAMAGES.
 */
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.DataFormats;
using Microsoft.VisualBasic;

namespace WirelessDownloadTool3._5
{
    public partial class Form1 : Form
    {
        Server? ListeningServer = null;
        Form2? ManagerForm = null;

        public Form1()
        {
            InitializeComponent();
            LoadAvailableIPs();
        }

        private void LoadAvailableIPs()
        {
            AddressComboBox.Items.Clear(); // 清空 ComboBox  

            // 获取所有网络接口  
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // 确保接口处于活动状态  
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    // 获取接口的所有 IP 地址  
                    foreach (var addr in ni.GetIPProperties().UnicastAddresses)
                    {
                        // 只处理 IPv4 地址  
                        if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            AddressComboBox.Items.Add(addr.Address.ToString()); // 添加到 ComboBox  
                        }
                    }
                }
            }

            // 检查是否有项可供选择，若有则选择第一个  
            if (AddressComboBox.Items.Count > 0)
            {
                AddressComboBox.SelectedIndex = 0; // 自动选择第一个项  
                //Server.IPAddr = AddressComboBox.Text;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(PortTextBox.Text, out _))
            {
                PortTextBox.Text = new string([.. PortTextBox.Text.Where(char.IsDigit)]);
                PortTextBox.SelectionStart = PortTextBox.Text.Length;
            }
        }

        private void ComboBox1_DropDown(object sender, EventArgs e)
        {
            LoadAvailableIPs();
        }

        private async void Listener_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortTextBox.Text == "")
                {
                    MessageBox.Show($"发生错误: 未填写端口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListeningServer = new Server(AddressComboBox.Text, int.Parse(PortTextBox.Text));
                ManagerForm = new Form2(ListeningServer);
                ListeningServer.Start(ManagerForm);

                await Task.Delay(100);

                this.Hide();
                ManagerForm.ShowDialog();
                this.Show();

                ManagerForm?.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class SystemConnectStatus
    {
        public enum SysStatus
        {
            Disconnected = 0,
            Connecting = 1,
            Connected = 2,
            UsingModule = 3
        };

        private static bool ContainsDelegate(Delegate? del, Delegate target)
        {
            if (del == null)
            {
                return false;
            }

            foreach (var d in del.GetInvocationList())
            {
                if (del == null)
                {
                    return false;
                }

                if (d == target)
                {
                    return true;
                }
            }
            return false;
        }

        public delegate void StatusChangedHandler(object sender, SysStatus e);

        public event StatusChangedHandler? StatusChanged_;

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

    public class Server
    {
        public string IPAddr;
        public int ListenPort;
        private Form2? MangagerForm;
        private readonly Thread listenerThreadHandle;
        private bool ListerningFlag = false;
        public Server(string ip,int port)
        {
            IPAddr = ip;
            ListenPort = port;
            listenerThreadHandle = new Thread(this.ListeningThread);
        }

        public void Start(Form2 form)
        {
            MangagerForm = form;
            ListerningFlag = true;
            listenerThreadHandle.IsBackground = true; 
            listenerThreadHandle.Start();
        }

        public void Stop()
        {
            ListerningFlag = false;
            if(listenerThreadHandle.ThreadState == ThreadState.Background)
            {
                listenerThreadHandle.Join();
            }
        }

        private void ListeningThread()
        {
            TcpListener? listener = null;

            try
            {
                // 创建TcpListener监听指定的IP地址和端口  
                listener = new TcpListener(IPAddress.Parse(IPAddr), ListenPort);
                listener.Start(); // 开始监听  

                Console.WriteLine($"正在监听 IP: {IPAddr} 端口: {ListenPort}...");

                while (ListerningFlag)
                {
                    if (listener.Pending()) // 检查是否有连接请求  
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        if(MangagerForm == null)
                        {
                            ListerningFlag = false;
                            continue;
                        }
                        ESPDevice device = new(client, MangagerForm);

                        // 处理连接  
                    }
                    else
                    {
                        Thread.Sleep(100); // 减小 CPU 占用  
                    }
                }
            }
            catch (SocketException)
            {
                ListerningFlag = false;

                if(MangagerForm != null)
                {
                    while (!MangagerForm.IsHandleCreated)
                    {
                        ;
                    }

                    MangagerForm.Invoke(() =>
                    {
                        MangagerForm?.Close();
                        MangagerForm?.Dispose();
                    });
                }

                MessageBox.Show($"发生错误: 端口被占用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                ListerningFlag = false;

                if (MangagerForm != null)
                {
                    while (!MangagerForm.IsHandleCreated)
                    {
                        ;
                    }

                    MangagerForm.Invoke(() =>
                    {
                        MangagerForm?.Close();
                        MangagerForm?.Dispose();
                    });
                }

                MessageBox.Show($"发生错误: 未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener.Dispose();
                    listener = null;
                }
            }
        }

        //private static void HandleClient(TcpClient client)
        //{
        //    try
        //    {
        //        using (var networkStream = client.GetStream())
        //        {
        //            byte[] buffer = new byte[1024];
        //            int bytesRead;

        //            // 读取客户端发送的数据  
        //            while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                // 将接收到的数据转换为字符串  
        //                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        //                Console.WriteLine($"接收到的数据: {receivedData}");

        //                // 发送响应给客户端  
        //                byte[] responseData = Encoding.UTF8.GetBytes("已收到数据");
        //                networkStream.Write(responseData, 0, responseData.Length);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"处理客户端时发生错误: {ex.Message}");
        //    }
        //    finally
        //    {
        //        client.Close(); // 关闭客户端连接

        //        Console.WriteLine("客户端连接已关闭.");
        //    }
        //}
    }
}