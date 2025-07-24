using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WirelessDownloadTool3._5
{
    public class ESPDevice
    {
        private bool _deviceConnectAlive;
        public NetworkStream DeviceStream { get; set; }
        public string? DeviceID { get; }
        public string? DeviceVersion { get; }

        public bool DeviceConnect = false;

        public List<Command>? CommandList;

        public bool DeviceConnectAlive
        {
            get { return _deviceConnectAlive; }
            set
            {
                // 在这里添加你的逻辑  
                if (value != _deviceConnectAlive)
                {
                    Console.WriteLine($"设备连接状态更新：{DeviceID} -> {_deviceConnectAlive}");

                    // 设置新值  
                    _deviceConnectAlive = value;
                }
            }
        }

        public static readonly DeviceGroup Group = new();

        public ESPDevice(TcpClient client, Form2 MangagerForm)
        {
            client.SendTimeout = 5000;
            client.ReceiveTimeout = 5000;
            client.NoDelay = true;
            client.Client.SendBufferSize = 1280;
            DeviceStream  = client.GetStream();
            DeviceID = DeviceGetID(this).Result;
            DeviceVersion = DeviceGetVersion(this).Result;
            if (DeviceConnect && DeviceID != null && DeviceVersion != null)
            {
                foreach(var device in ESPDevice.Group.ESPDeviceGroup)
                {
                    if(DeviceID == device.DeviceID)
                    {
                        device.DeviceStream = DeviceStream;
                        return;
                    }
                }

                MangagerForm.AddItemsInCombobox(Group, this);
                DeviceConnectAlive = true;
            }
            else
            {
                client.Close();
                client.Dispose();
            }
        }

        private static async Task<string?> DeviceGetID(ESPDevice device)
        {
            NetworkStream stream = device.DeviceStream;

            var helper = new NetworkHelper(stream);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("{ESPGETID}");
            byte[] read_buffer = new byte[28];
            TimeSpan timeout = TimeSpan.FromSeconds(5);

            try
            {
                await helper.WriteWithTimeoutAsync(buffer, 0, buffer.Length, timeout);
                Console.WriteLine("数据写入成功。");

                int bytesRead = await helper.ReadWithTimeoutAsync(read_buffer, 0, read_buffer.Length, timeout);
                Console.WriteLine($"读取了 {bytesRead} 字节。");
                if (bytesRead > 0)
                {
                    device.DeviceConnect = true;
                    return ASCIIEncoding.UTF8.GetString(read_buffer, 0, bytesRead);
                }
            }
            catch (TimeoutException ex)
            {
                device.DeviceConnectAlive = false;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            device.DeviceConnect = false;
            return null;
        }

        private static async Task<string?> DeviceGetVersion(ESPDevice device)
        {
            NetworkStream stream = device.DeviceStream;

            var helper = new NetworkHelper(stream);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("{ESPGETVERSION}");
            byte[] read_buffer = new byte[28];
            TimeSpan timeout = TimeSpan.FromSeconds(5);

            try
            {
                await helper.WriteWithTimeoutAsync(buffer, 0, buffer.Length, timeout);
                Console.WriteLine("数据写入成功。");

                int bytesRead = await helper.ReadWithTimeoutAsync(read_buffer, 0, read_buffer.Length, timeout);
                Console.WriteLine($"读取了 {bytesRead} 字节。");
                if (bytesRead > 0)
                {
                    device.DeviceConnect = true;
                    return ASCIIEncoding.UTF8.GetString(read_buffer, 0, bytesRead);
                }
            }
            catch (TimeoutException ex)
            {
                device.DeviceConnectAlive = false;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            device.DeviceConnect = false;
            return null;
        }

        public static async Task<List<ModuleInfo>?> ReadAndStoreModulesAsync(NetworkHelper networkHelper, int bufferSize, TimeSpan timeout)
        {
            byte[] buffer = new byte[bufferSize];

            try
            {
                int bytesRead = await networkHelper.ReadWithTimeoutAsync(buffer, 0, bufferSize, timeout);

                return ParseModuleData([.. buffer.Take(bytesRead)]);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"读取操作超时: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取数据时发生错误: {ex.Message}");
                return null;
            }
        }

        private static List<ModuleInfo> ParseModuleData(byte[] data)
        {
            List<ModuleInfo> modules = [];

            string rawData = System.Text.Encoding.UTF8.GetString(data);

            string[] moduleEntries = rawData.Split(["()"], StringSplitOptions.RemoveEmptyEntries);

            foreach (string entry in moduleEntries)
            {
                string[] fields = entry.Split(["[]"], StringSplitOptions.None);

                if (fields.Length >= 4)
                {
                    string name = fields[0].Trim() != "N" ? fields[0] : "NULL";
                    string description = fields[1].Trim() != "N" ? fields[1] : "NULL";
                    int version = int.TryParse(fields[2], out int v) ? v : 0;
                    string command = fields[3].Trim() != "N" ? fields[3].Replace("{", "").Replace("}", "") : "NULL";

                    modules.Add(new ModuleInfo(name, description, version, command));
                }
            }

            return modules;
        }

    }
    public class ModuleInfo(string name, string description, int version, string command)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public int Version { get; set; } = version;
        public string Command { get; set; } = command;

        public override string ToString()
        {
            return $"{Name} - {Description} (Version: {Version}, Command: {Command})";
        }
    }

    public class DeviceGroup
    {  
        public BindingList<ESPDevice> ESPDeviceGroup = [];
    }

    public class NetworkHelper(NetworkStream networkStream)
    {
        private readonly NetworkStream _networkStream = networkStream;

        public async Task<int> ReadWithTimeoutAsync(byte[] buffer, int offset, int size, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);

            try
            {
                return await Task.Run(() =>
                {
                    return _networkStream.ReadAsync(buffer, offset, size, cts.Token).Result;
                }, cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException("操作超时。");
            }
            catch (Exception ex)
            {
                throw new Exception("读取数据时发生错误。", ex);
            }
        }

        public async Task WriteWithTimeoutAsync(byte[] buffer, int offset, int size, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);

            try
            {
                await Task.Run(() =>
                {
                    _networkStream.WriteAsync(buffer, offset, size, cts.Token).Wait();
                }, cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException("写入操作超时。");
            }
            catch (Exception ex)
            {
                throw new Exception("写入数据时发生错误。", ex);
            }
        }
    }
}
