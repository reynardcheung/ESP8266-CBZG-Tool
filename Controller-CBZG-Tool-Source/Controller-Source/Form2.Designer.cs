namespace WirelessDownloadTool3._5
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OnlineDevice = new TextBox();
            DevicesComboBox = new ComboBox();
            button2 = new Button();
            textBox2 = new TextBox();
            VersionText = new TextBox();
            ModuleCountText = new TextBox();
            ModuleComboBox = new ComboBox();
            RefreshMSG = new Button();
            ModuleName = new TextBox();
            ModuleVersion = new TextBox();
            CMDInvoke = new TextBox();
            BriefText = new TextBox();
            InvokeFunction = new Button();
            DeviceDisconnect = new Button();
            DeviceStatus = new TextBox();
            SuspendLayout();
            // 
            // OnlineDevice
            // 
            OnlineDevice.BackColor = Color.White;
            OnlineDevice.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            OnlineDevice.Location = new Point(0, 391);
            OnlineDevice.Margin = new Padding(4, 3, 4, 3);
            OnlineDevice.Name = "OnlineDevice";
            OnlineDevice.ReadOnly = true;
            OnlineDevice.Size = new Size(367, 34);
            OnlineDevice.TabIndex = 0;
            OnlineDevice.Text = "当前在线设备：0";
            OnlineDevice.TextAlign = HorizontalAlignment.Center;
            // 
            // DevicesComboBox
            // 
            DevicesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DevicesComboBox.FormattingEnabled = true;
            DevicesComboBox.Location = new Point(2, 3);
            DevicesComboBox.Margin = new Padding(4, 3, 4, 3);
            DevicesComboBox.Name = "DevicesComboBox";
            DevicesComboBox.Size = new Size(364, 36);
            DevicesComboBox.TabIndex = 1;
            DevicesComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(118, 125);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(8, 8);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            textBox2.Location = new Point(2, 44);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(364, 34);
            textBox2.TabIndex = 4;
            textBox2.Text = "设备信息：";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // VersionText
            // 
            VersionText.BackColor = Color.White;
            VersionText.Location = new Point(2, 85);
            VersionText.Margin = new Padding(4, 3, 4, 3);
            VersionText.Name = "VersionText";
            VersionText.ReadOnly = true;
            VersionText.Size = new Size(175, 34);
            VersionText.TabIndex = 5;
            VersionText.Text = "版本：";
            // 
            // ModuleCountText
            // 
            ModuleCountText.BackColor = Color.White;
            ModuleCountText.Location = new Point(183, 85);
            ModuleCountText.Margin = new Padding(4, 3, 4, 3);
            ModuleCountText.Name = "ModuleCountText";
            ModuleCountText.ReadOnly = true;
            ModuleCountText.Size = new Size(184, 34);
            ModuleCountText.TabIndex = 6;
            ModuleCountText.Text = "模块数量：";
            // 
            // ModuleComboBox
            // 
            ModuleComboBox.BackColor = Color.White;
            ModuleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ModuleComboBox.FormattingEnabled = true;
            ModuleComboBox.Location = new Point(2, 125);
            ModuleComboBox.Margin = new Padding(4, 3, 4, 3);
            ModuleComboBox.Name = "ModuleComboBox";
            ModuleComboBox.Size = new Size(364, 36);
            ModuleComboBox.TabIndex = 7;
            ModuleComboBox.SelectedIndexChanged += ModuleComboBox_SelectedIndexChanged;
            // 
            // RefreshMSG
            // 
            RefreshMSG.Enabled = false;
            RefreshMSG.Location = new Point(304, 351);
            RefreshMSG.Margin = new Padding(4, 3, 4, 3);
            RefreshMSG.Name = "RefreshMSG";
            RefreshMSG.Size = new Size(63, 35);
            RefreshMSG.TabIndex = 8;
            RefreshMSG.Text = "刷新";
            RefreshMSG.UseVisualStyleBackColor = true;
            RefreshMSG.Click += Button1_Click;
            // 
            // ModuleName
            // 
            ModuleName.BackColor = Color.White;
            ModuleName.Location = new Point(2, 167);
            ModuleName.Margin = new Padding(4, 3, 4, 3);
            ModuleName.Name = "ModuleName";
            ModuleName.ReadOnly = true;
            ModuleName.Size = new Size(232, 34);
            ModuleName.TabIndex = 9;
            ModuleName.Text = "名称：";
            // 
            // ModuleVersion
            // 
            ModuleVersion.BackColor = Color.White;
            ModuleVersion.Location = new Point(239, 167);
            ModuleVersion.Margin = new Padding(4, 3, 4, 3);
            ModuleVersion.Name = "ModuleVersion";
            ModuleVersion.ReadOnly = true;
            ModuleVersion.Size = new Size(128, 34);
            ModuleVersion.TabIndex = 10;
            ModuleVersion.Text = "版本：";
            // 
            // CMDInvoke
            // 
            CMDInvoke.BackColor = Color.White;
            CMDInvoke.Location = new Point(2, 207);
            CMDInvoke.Margin = new Padding(4, 3, 4, 3);
            CMDInvoke.Name = "CMDInvoke";
            CMDInvoke.ReadOnly = true;
            CMDInvoke.Size = new Size(364, 34);
            CMDInvoke.TabIndex = 11;
            CMDInvoke.Text = "调用方法：";
            // 
            // BriefText
            // 
            BriefText.AcceptsTab = true;
            BriefText.BackColor = Color.White;
            BriefText.Location = new Point(2, 247);
            BriefText.Margin = new Padding(4, 3, 4, 3);
            BriefText.Multiline = true;
            BriefText.Name = "BriefText";
            BriefText.ReadOnly = true;
            BriefText.Size = new Size(364, 97);
            BriefText.TabIndex = 12;
            BriefText.Text = "简介：";
            // 
            // InvokeFunction
            // 
            InvokeFunction.Enabled = false;
            InvokeFunction.Location = new Point(2, 351);
            InvokeFunction.Margin = new Padding(4, 3, 4, 3);
            InvokeFunction.Name = "InvokeFunction";
            InvokeFunction.Size = new Size(64, 35);
            InvokeFunction.TabIndex = 13;
            InvokeFunction.Text = "调用";
            InvokeFunction.UseVisualStyleBackColor = true;
            InvokeFunction.Click += InvokeFunction_Click;
            // 
            // DeviceDisconnect
            // 
            DeviceDisconnect.Enabled = false;
            DeviceDisconnect.Location = new Point(68, 351);
            DeviceDisconnect.Margin = new Padding(4, 3, 4, 3);
            DeviceDisconnect.Name = "DeviceDisconnect";
            DeviceDisconnect.Size = new Size(62, 35);
            DeviceDisconnect.TabIndex = 14;
            DeviceDisconnect.Text = "删除";
            DeviceDisconnect.UseVisualStyleBackColor = true;
            DeviceDisconnect.Click += DeviceDisconnect_Click;
            // 
            // DeviceStatus
            // 
            DeviceStatus.BackColor = Color.White;
            DeviceStatus.BorderStyle = BorderStyle.None;
            DeviceStatus.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            DeviceStatus.Location = new Point(132, 355);
            DeviceStatus.Margin = new Padding(4, 3, 4, 3);
            DeviceStatus.Name = "DeviceStatus";
            DeviceStatus.ReadOnly = true;
            DeviceStatus.Size = new Size(165, 27);
            DeviceStatus.TabIndex = 15;
            DeviceStatus.TextAlign = HorizontalAlignment.Center;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(369, 428);
            Controls.Add(DeviceStatus);
            Controls.Add(DeviceDisconnect);
            Controls.Add(InvokeFunction);
            Controls.Add(BriefText);
            Controls.Add(CMDInvoke);
            Controls.Add(ModuleVersion);
            Controls.Add(ModuleName);
            Controls.Add(RefreshMSG);
            Controls.Add(ModuleComboBox);
            Controls.Add(ModuleCountText);
            Controls.Add(VersionText);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(DevicesComboBox);
            Controls.Add(OnlineDevice);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CBZG";
            FormClosed += Form2_FormClosed;
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox OnlineDevice;
        private ComboBox DevicesComboBox;
        private Button button2;
        private TextBox textBox2;
        private TextBox VersionText;
        private TextBox ModuleCountText;
        private ComboBox ModuleComboBox;
        private Button RefreshMSG;
        private TextBox ModuleName;
        private TextBox ModuleVersion;
        private TextBox CMDInvoke;
        private TextBox BriefText;
        private Button InvokeFunction;
        private Button DeviceDisconnect;
        private TextBox DeviceStatus;
    }
}