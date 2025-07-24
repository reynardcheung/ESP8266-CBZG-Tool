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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WirelessDownloadTool3._5
{
    public partial class Form2 : Form
    {
        readonly Server ListernServer;
        public Form2(Server _ListenServer)
        {
            InitializeComponent();
            ListernServer = _ListenServer;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListernServer.Stop();
            ESPDevice.Group.ESPDeviceGroup.Clear();
        }

        public void AddItemsInCombobox(DeviceGroup devices, ESPDevice device)
        {
            if (this == null)
            {
                Environment.Exit(0);
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(() =>
                {
                    if (devices == null) { return; }
                    devices.ESPDeviceGroup.Add(device);
                    this.DevicesComboBox.DataSource = devices.ESPDeviceGroup;
                    this.DevicesComboBox.DisplayMember = "DeviceID";
                    this.DevicesComboBox.ValueMember = "DeviceStream";
                    this.OnlineDevice.Text = $"当前在线设备：{devices.ESPDeviceGroup.Count}";
                }));
            }
            else
            {

            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectUIChange(DevicesComboBox.SelectedItem);
            if (this.DevicesComboBox.SelectedItem != null)
            {
                this.DeviceDisconnect.Enabled = true;
                this.VersionText.Text = "版本：" + ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].DeviceVersion;
                if (ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].CommandList != null)
                {
                    // 将解析后的列表绑定到 ComboBox  
                    ModuleComboBox.DataSource = ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].CommandList;
                    ModuleComboBox.DisplayMember = "Name";
                    ModuleComboBox.ValueMember = "CommandName";
                }
                else
                {
                    ModuleComboBox.DataSource = null;
                    ModuleComboBox.Items.Clear();
                }
            }
            else
            {
                ModuleComboBox.DataSource = null;
                ModuleComboBox.Items.Clear();
            }
        }


        private void SelectUIChange(object? sender)
        {
            if (sender is not ESPDevice device)
            {
                ModuleComboBox.SelectedIndex = -1;
                this.RefreshMSG.Enabled = false;
                this.InvokeFunction.Enabled = false;
                this.DeviceStatus.Text = "未选择设备";
                this.DeviceStatus.BackColor = Color.Yellow;
            }
            else if (device.DeviceConnectAlive == false)
            {
                ModuleComboBox.SelectedIndex = -1;
                this.RefreshMSG.Enabled = false;
                this.InvokeFunction.Enabled = false;
                this.DeviceStatus.Text = "设备断开连接";
                this.DeviceStatus.BackColor = Color.Red;
            }
            else
            {
                ModuleComboBox.SelectedIndex = -1;
                this.RefreshMSG.Enabled = true;
                this.DeviceStatus.Text = "设备响应正常";
                this.DeviceStatus.BackColor = Color.GreenYellow;
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            if (this.DevicesComboBox.SelectedItem == null)
            {
                return;
            }
            if (this.DevicesComboBox.SelectedItem is not ESPDevice device)
            {
                return;
            }

            if (device.DeviceStream is not NetworkStream stream)
            {
                MessageBox.Show(Form1.ActiveForm, "未选中对象", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DevicesComboBox.Enabled = false;

            var helper = new NetworkHelper(stream);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("{ESPGETMODULE}");
            byte[] jsonBytes = new byte[4096];
            int jsonNumber = 0;
            byte[] jsonBuffers = new byte[4096];
            TimeSpan timeout = TimeSpan.FromSeconds(5);

            try
            {
                await helper.WriteWithTimeoutAsync(buffer, 0, buffer.Length, timeout);
                while (true)
                {
                    int bytesRead = await helper.ReadWithTimeoutAsync(jsonBuffers, 0, jsonBuffers.Length, timeout);
                    if (bytesRead > 0 && (jsonBuffers[bytesRead - 1] == ']' || jsonBuffers[bytesRead - 1] == '}'))
                    {
                        Array.Copy(jsonBuffers, 0, jsonBytes, jsonNumber, bytesRead);
                        jsonNumber += bytesRead;
                        break;
                    }
                    else if (bytesRead > 0 && jsonNumber + bytesRead < 4096)
                    {

                    }
                    else
                    {
                        MessageBox.Show(Form1.ActiveForm, "读取模组异常,请重新尝试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    Array.Copy(jsonBuffers, 0, jsonBytes, jsonNumber, bytesRead);
                    jsonNumber += bytesRead;
                }
                if (jsonNumber > 0)
                {
                    string jsonString = Encoding.UTF8.GetString(jsonBytes);

                    if (jsonString == null)
                    {
                        return;
                    }

                    jsonString = jsonString.Replace("\0", "");

                    device.CommandList = JsonSerializer.Deserialize<List<Command>>(jsonString);

                    if (device.CommandList != null)
                    {
                        device.CommandList?.RemoveAll(cmd => cmd?.Name == "NULL");
                        ModuleComboBox.DataSource = device.CommandList;

                        // 绑定过滤后的列表
                        ModuleComboBox.DataSource = device.CommandList;
                        ModuleComboBox.DisplayMember = "Name";
                        ModuleComboBox.ValueMember = "CommandName";
                    }
                }
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(Form1.ActiveForm, "设备网络超时，即将自动删除", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UIDeleteSelectDevice();
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form1.ActiveForm, "设备网络异常，即将自动删除", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UIDeleteSelectDevice();
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            finally
            {
                DevicesComboBox.Enabled = true;
            }
            //device.DeviceConnect = false;
        }

        private void ModuleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModuleComboBox.SelectedIndex != -1)
            {
                this.InvokeFunction.Enabled = true;
                this.DeviceDisconnect.Enabled = true;

                var selectedIndex = this.DevicesComboBox.SelectedIndex;

                if (ESPDevice.Group != null &&
                    ESPDevice.Group.ESPDeviceGroup != null &&
                    selectedIndex >= 0 &&
                    selectedIndex < ESPDevice.Group.ESPDeviceGroup.Count)
                {
                    var deviceGroup = ESPDevice.Group.ESPDeviceGroup[selectedIndex];

                    if (deviceGroup.CommandList != null)
                    {
                        var moduleCount = deviceGroup.CommandList.Count;
                        this.ModuleCountText.Text = "模块数量：" + moduleCount.ToString();

                        if (ModuleComboBox != null &&
                            ModuleComboBox.SelectedIndex >= 0 &&
                            ModuleComboBox.SelectedIndex < moduleCount)
                        {
                            var command = deviceGroup.CommandList[ModuleComboBox.SelectedIndex];

                            // 设置文本属性  
                            this.ModuleName.Text = "名称：" + command.Name;
                            this.CMDInvoke.Text = "调用方法：" + command.CommandName;
                            this.ModuleVersion.Text = "版本：" + command.ModuleVersion;
                            this.BriefText.Text = "简介：" + command.Brief;
                        }
                        else
                        {
                            this.ModuleName.Text = "名称：未选择模块";
                            this.CMDInvoke.Text = "调用方法：无效选择";
                        }
                    }

                }
            }
            else
            {
                this.ModuleName.Text = "名称：";
                this.VersionText.Text = "版本：";
                this.CMDInvoke.Text = "调用方法：";
                this.ModuleCountText.Text = "模块数量：";
                this.BriefText.Text = "简介：";
                this.InvokeFunction.Enabled = false;
                //this.DeviceDisconnect.Enabled = false;
            }
        }

        private void InvokeFunction_Click(object sender, EventArgs e)
        {
            string dllPath;
            var selectedIndex = this.DevicesComboBox.SelectedIndex;
            if (ESPDevice.Group != null &&
                    ESPDevice.Group.ESPDeviceGroup != null &&
                    selectedIndex >= 0 &&
                    selectedIndex < ESPDevice.Group.ESPDeviceGroup.Count)
            {
                var deviceGroup = ESPDevice.Group.ESPDeviceGroup[selectedIndex];

                // 检查 CommandList 是否为 null  
                if (deviceGroup.CommandList != null)
                {
                    var moduleCount = deviceGroup.CommandList.Count;
                    this.ModuleCountText.Text = "模块数量：" + moduleCount.ToString();

                    if (ModuleComboBox != null &&
                        ModuleComboBox.SelectedIndex >= 0 &&
                        ModuleComboBox.SelectedIndex < moduleCount)
                    {
                        var command = deviceGroup.CommandList[ModuleComboBox.SelectedIndex];

                        if (command.Name == "NULL")
                        {
                            MessageBox.Show($"不能调用名称为NULL的模块", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dllPath = $"{System.Environment.CurrentDirectory}\\Modules\\{command.Name}\\{command.Name}.dll";
                        if (!File.Exists(dllPath))
                        {
                            MessageBox.Show($"找不到DLL文件：{dllPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"发生错误: 空引用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"发生错误: 空引用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show($"发生错误: 空引用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileVersionInfo FileInfo = FileVersionInfo.GetVersionInfo(dllPath);
            ESPDevice Device = ESPDevice.Group.ESPDeviceGroup[selectedIndex];
            if (FileInfo.FileVersion != null && Device.DeviceVersion != null)
            {
                string Version = Device.DeviceVersion.Replace("\0", "");

                if (FileInfo.FileVersion != Version)
                {
                    var Result = MessageBox.Show($"警告：模块需求版本与插件版本不同，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Result == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        return;
                    }
                }
            }

            this.Hide();
            InvokeModule(dllPath, ESPDevice.Group.ESPDeviceGroup[selectedIndex]);
            this.Show();
        }

        private void DeviceDisconnect_Click(object sender, EventArgs e)
        {
            UIDeleteSelectDevice();
        }

        private void UIDeleteSelectDevice()
        {
            if (this.DevicesComboBox.SelectedIndex != -1 && ESPDevice.Group.ESPDeviceGroup.Count > 0)
            {
                ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].DeviceConnectAlive = false;
                ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].DeviceStream.Close();
                ESPDevice.Group.ESPDeviceGroup[this.DevicesComboBox.SelectedIndex].DeviceStream.Dispose();
                ESPDevice.Group.ESPDeviceGroup.RemoveAt(this.DevicesComboBox.SelectedIndex);
                this.OnlineDevice.Text = $"当前在线设备：{ESPDevice.Group.ESPDeviceGroup.Count}";

            }
            else
            {
                SelectUIChange(null);
            }
        }

        private void InvokeModule(string dllPath, ESPDevice device)
        {
            try
            {
                string EntryClass = "ModNamespace.ModProgram";
                string EntryMethod = "ModMain";

                object[] parameters = [device.DeviceStream, this];
                PluginManage Module = new(dllPath, EntryClass, EntryMethod, parameters);
                Module.LoadAndRunPlugin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"发生错误: {ex.Message}");
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
    public class Command
    {
        public string Name { get; set; } = "Unknown";
        public string Brief { get; set; } = "No Description";
        public string ModuleVersion { get; set; } = "Unknown";
        public string CommandName { get; set; } = "{}";
    }

    public class UnloadablePluginContext(string pluginPath) : AssemblyLoadContext(isCollectible: true)
    {
        private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            return assemblyPath != null
                ? LoadFromAssemblyPath(assemblyPath)
                : null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return libraryPath != null
                ? LoadUnmanagedDllFromPath(libraryPath)
                : base.LoadUnmanagedDll(unmanagedDllName);
        }
    }

    public class PluginManage(string Path, string Entry, string Function, object[] Param)
    {
        private string DllPath { get; set; } = Path;
        private string EntryClass { get; set; } = Entry;
        private string MainMethod { get; set; } = Function;
        private object[] MethodParam { get; set; } = Param;

        public void LoadAndRunPlugin()
        {
            var alc = new UnloadablePluginContext(DllPath);

            Assembly pluginAssembly = alc.LoadFromAssemblyPath(DllPath);

            Type? entryType = pluginAssembly.GetType(EntryClass);
            MethodInfo? runMethod = entryType?.GetMethod(MainMethod);

            if (runMethod != null)
            {
                object? instance = Activator.CreateInstance(entryType!);
                runMethod.Invoke(instance, MethodParam);
                UnloadPlugin();
            }
        }

        public void UnloadPlugin()
        {
            var alc = new UnloadablePluginContext(DllPath);
            alc.Unload();
            for (int i = 0; i < 3; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(100);
            }
        }
    }
}
