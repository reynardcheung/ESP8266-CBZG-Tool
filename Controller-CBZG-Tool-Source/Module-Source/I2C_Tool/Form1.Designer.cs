namespace I2C_Tool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            I2C_Tool.ColorItem colorItem13 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem14 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem15 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem16 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem17 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem18 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem19 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem20 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem21 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem22 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem23 = new I2C_Tool.ColorItem();
            I2C_Tool.ColorItem colorItem24 = new I2C_Tool.ColorItem();
            this.DeviceScanButton = new System.Windows.Forms.Button();
            this.DeviceComboBox = new System.Windows.Forms.ComboBox();
            this.RegisterGridView = new System.Windows.Forms.DataGridView();
            this.设备地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.寄存器地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.寄存器数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.内容 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label = new System.Windows.Forms.Label();
            this.DialogDeviceAddrComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DialogRegAddrTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DialogWriteBytes = new System.Windows.Forms.TextBox();
            this.I2CWriteButton = new System.Windows.Forms.Button();
            this.DialogReadRegButton = new System.Windows.Forms.Button();
            this.ClearDialogMsgButton = new System.Windows.Forms.Button();
            this.AutoRegFlush = new System.Windows.Forms.CheckBox();
            this.FlushRegFrequent = new System.Windows.Forms.NumericUpDown();
            this.DialogReadBytesNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ListDeviceAddrComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ListRegAddr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ListBytesNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.AddRegToListButton = new System.Windows.Forms.Button();
            this.RegDeleteButton = new System.Windows.Forms.Button();
            this.RegEditButton = new System.Windows.Forms.Button();
            this.ReadListButton = new System.Windows.Forms.Button();
            this.IICToolSwitch = new System.Windows.Forms.CheckBox();
            this.I2CMsgTextBox = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.WaveDataHighBit = new System.Windows.Forms.ComboBox();
            this.AddWaveButton = new System.Windows.Forms.Button();
            this.WaveList = new System.Windows.Forms.ComboBox();
            this.DeleteWaveButton = new System.Windows.Forms.Button();
            this.WaveDataLowBit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.WaveTimeBase = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.DevComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.RegHighByteEnable = new System.Windows.Forms.CheckBox();
            this.RegLowByteEnable = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.WaveName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NumberPerPixel = new System.Windows.Forms.NumericUpDown();
            this.WavePictureBox1 = new I2C_Tool.WavePictureBox();
            this.ColorBox = new I2C_Tool.ColorComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlushRegFrequent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogReadBytesNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListBytesNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaveTimeBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberPerPixel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WavePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DeviceScanButton
            // 
            this.DeviceScanButton.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeviceScanButton.Location = new System.Drawing.Point(113, 11);
            this.DeviceScanButton.Name = "DeviceScanButton";
            this.DeviceScanButton.Size = new System.Drawing.Size(103, 35);
            this.DeviceScanButton.TabIndex = 0;
            this.DeviceScanButton.Text = "设备扫描";
            this.DeviceScanButton.UseVisualStyleBackColor = true;
            this.DeviceScanButton.Click += new System.EventHandler(this.DeviceScanButton_Click);
            // 
            // DeviceComboBox
            // 
            this.DeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceComboBox.FormattingEnabled = true;
            this.DeviceComboBox.Location = new System.Drawing.Point(222, 15);
            this.DeviceComboBox.Name = "DeviceComboBox";
            this.DeviceComboBox.Size = new System.Drawing.Size(162, 29);
            this.DeviceComboBox.TabIndex = 1;
            // 
            // RegisterGridView
            // 
            this.RegisterGridView.AllowUserToAddRows = false;
            this.RegisterGridView.AllowUserToDeleteRows = false;
            this.RegisterGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RegisterGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.RegisterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegisterGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.设备地址,
            this.寄存器地址,
            this.寄存器数量,
            this.内容});
            this.RegisterGridView.GridColor = System.Drawing.Color.Orange;
            this.RegisterGridView.Location = new System.Drawing.Point(12, 169);
            this.RegisterGridView.MultiSelect = false;
            this.RegisterGridView.Name = "RegisterGridView";
            this.RegisterGridView.ReadOnly = true;
            this.RegisterGridView.RowHeadersVisible = false;
            this.RegisterGridView.RowHeadersWidth = 72;
            this.RegisterGridView.RowTemplate.Height = 33;
            this.RegisterGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RegisterGridView.ShowEditingIcon = false;
            this.RegisterGridView.Size = new System.Drawing.Size(372, 420);
            this.RegisterGridView.TabIndex = 2;
            this.RegisterGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RegisterGridView_CellFormatting);
            // 
            // 设备地址
            // 
            this.设备地址.DataPropertyName = "DeviceAddress";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.设备地址.DefaultCellStyle = dataGridViewCellStyle6;
            this.设备地址.HeaderText = "设备地址";
            this.设备地址.MinimumWidth = 9;
            this.设备地址.Name = "设备地址";
            this.设备地址.ReadOnly = true;
            this.设备地址.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.设备地址.Width = 125;
            // 
            // 寄存器地址
            // 
            this.寄存器地址.DataPropertyName = "RegisterAddress";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.寄存器地址.DefaultCellStyle = dataGridViewCellStyle7;
            this.寄存器地址.HeaderText = "REG地址";
            this.寄存器地址.MinimumWidth = 9;
            this.寄存器地址.Name = "寄存器地址";
            this.寄存器地址.ReadOnly = true;
            this.寄存器地址.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄存器地址.Width = 125;
            // 
            // 寄存器数量
            // 
            this.寄存器数量.DataPropertyName = "RegisterNumber";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.寄存器数量.DefaultCellStyle = dataGridViewCellStyle8;
            this.寄存器数量.HeaderText = "字节数量";
            this.寄存器数量.MinimumWidth = 9;
            this.寄存器数量.Name = "寄存器数量";
            this.寄存器数量.ReadOnly = true;
            this.寄存器数量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄存器数量.Width = 125;
            // 
            // 内容
            // 
            this.内容.DataPropertyName = "RegisterValue";
            this.内容.HeaderText = "内容";
            this.内容.MinimumWidth = 9;
            this.内容.Name = "内容";
            this.内容.ReadOnly = true;
            this.内容.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.内容.Width = 175;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label.Location = new System.Drawing.Point(920, 16);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(80, 18);
            this.Label.TabIndex = 4;
            this.Label.Text = "设备地址";
            // 
            // DialogDeviceAddrComboBox
            // 
            this.DialogDeviceAddrComboBox.FormattingEnabled = true;
            this.DialogDeviceAddrComboBox.Location = new System.Drawing.Point(1020, 13);
            this.DialogDeviceAddrComboBox.MaxLength = 4;
            this.DialogDeviceAddrComboBox.Name = "DialogDeviceAddrComboBox";
            this.DialogDeviceAddrComboBox.Size = new System.Drawing.Size(103, 29);
            this.DialogDeviceAddrComboBox.TabIndex = 5;
            this.DialogDeviceAddrComboBox.Text = "0x3C";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1129, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "寄存器地址";
            // 
            // DialogRegAddrTextBox
            // 
            this.DialogRegAddrTextBox.Location = new System.Drawing.Point(1250, 14);
            this.DialogRegAddrTextBox.MaxLength = 4;
            this.DialogRegAddrTextBox.Name = "DialogRegAddrTextBox";
            this.DialogRegAddrTextBox.Size = new System.Drawing.Size(133, 31);
            this.DialogRegAddrTextBox.TabIndex = 7;
            this.DialogRegAddrTextBox.Text = "0x00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(1129, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "读取字节数";
            // 
            // DialogWriteBytes
            // 
            this.DialogWriteBytes.Location = new System.Drawing.Point(924, 85);
            this.DialogWriteBytes.Multiline = true;
            this.DialogWriteBytes.Name = "DialogWriteBytes";
            this.DialogWriteBytes.Size = new System.Drawing.Size(582, 78);
            this.DialogWriteBytes.TabIndex = 11;
            // 
            // I2CWriteButton
            // 
            this.I2CWriteButton.Location = new System.Drawing.Point(1430, 14);
            this.I2CWriteButton.Name = "I2CWriteButton";
            this.I2CWriteButton.Size = new System.Drawing.Size(35, 66);
            this.I2CWriteButton.TabIndex = 12;
            this.I2CWriteButton.Text = "写入";
            this.I2CWriteButton.UseVisualStyleBackColor = true;
            this.I2CWriteButton.Click += new System.EventHandler(this.I2CWriteButton_Click);
            // 
            // DialogReadRegButton
            // 
            this.DialogReadRegButton.Location = new System.Drawing.Point(1389, 14);
            this.DialogReadRegButton.Name = "DialogReadRegButton";
            this.DialogReadRegButton.Size = new System.Drawing.Size(35, 66);
            this.DialogReadRegButton.TabIndex = 13;
            this.DialogReadRegButton.Text = "读取";
            this.DialogReadRegButton.UseVisualStyleBackColor = true;
            this.DialogReadRegButton.Click += new System.EventHandler(this.DialogReadRegButton_Click);
            // 
            // ClearDialogMsgButton
            // 
            this.ClearDialogMsgButton.Location = new System.Drawing.Point(1471, 14);
            this.ClearDialogMsgButton.Name = "ClearDialogMsgButton";
            this.ClearDialogMsgButton.Size = new System.Drawing.Size(35, 66);
            this.ClearDialogMsgButton.TabIndex = 14;
            this.ClearDialogMsgButton.Text = "清空";
            this.ClearDialogMsgButton.UseVisualStyleBackColor = true;
            this.ClearDialogMsgButton.Click += new System.EventHandler(this.ClearDialogMsgButton_Click);
            // 
            // AutoRegFlush
            // 
            this.AutoRegFlush.AutoSize = true;
            this.AutoRegFlush.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AutoRegFlush.Location = new System.Drawing.Point(222, 96);
            this.AutoRegFlush.Name = "AutoRegFlush";
            this.AutoRegFlush.Size = new System.Drawing.Size(70, 22);
            this.AutoRegFlush.TabIndex = 22;
            this.AutoRegFlush.Text = "刷新";
            this.AutoRegFlush.UseVisualStyleBackColor = true;
            this.AutoRegFlush.CheckedChanged += new System.EventHandler(this.AutoRegFlush_CheckedChanged);
            // 
            // FlushRegFrequent
            // 
            this.FlushRegFrequent.Location = new System.Drawing.Point(298, 91);
            this.FlushRegFrequent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.FlushRegFrequent.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.FlushRegFrequent.Name = "FlushRegFrequent";
            this.FlushRegFrequent.Size = new System.Drawing.Size(86, 31);
            this.FlushRegFrequent.TabIndex = 23;
            this.FlushRegFrequent.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // DialogReadBytesNumber
            // 
            this.DialogReadBytesNumber.Location = new System.Drawing.Point(1250, 48);
            this.DialogReadBytesNumber.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.DialogReadBytesNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DialogReadBytesNumber.Name = "DialogReadBytesNumber";
            this.DialogReadBytesNumber.Size = new System.Drawing.Size(133, 31);
            this.DialogReadBytesNumber.TabIndex = 24;
            this.DialogReadBytesNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(920, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 25;
            this.label2.Text = "写入字节：";
            // 
            // ListDeviceAddrComboBox
            // 
            this.ListDeviceAddrComboBox.FormattingEnabled = true;
            this.ListDeviceAddrComboBox.Location = new System.Drawing.Point(109, 51);
            this.ListDeviceAddrComboBox.MaxLength = 4;
            this.ListDeviceAddrComboBox.Name = "ListDeviceAddrComboBox";
            this.ListDeviceAddrComboBox.Size = new System.Drawing.Size(103, 29);
            this.ListDeviceAddrComboBox.TabIndex = 27;
            this.ListDeviceAddrComboBox.Text = "0x3C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(13, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "设备地址";
            // 
            // ListRegAddr
            // 
            this.ListRegAddr.Location = new System.Drawing.Point(131, 90);
            this.ListRegAddr.MaxLength = 4;
            this.ListRegAddr.Name = "ListRegAddr";
            this.ListRegAddr.Size = new System.Drawing.Size(81, 31);
            this.ListRegAddr.TabIndex = 29;
            this.ListRegAddr.Text = "0x00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(13, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 28;
            this.label5.Text = "寄存器地址";
            // 
            // ListBytesNumber
            // 
            this.ListBytesNumber.Location = new System.Drawing.Point(318, 54);
            this.ListBytesNumber.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.ListBytesNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ListBytesNumber.Name = "ListBytesNumber";
            this.ListBytesNumber.Size = new System.Drawing.Size(66, 31);
            this.ListBytesNumber.TabIndex = 31;
            this.ListBytesNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(219, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 30;
            this.label6.Text = "读字节数";
            // 
            // AddRegToListButton
            // 
            this.AddRegToListButton.Location = new System.Drawing.Point(105, 127);
            this.AddRegToListButton.Name = "AddRegToListButton";
            this.AddRegToListButton.Size = new System.Drawing.Size(65, 35);
            this.AddRegToListButton.TabIndex = 32;
            this.AddRegToListButton.Text = "添加";
            this.AddRegToListButton.UseVisualStyleBackColor = true;
            this.AddRegToListButton.Click += new System.EventHandler(this.AddRegToListButton_Click);
            // 
            // RegDeleteButton
            // 
            this.RegDeleteButton.Location = new System.Drawing.Point(176, 128);
            this.RegDeleteButton.Name = "RegDeleteButton";
            this.RegDeleteButton.Size = new System.Drawing.Size(65, 35);
            this.RegDeleteButton.TabIndex = 33;
            this.RegDeleteButton.Text = "删除";
            this.RegDeleteButton.UseVisualStyleBackColor = true;
            this.RegDeleteButton.Click += new System.EventHandler(this.RegDeleteButton_Click);
            // 
            // RegEditButton
            // 
            this.RegEditButton.Location = new System.Drawing.Point(319, 128);
            this.RegEditButton.Name = "RegEditButton";
            this.RegEditButton.Size = new System.Drawing.Size(65, 35);
            this.RegEditButton.TabIndex = 34;
            this.RegEditButton.Text = "编辑";
            this.RegEditButton.UseVisualStyleBackColor = true;
            this.RegEditButton.Click += new System.EventHandler(this.RegEditButton_Click);
            // 
            // ReadListButton
            // 
            this.ReadListButton.Location = new System.Drawing.Point(247, 128);
            this.ReadListButton.Name = "ReadListButton";
            this.ReadListButton.Size = new System.Drawing.Size(65, 35);
            this.ReadListButton.TabIndex = 35;
            this.ReadListButton.Text = "读取";
            this.ReadListButton.UseVisualStyleBackColor = true;
            this.ReadListButton.Click += new System.EventHandler(this.ReadListButton_Click);
            // 
            // IICToolSwitch
            // 
            this.IICToolSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.IICToolSwitch.AutoSize = true;
            this.IICToolSwitch.BackColor = System.Drawing.Color.Red;
            this.IICToolSwitch.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.IICToolSwitch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.IICToolSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IICToolSwitch.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IICToolSwitch.Location = new System.Drawing.Point(16, 12);
            this.IICToolSwitch.Name = "IICToolSwitch";
            this.IICToolSwitch.Size = new System.Drawing.Size(86, 31);
            this.IICToolSwitch.TabIndex = 36;
            this.IICToolSwitch.Text = "总开关";
            this.IICToolSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.IICToolSwitch.UseVisualStyleBackColor = false;
            this.IICToolSwitch.CheckedChanged += new System.EventHandler(this.IICToolSwitch_CheckedChanged);
            // 
            // I2CMsgTextBox
            // 
            this.I2CMsgTextBox.Location = new System.Drawing.Point(924, 169);
            this.I2CMsgTextBox.Name = "I2CMsgTextBox";
            this.I2CMsgTextBox.ReadOnly = true;
            this.I2CMsgTextBox.Size = new System.Drawing.Size(582, 420);
            this.I2CMsgTextBox.TabIndex = 37;
            this.I2CMsgTextBox.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(401, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 18);
            this.label9.TabIndex = 45;
            this.label9.Text = "REG数据高位";
            // 
            // WaveDataHighBit
            // 
            this.WaveDataHighBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WaveDataHighBit.FormattingEnabled = true;
            this.WaveDataHighBit.Location = new System.Drawing.Point(525, 96);
            this.WaveDataHighBit.MaxLength = 4;
            this.WaveDataHighBit.Name = "WaveDataHighBit";
            this.WaveDataHighBit.Size = new System.Drawing.Size(106, 29);
            this.WaveDataHighBit.TabIndex = 46;
            // 
            // AddWaveButton
            // 
            this.AddWaveButton.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddWaveButton.Location = new System.Drawing.Point(665, 98);
            this.AddWaveButton.Name = "AddWaveButton";
            this.AddWaveButton.Size = new System.Drawing.Size(69, 63);
            this.AddWaveButton.TabIndex = 47;
            this.AddWaveButton.Text = "添加\r\n波形\r\n";
            this.AddWaveButton.UseVisualStyleBackColor = true;
            this.AddWaveButton.Click += new System.EventHandler(this.AddWaveButton_Click);
            // 
            // WaveList
            // 
            this.WaveList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WaveList.FormattingEnabled = true;
            this.WaveList.Location = new System.Drawing.Point(799, 13);
            this.WaveList.Name = "WaveList";
            this.WaveList.Size = new System.Drawing.Size(106, 29);
            this.WaveList.TabIndex = 48;
            // 
            // DeleteWaveButton
            // 
            this.DeleteWaveButton.Location = new System.Drawing.Point(754, 51);
            this.DeleteWaveButton.Name = "DeleteWaveButton";
            this.DeleteWaveButton.Size = new System.Drawing.Size(151, 34);
            this.DeleteWaveButton.TabIndex = 49;
            this.DeleteWaveButton.Text = "删除波形";
            this.DeleteWaveButton.UseVisualStyleBackColor = true;
            this.DeleteWaveButton.Click += new System.EventHandler(this.DeleteWaveButton_Click);
            // 
            // WaveDataLowBit
            // 
            this.WaveDataLowBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WaveDataLowBit.FormattingEnabled = true;
            this.WaveDataLowBit.Location = new System.Drawing.Point(525, 131);
            this.WaveDataLowBit.MaxLength = 4;
            this.WaveDataLowBit.Name = "WaveDataLowBit";
            this.WaveDataLowBit.Size = new System.Drawing.Size(106, 29);
            this.WaveDataLowBit.TabIndex = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(401, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 18);
            this.label7.TabIndex = 51;
            this.label7.Text = "REG数据低位";
            // 
            // WaveTimeBase
            // 
            this.WaveTimeBase.Location = new System.Drawing.Point(819, 131);
            this.WaveTimeBase.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.WaveTimeBase.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.WaveTimeBase.Name = "WaveTimeBase";
            this.WaveTimeBase.Size = new System.Drawing.Size(86, 31);
            this.WaveTimeBase.TabIndex = 52;
            this.WaveTimeBase.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WaveTimeBase.ValueChanged += new System.EventHandler(this.WaveTimeBase_ValueChanged);
            this.WaveTimeBase.Click += new System.EventHandler(this.WaveTimeBase_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(750, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 18);
            this.label10.TabIndex = 53;
            this.label10.Text = "时基";
            // 
            // DevComboBox
            // 
            this.DevComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DevComboBox.FormattingEnabled = true;
            this.DevComboBox.Location = new System.Drawing.Point(525, 12);
            this.DevComboBox.MaxLength = 4;
            this.DevComboBox.Name = "DevComboBox";
            this.DevComboBox.Size = new System.Drawing.Size(209, 29);
            this.DevComboBox.TabIndex = 55;
            this.DevComboBox.Click += new System.EventHandler(this.DevComboBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(401, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 18);
            this.label11.TabIndex = 54;
            this.label11.Text = "设备";
            // 
            // RegHighByteEnable
            // 
            this.RegHighByteEnable.AutoSize = true;
            this.RegHighByteEnable.Checked = true;
            this.RegHighByteEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RegHighByteEnable.Location = new System.Drawing.Point(637, 101);
            this.RegHighByteEnable.Name = "RegHighByteEnable";
            this.RegHighByteEnable.Size = new System.Drawing.Size(22, 21);
            this.RegHighByteEnable.TabIndex = 56;
            this.RegHighByteEnable.UseVisualStyleBackColor = true;
            this.RegHighByteEnable.CheckedChanged += new System.EventHandler(this.ByteSelect_CheckedChanged);
            // 
            // RegLowByteEnable
            // 
            this.RegLowByteEnable.AutoSize = true;
            this.RegLowByteEnable.Checked = true;
            this.RegLowByteEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RegLowByteEnable.Location = new System.Drawing.Point(637, 138);
            this.RegLowByteEnable.Name = "RegLowByteEnable";
            this.RegLowByteEnable.Size = new System.Drawing.Size(22, 21);
            this.RegLowByteEnable.TabIndex = 57;
            this.RegLowByteEnable.UseVisualStyleBackColor = true;
            this.RegLowByteEnable.CheckedChanged += new System.EventHandler(this.ByteSelect_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(750, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 58;
            this.label8.Text = "波形";
            // 
            // WaveName
            // 
            this.WaveName.Location = new System.Drawing.Point(525, 57);
            this.WaveName.MaxLength = 12;
            this.WaveName.Name = "WaveName";
            this.WaveName.Size = new System.Drawing.Size(106, 31);
            this.WaveName.TabIndex = 60;
            this.WaveName.Text = "Wave1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(401, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 59;
            this.label12.Text = "波形名称";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(750, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 18);
            this.label13.TabIndex = 63;
            this.label13.Text = "N/Px";
            // 
            // NumberPerPixel
            // 
            this.NumberPerPixel.Location = new System.Drawing.Point(819, 97);
            this.NumberPerPixel.Maximum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.NumberPerPixel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumberPerPixel.Name = "NumberPerPixel";
            this.NumberPerPixel.Size = new System.Drawing.Size(86, 31);
            this.NumberPerPixel.TabIndex = 62;
            this.NumberPerPixel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumberPerPixel.ValueChanged += new System.EventHandler(this.NumberPerPixel_ValueChanged);
            this.NumberPerPixel.Click += new System.EventHandler(this.NumberPerPixel_ValueChanged);
            // 
            // WavePictureBox1
            // 
            this.WavePictureBox1.BackColor = System.Drawing.Color.White;
            this.WavePictureBox1.CurrentTimeBase = 1000D;
            this.WavePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("WavePictureBox1.Image")));
            this.WavePictureBox1.Location = new System.Drawing.Point(405, 169);
            this.WavePictureBox1.Name = "WavePictureBox1";
            this.WavePictureBox1.Size = new System.Drawing.Size(500, 420);
            this.WavePictureBox1.TabIndex = 66;
            this.WavePictureBox1.TabStop = false;
            this.WavePictureBox1.VerticalScale = 10D;
            // 
            // ColorBox
            // 
            this.ColorBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColorBox.FormattingEnabled = true;
            colorItem13.Color = System.Drawing.Color.Red;
            colorItem13.Name = "红";
            colorItem14.Color = System.Drawing.Color.Lime;
            colorItem14.Name = "绿";
            colorItem15.Color = System.Drawing.Color.DeepSkyBlue;
            colorItem15.Name = "蓝";
            colorItem16.Color = System.Drawing.Color.Yellow;
            colorItem16.Name = "黄";
            colorItem17.Color = System.Drawing.Color.Black;
            colorItem17.Name = "黑";
            colorItem18.Color = System.Drawing.Color.HotPink;
            colorItem18.Name = "粉";
            colorItem19.Color = System.Drawing.Color.Red;
            colorItem19.Name = "红";
            colorItem20.Color = System.Drawing.Color.Lime;
            colorItem20.Name = "绿";
            colorItem21.Color = System.Drawing.Color.DeepSkyBlue;
            colorItem21.Name = "蓝";
            colorItem22.Color = System.Drawing.Color.Yellow;
            colorItem22.Name = "黄";
            colorItem23.Color = System.Drawing.Color.Black;
            colorItem23.Name = "黑";
            colorItem24.Color = System.Drawing.Color.HotPink;
            colorItem24.Name = "粉";
            this.ColorBox.Items.AddRange(new object[] {
            colorItem13,
            colorItem14,
            colorItem15,
            colorItem16,
            colorItem17,
            colorItem18,
            colorItem19,
            colorItem20,
            colorItem21,
            colorItem22,
            colorItem23,
            colorItem24});
            this.ColorBox.Location = new System.Drawing.Point(645, 56);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.SelectedColor = System.Drawing.Color.Empty;
            this.ColorBox.Size = new System.Drawing.Size(89, 32);
            this.ColorBox.TabIndex = 65;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1518, 600);
            this.Controls.Add(this.WavePictureBox1);
            this.Controls.Add(this.ColorBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.NumberPerPixel);
            this.Controls.Add(this.WaveName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RegLowByteEnable);
            this.Controls.Add(this.RegHighByteEnable);
            this.Controls.Add(this.DevComboBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.WaveTimeBase);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.WaveDataLowBit);
            this.Controls.Add(this.DeleteWaveButton);
            this.Controls.Add(this.WaveList);
            this.Controls.Add(this.AddWaveButton);
            this.Controls.Add(this.WaveDataHighBit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.I2CMsgTextBox);
            this.Controls.Add(this.IICToolSwitch);
            this.Controls.Add(this.ReadListButton);
            this.Controls.Add(this.RegEditButton);
            this.Controls.Add(this.RegDeleteButton);
            this.Controls.Add(this.AddRegToListButton);
            this.Controls.Add(this.ListBytesNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ListRegAddr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ListDeviceAddrComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DialogReadBytesNumber);
            this.Controls.Add(this.FlushRegFrequent);
            this.Controls.Add(this.AutoRegFlush);
            this.Controls.Add(this.ClearDialogMsgButton);
            this.Controls.Add(this.DialogReadRegButton);
            this.Controls.Add(this.I2CWriteButton);
            this.Controls.Add(this.DialogWriteBytes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DialogRegAddrTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DialogDeviceAddrComboBox);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.RegisterGridView);
            this.Controls.Add(this.DeviceComboBox);
            this.Controls.Add(this.DeviceScanButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "I2C Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RegisterGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlushRegFrequent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialogReadBytesNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListBytesNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaveTimeBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberPerPixel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WavePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeviceScanButton;
        private System.Windows.Forms.ComboBox DeviceComboBox;
        private System.Windows.Forms.DataGridView RegisterGridView;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.ComboBox DialogDeviceAddrComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DialogRegAddrTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DialogWriteBytes;
        private System.Windows.Forms.Button I2CWriteButton;
        private System.Windows.Forms.Button DialogReadRegButton;
        private System.Windows.Forms.CheckBox AutoRegFlush;
        private System.Windows.Forms.NumericUpDown FlushRegFrequent;
        private System.Windows.Forms.NumericUpDown DialogReadBytesNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ListDeviceAddrComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ListRegAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ListBytesNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button AddRegToListButton;
        private System.Windows.Forms.Button RegDeleteButton;
        private System.Windows.Forms.Button RegEditButton;
        private System.Windows.Forms.Button ReadListButton;
        private System.Windows.Forms.CheckBox IICToolSwitch;
        private System.Windows.Forms.Button ClearDialogMsgButton;
        private System.Windows.Forms.RichTextBox I2CMsgTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox WaveDataHighBit;
        private System.Windows.Forms.ComboBox WaveList;
        private System.Windows.Forms.Button DeleteWaveButton;
        private System.Windows.Forms.ComboBox WaveDataLowBit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown WaveTimeBase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox DevComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button AddWaveButton;
        private System.Windows.Forms.CheckBox RegHighByteEnable;
        private System.Windows.Forms.CheckBox RegLowByteEnable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox WaveName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown NumberPerPixel;
        private ColorComboBox ColorBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 寄存器地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 寄存器数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 内容;
        private WavePictureBox WavePictureBox1;
    }
}