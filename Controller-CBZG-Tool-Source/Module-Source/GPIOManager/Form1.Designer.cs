namespace GPIOManager
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.GPIO0Group = new System.Windows.Forms.GroupBox();
            this.PWMToDevice_0 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PWMPhase_0 = new System.Windows.Forms.NumericUpDown();
            this.PWMDuty_0 = new System.Windows.Forms.NumericUpDown();
            this.PWMPeriod_0 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GPIOToDevice_0 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PinLevel_0 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PinRes_0 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PinMode_0 = new System.Windows.Forms.ComboBox();
            this.Auto_PWMToDevice = new System.Windows.Forms.CheckBox();
            this.GPIOManagerSW = new System.Windows.Forms.CheckBox();
            this.GPIO2Group = new System.Windows.Forms.GroupBox();
            this.PWMToDevice_2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PWMPhase_2 = new System.Windows.Forms.NumericUpDown();
            this.PWMDuty_2 = new System.Windows.Forms.NumericUpDown();
            this.PWMPeriod_2 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.GPIOToDevice_2 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.PinLevel_2 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.PinRes_2 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.PinMode_2 = new System.Windows.Forms.ComboBox();
            this.AutoUpdataTimer = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GPIO0Group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPhase_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMDuty_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPeriod_0)).BeginInit();
            this.GPIO2Group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPhase_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMDuty_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPeriod_2)).BeginInit();
            this.SuspendLayout();
            // 
            // GPIO0Group
            // 
            this.GPIO0Group.Controls.Add(this.PWMToDevice_0);
            this.GPIO0Group.Controls.Add(this.label9);
            this.GPIO0Group.Controls.Add(this.label8);
            this.GPIO0Group.Controls.Add(this.label7);
            this.GPIO0Group.Controls.Add(this.PWMPhase_0);
            this.GPIO0Group.Controls.Add(this.PWMDuty_0);
            this.GPIO0Group.Controls.Add(this.PWMPeriod_0);
            this.GPIO0Group.Controls.Add(this.label6);
            this.GPIO0Group.Controls.Add(this.label5);
            this.GPIO0Group.Controls.Add(this.label4);
            this.GPIO0Group.Controls.Add(this.GPIOToDevice_0);
            this.GPIO0Group.Controls.Add(this.label3);
            this.GPIO0Group.Controls.Add(this.PinLevel_0);
            this.GPIO0Group.Controls.Add(this.label2);
            this.GPIO0Group.Controls.Add(this.PinRes_0);
            this.GPIO0Group.Controls.Add(this.label1);
            this.GPIO0Group.Controls.Add(this.PinMode_0);
            this.GPIO0Group.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GPIO0Group.Location = new System.Drawing.Point(12, 54);
            this.GPIO0Group.Name = "GPIO0Group";
            this.GPIO0Group.Size = new System.Drawing.Size(450, 346);
            this.GPIO0Group.TabIndex = 0;
            this.GPIO0Group.TabStop = false;
            this.GPIO0Group.Text = "GPIO 0";
            // 
            // PWMToDevice_0
            // 
            this.PWMToDevice_0.Location = new System.Drawing.Point(37, 298);
            this.PWMToDevice_0.Name = "PWMToDevice_0";
            this.PWMToDevice_0.Size = new System.Drawing.Size(382, 35);
            this.PWMToDevice_0.TabIndex = 19;
            this.PWMToDevice_0.Text = "上传至设备";
            this.PWMToDevice_0.UseVisualStyleBackColor = true;
            this.PWMToDevice_0.Click += new System.EventHandler(this.PWMModeUpdata_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(367, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "角度";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(367, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 21);
            this.label8.TabIndex = 17;
            this.label8.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(367, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 21);
            this.label7.TabIndex = 16;
            this.label7.Text = "微秒";
            // 
            // PWMPhase_0
            // 
            this.PWMPhase_0.DecimalPlaces = 1;
            this.PWMPhase_0.Location = new System.Drawing.Point(171, 261);
            this.PWMPhase_0.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.PWMPhase_0.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.PWMPhase_0.Name = "PWMPhase_0";
            this.PWMPhase_0.Size = new System.Drawing.Size(173, 31);
            this.PWMPhase_0.TabIndex = 15;
            // 
            // PWMDuty_0
            // 
            this.PWMDuty_0.Location = new System.Drawing.Point(171, 225);
            this.PWMDuty_0.Name = "PWMDuty_0";
            this.PWMDuty_0.Size = new System.Drawing.Size(173, 31);
            this.PWMDuty_0.TabIndex = 14;
            this.PWMDuty_0.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // PWMPeriod_0
            // 
            this.PWMPeriod_0.Location = new System.Drawing.Point(171, 188);
            this.PWMPeriod_0.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.PWMPeriod_0.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PWMPeriod_0.Name = "PWMPeriod_0";
            this.PWMPeriod_0.Size = new System.Drawing.Size(173, 31);
            this.PWMPeriod_0.TabIndex = 13;
            this.PWMPeriod_0.ThousandsSeparator = true;
            this.PWMPeriod_0.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "相位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 9;
            this.label5.Text = "占空比";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "周期";
            // 
            // GPIOToDevice_0
            // 
            this.GPIOToDevice_0.Location = new System.Drawing.Point(37, 147);
            this.GPIOToDevice_0.Name = "GPIOToDevice_0";
            this.GPIOToDevice_0.Size = new System.Drawing.Size(382, 35);
            this.GPIOToDevice_0.TabIndex = 7;
            this.GPIOToDevice_0.Text = "上传至设备";
            this.GPIOToDevice_0.UseVisualStyleBackColor = true;
            this.GPIOToDevice_0.Click += new System.EventHandler(this.GPIOModeUpdata_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "电平";
            // 
            // PinLevel_0
            // 
            this.PinLevel_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinLevel_0.FormattingEnabled = true;
            this.PinLevel_0.Items.AddRange(new object[] {
            "高",
            "低"});
            this.PinLevel_0.Location = new System.Drawing.Point(171, 112);
            this.PinLevel_0.Name = "PinLevel_0";
            this.PinLevel_0.Size = new System.Drawing.Size(173, 29);
            this.PinLevel_0.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "上拉/下拉";
            // 
            // PinRes_0
            // 
            this.PinRes_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinRes_0.FormattingEnabled = true;
            this.PinRes_0.Items.AddRange(new object[] {
            "上拉",
            "下拉",
            "浮空"});
            this.PinRes_0.Location = new System.Drawing.Point(171, 77);
            this.PinRes_0.Name = "PinRes_0";
            this.PinRes_0.Size = new System.Drawing.Size(173, 29);
            this.PinRes_0.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "引脚模式";
            // 
            // PinMode_0
            // 
            this.PinMode_0.DisplayMember = "0";
            this.PinMode_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinMode_0.FormattingEnabled = true;
            this.PinMode_0.Items.AddRange(new object[] {
            "禁用",
            "输入模式",
            "输出模式",
            "开漏模式"});
            this.PinMode_0.Location = new System.Drawing.Point(171, 42);
            this.PinMode_0.Name = "PinMode_0";
            this.PinMode_0.Size = new System.Drawing.Size(173, 29);
            this.PinMode_0.TabIndex = 1;
            // 
            // Auto_PWMToDevice
            // 
            this.Auto_PWMToDevice.Appearance = System.Windows.Forms.Appearance.Button;
            this.Auto_PWMToDevice.AutoSize = true;
            this.Auto_PWMToDevice.BackColor = System.Drawing.Color.Red;
            this.Auto_PWMToDevice.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.Auto_PWMToDevice.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.Auto_PWMToDevice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.Auto_PWMToDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Auto_PWMToDevice.Location = new System.Drawing.Point(190, 12);
            this.Auto_PWMToDevice.Name = "Auto_PWMToDevice";
            this.Auto_PWMToDevice.Size = new System.Drawing.Size(179, 31);
            this.Auto_PWMToDevice.TabIndex = 3;
            this.Auto_PWMToDevice.Text = "PWM自动上传开关";
            this.Auto_PWMToDevice.UseVisualStyleBackColor = false;
            this.Auto_PWMToDevice.CheckedChanged += new System.EventHandler(this.Auto_PWMToDevice_CheckedChanged);
            // 
            // GPIOManagerSW
            // 
            this.GPIOManagerSW.Appearance = System.Windows.Forms.Appearance.Button;
            this.GPIOManagerSW.AutoSize = true;
            this.GPIOManagerSW.BackColor = System.Drawing.Color.Red;
            this.GPIOManagerSW.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.GPIOManagerSW.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.GPIOManagerSW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.GPIOManagerSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GPIOManagerSW.Location = new System.Drawing.Point(12, 12);
            this.GPIOManagerSW.Name = "GPIOManagerSW";
            this.GPIOManagerSW.Size = new System.Drawing.Size(169, 31);
            this.GPIOManagerSW.TabIndex = 2;
            this.GPIOManagerSW.Text = "GPIO管理器开关";
            this.GPIOManagerSW.UseVisualStyleBackColor = false;
            this.GPIOManagerSW.CheckedChanged += new System.EventHandler(this.GPIOManagerSW_CheckedChanged);
            // 
            // GPIO2Group
            // 
            this.GPIO2Group.Controls.Add(this.PWMToDevice_2);
            this.GPIO2Group.Controls.Add(this.label10);
            this.GPIO2Group.Controls.Add(this.label11);
            this.GPIO2Group.Controls.Add(this.label12);
            this.GPIO2Group.Controls.Add(this.PWMPhase_2);
            this.GPIO2Group.Controls.Add(this.PWMDuty_2);
            this.GPIO2Group.Controls.Add(this.PWMPeriod_2);
            this.GPIO2Group.Controls.Add(this.label13);
            this.GPIO2Group.Controls.Add(this.label14);
            this.GPIO2Group.Controls.Add(this.label15);
            this.GPIO2Group.Controls.Add(this.GPIOToDevice_2);
            this.GPIO2Group.Controls.Add(this.label16);
            this.GPIO2Group.Controls.Add(this.PinLevel_2);
            this.GPIO2Group.Controls.Add(this.label17);
            this.GPIO2Group.Controls.Add(this.PinRes_2);
            this.GPIO2Group.Controls.Add(this.label18);
            this.GPIO2Group.Controls.Add(this.PinMode_2);
            this.GPIO2Group.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GPIO2Group.Location = new System.Drawing.Point(468, 54);
            this.GPIO2Group.Name = "GPIO2Group";
            this.GPIO2Group.Size = new System.Drawing.Size(450, 346);
            this.GPIO2Group.TabIndex = 3;
            this.GPIO2Group.TabStop = false;
            this.GPIO2Group.Text = "GPIO 2";
            // 
            // PWMToDevice_2
            // 
            this.PWMToDevice_2.Location = new System.Drawing.Point(37, 298);
            this.PWMToDevice_2.Name = "PWMToDevice_2";
            this.PWMToDevice_2.Size = new System.Drawing.Size(382, 35);
            this.PWMToDevice_2.TabIndex = 19;
            this.PWMToDevice_2.Text = "上传至设备";
            this.PWMToDevice_2.UseVisualStyleBackColor = true;
            this.PWMToDevice_2.Click += new System.EventHandler(this.PWMModeUpdata_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(367, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 21);
            this.label10.TabIndex = 18;
            this.label10.Text = "角度";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(367, 227);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 21);
            this.label11.TabIndex = 17;
            this.label11.Text = "%";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 191);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 21);
            this.label12.TabIndex = 16;
            this.label12.Text = "微秒";
            // 
            // PWMPhase_2
            // 
            this.PWMPhase_2.DecimalPlaces = 1;
            this.PWMPhase_2.Location = new System.Drawing.Point(171, 261);
            this.PWMPhase_2.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.PWMPhase_2.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.PWMPhase_2.Name = "PWMPhase_2";
            this.PWMPhase_2.Size = new System.Drawing.Size(173, 31);
            this.PWMPhase_2.TabIndex = 15;
            // 
            // PWMDuty_2
            // 
            this.PWMDuty_2.Location = new System.Drawing.Point(171, 225);
            this.PWMDuty_2.Name = "PWMDuty_2";
            this.PWMDuty_2.Size = new System.Drawing.Size(173, 31);
            this.PWMDuty_2.TabIndex = 14;
            this.PWMDuty_2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // PWMPeriod_2
            // 
            this.PWMPeriod_2.Location = new System.Drawing.Point(171, 188);
            this.PWMPeriod_2.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.PWMPeriod_2.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PWMPeriod_2.Name = "PWMPeriod_2";
            this.PWMPeriod_2.Size = new System.Drawing.Size(173, 31);
            this.PWMPeriod_2.TabIndex = 13;
            this.PWMPeriod_2.ThousandsSeparator = true;
            this.PWMPeriod_2.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 261);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 21);
            this.label13.TabIndex = 11;
            this.label13.Text = "相位";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 226);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 21);
            this.label14.TabIndex = 9;
            this.label14.Text = "占空比";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 191);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 21);
            this.label15.TabIndex = 6;
            this.label15.Text = "周期";
            // 
            // GPIOToDevice_2
            // 
            this.GPIOToDevice_2.Location = new System.Drawing.Point(37, 147);
            this.GPIOToDevice_2.Name = "GPIOToDevice_2";
            this.GPIOToDevice_2.Size = new System.Drawing.Size(382, 35);
            this.GPIOToDevice_2.TabIndex = 7;
            this.GPIOToDevice_2.Text = "上传至设备";
            this.GPIOToDevice_2.UseVisualStyleBackColor = true;
            this.GPIOToDevice_2.Click += new System.EventHandler(this.GPIOModeUpdata_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(32, 115);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 21);
            this.label16.TabIndex = 6;
            this.label16.Text = "电平";
            // 
            // PinLevel_2
            // 
            this.PinLevel_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinLevel_2.FormattingEnabled = true;
            this.PinLevel_2.Items.AddRange(new object[] {
            "高",
            "低"});
            this.PinLevel_2.Location = new System.Drawing.Point(171, 112);
            this.PinLevel_2.Name = "PinLevel_2";
            this.PinLevel_2.Size = new System.Drawing.Size(173, 29);
            this.PinLevel_2.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 21);
            this.label17.TabIndex = 4;
            this.label17.Text = "上拉/下拉";
            // 
            // PinRes_2
            // 
            this.PinRes_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinRes_2.FormattingEnabled = true;
            this.PinRes_2.Items.AddRange(new object[] {
            "上拉",
            "下拉",
            "浮空"});
            this.PinRes_2.Location = new System.Drawing.Point(171, 77);
            this.PinRes_2.Name = "PinRes_2";
            this.PinRes_2.Size = new System.Drawing.Size(173, 29);
            this.PinRes_2.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(32, 45);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 21);
            this.label18.TabIndex = 2;
            this.label18.Text = "引脚模式";
            // 
            // PinMode_2
            // 
            this.PinMode_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinMode_2.FormattingEnabled = true;
            this.PinMode_2.Items.AddRange(new object[] {
            "禁用",
            "输入模式",
            "输出模式",
            "开漏模式"});
            this.PinMode_2.Location = new System.Drawing.Point(171, 42);
            this.PinMode_2.Name = "PinMode_2";
            this.PinMode_2.Size = new System.Drawing.Size(173, 29);
            this.PinMode_2.TabIndex = 1;
            // 
            // AutoUpdataTimer
            // 
            this.AutoUpdataTimer.Interval = 500;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.71429F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(373, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(543, 29);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "PWM功能因个人无法解决的相关问题，暂时禁用";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 411);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Auto_PWMToDevice);
            this.Controls.Add(this.GPIO2Group);
            this.Controls.Add(this.GPIOManagerSW);
            this.Controls.Add(this.GPIO0Group);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GPIO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.GPIO0Group.ResumeLayout(false);
            this.GPIO0Group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPhase_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMDuty_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPeriod_0)).EndInit();
            this.GPIO2Group.ResumeLayout(false);
            this.GPIO2Group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPhase_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMDuty_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PWMPeriod_2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GPIO0Group;
        private System.Windows.Forms.CheckBox GPIOManagerSW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PinMode_0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox PinLevel_0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown PWMPhase_0;
        private System.Windows.Forms.NumericUpDown PWMDuty_0;
        private System.Windows.Forms.NumericUpDown PWMPeriod_0;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button PWMToDevice_0;
        private System.Windows.Forms.Button GPIOToDevice_0;
        private System.Windows.Forms.CheckBox Auto_PWMToDevice;
        private System.Windows.Forms.GroupBox GPIO2Group;
        private System.Windows.Forms.Button PWMToDevice_2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown PWMPhase_2;
        private System.Windows.Forms.NumericUpDown PWMDuty_2;
        private System.Windows.Forms.NumericUpDown PWMPeriod_2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button GPIOToDevice_2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox PinLevel_2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox PinRes_2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox PinMode_2;
        private System.Windows.Forms.Timer AutoUpdataTimer;
        private System.Windows.Forms.ComboBox PinRes_0;
        private System.Windows.Forms.TextBox textBox1;
    }
}