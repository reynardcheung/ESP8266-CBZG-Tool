namespace WirelessUart
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
            this.OnWirelessUartButton = new System.Windows.Forms.Button();
            this.OffWirelessUartButton = new System.Windows.Forms.Button();
            this.COMComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectStatus = new System.Windows.Forms.TextBox();
            this.RXrichTextBox = new System.Windows.Forms.RichTextBox();
            this.RXHEXrichTextBox = new System.Windows.Forms.RichTextBox();
            this.SendRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ConditionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ConditionHexCheckBox = new System.Windows.Forms.CheckBox();
            this.OutputHexCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.AddIntoButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UartSendButton = new System.Windows.Forms.Button();
            this.UartHexSendButton = new System.Windows.Forms.Button();
            this.ConditionOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.QuickSend1 = new System.Windows.Forms.Button();
            this.QuickSendTextBox1 = new System.Windows.Forms.TextBox();
            this.COMForwardButton = new System.Windows.Forms.Button();
            this.OffCOMForwardButton = new System.Windows.Forms.Button();
            this.ComForwardStatus = new System.Windows.Forms.TextBox();
            this.TextSaveButton = new System.Windows.Forms.Button();
            this.HexSaveButton = new System.Windows.Forms.Button();
            this.UartFIleSendButton = new System.Windows.Forms.Button();
            this.CounterTextBox = new System.Windows.Forms.TextBox();
            this.DataClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ConditionOutputDelay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.QuickSendHexCheckBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.EditConditionButton = new System.Windows.Forms.Button();
            this.ReadConditionButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ConditionUpButton = new System.Windows.Forms.Button();
            this.ConditionDownButton = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox2 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox2 = new System.Windows.Forms.TextBox();
            this.QuickSend2 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox3 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox3 = new System.Windows.Forms.TextBox();
            this.QuickSend3 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox4 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox4 = new System.Windows.Forms.TextBox();
            this.QuickSend4 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox5 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox5 = new System.Windows.Forms.TextBox();
            this.QuickSend5 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox6 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox6 = new System.Windows.Forms.TextBox();
            this.QuickSend6 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox7 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox7 = new System.Windows.Forms.TextBox();
            this.QuickSend7 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox8 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox8 = new System.Windows.Forms.TextBox();
            this.QuickSend8 = new System.Windows.Forms.Button();
            this.QuickSendHexCheckBox9 = new System.Windows.Forms.CheckBox();
            this.QuickSendTextBox9 = new System.Windows.Forms.TextBox();
            this.QuickSend9 = new System.Windows.Forms.Button();
            this.SerialPinSyncCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // OnWirelessUartButton
            // 
            this.OnWirelessUartButton.Location = new System.Drawing.Point(803, 543);
            this.OnWirelessUartButton.Name = "OnWirelessUartButton";
            this.OnWirelessUartButton.Size = new System.Drawing.Size(151, 34);
            this.OnWirelessUartButton.TabIndex = 0;
            this.OnWirelessUartButton.Text = "开启无线串口";
            this.OnWirelessUartButton.UseVisualStyleBackColor = true;
            this.OnWirelessUartButton.Click += new System.EventHandler(this.OnWirelessUartButton_Click);
            // 
            // OffWirelessUartButton
            // 
            this.OffWirelessUartButton.Enabled = false;
            this.OffWirelessUartButton.Location = new System.Drawing.Point(803, 583);
            this.OffWirelessUartButton.Name = "OffWirelessUartButton";
            this.OffWirelessUartButton.Size = new System.Drawing.Size(151, 34);
            this.OffWirelessUartButton.TabIndex = 1;
            this.OffWirelessUartButton.Text = "关闭无线串口";
            this.OffWirelessUartButton.UseVisualStyleBackColor = true;
            this.OffWirelessUartButton.Click += new System.EventHandler(this.OffWirelessUartButton_Click);
            // 
            // COMComboBox
            // 
            this.COMComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMComboBox.FormattingEnabled = true;
            this.COMComboBox.Location = new System.Drawing.Point(960, 473);
            this.COMComboBox.Name = "COMComboBox";
            this.COMComboBox.Size = new System.Drawing.Size(219, 29);
            this.COMComboBox.TabIndex = 2;
            this.COMComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.COMComboBox_MouseClick);
            // 
            // ConnectStatus
            // 
            this.ConnectStatus.BackColor = System.Drawing.Color.White;
            this.ConnectStatus.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConnectStatus.Location = new System.Drawing.Point(803, 473);
            this.ConnectStatus.Multiline = true;
            this.ConnectStatus.Name = "ConnectStatus";
            this.ConnectStatus.ReadOnly = true;
            this.ConnectStatus.Size = new System.Drawing.Size(151, 64);
            this.ConnectStatus.TabIndex = 3;
            this.ConnectStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ConnectStatus.WordWrap = false;
            // 
            // RXrichTextBox
            // 
            this.RXrichTextBox.BackColor = System.Drawing.Color.White;
            this.RXrichTextBox.Location = new System.Drawing.Point(12, 31);
            this.RXrichTextBox.Name = "RXrichTextBox";
            this.RXrichTextBox.ReadOnly = true;
            this.RXrichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RXrichTextBox.Size = new System.Drawing.Size(656, 424);
            this.RXrichTextBox.TabIndex = 4;
            this.RXrichTextBox.Text = "";
            // 
            // RXHEXrichTextBox
            // 
            this.RXHEXrichTextBox.BackColor = System.Drawing.Color.White;
            this.RXHEXrichTextBox.Location = new System.Drawing.Point(698, 31);
            this.RXHEXrichTextBox.Name = "RXHEXrichTextBox";
            this.RXHEXrichTextBox.ReadOnly = true;
            this.RXHEXrichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RXHEXrichTextBox.Size = new System.Drawing.Size(656, 424);
            this.RXHEXrichTextBox.TabIndex = 5;
            this.RXHEXrichTextBox.Text = "";
            // 
            // SendRichTextBox
            // 
            this.SendRichTextBox.Location = new System.Drawing.Point(12, 481);
            this.SendRichTextBox.Name = "SendRichTextBox";
            this.SendRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.SendRichTextBox.Size = new System.Drawing.Size(656, 138);
            this.SendRichTextBox.TabIndex = 6;
            this.SendRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "文本接收";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(703, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "HEX接收";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1370, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(358, 423);
            this.dataGridView1.TabIndex = 9;
            // 
            // ConditionTextBox
            // 
            this.ConditionTextBox.Location = new System.Drawing.Point(1445, 505);
            this.ConditionTextBox.Name = "ConditionTextBox";
            this.ConditionTextBox.Size = new System.Drawing.Size(208, 31);
            this.ConditionTextBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1364, 510);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "条件：";
            // 
            // ConditionHexCheckBox
            // 
            this.ConditionHexCheckBox.AutoSize = true;
            this.ConditionHexCheckBox.Location = new System.Drawing.Point(1659, 510);
            this.ConditionHexCheckBox.Name = "ConditionHexCheckBox";
            this.ConditionHexCheckBox.Size = new System.Drawing.Size(69, 25);
            this.ConditionHexCheckBox.TabIndex = 12;
            this.ConditionHexCheckBox.Text = "HEX";
            this.ConditionHexCheckBox.UseVisualStyleBackColor = true;
            // 
            // OutputHexCheckBox
            // 
            this.OutputHexCheckBox.AutoSize = true;
            this.OutputHexCheckBox.Location = new System.Drawing.Point(1659, 546);
            this.OutputHexCheckBox.Name = "OutputHexCheckBox";
            this.OutputHexCheckBox.Size = new System.Drawing.Size(69, 25);
            this.OutputHexCheckBox.TabIndex = 15;
            this.OutputHexCheckBox.Text = "HEX";
            this.OutputHexCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1364, 547);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "输出：";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(1445, 542);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(208, 31);
            this.OutputTextBox.TabIndex = 13;
            // 
            // AddIntoButton
            // 
            this.AddIntoButton.Location = new System.Drawing.Point(1368, 577);
            this.AddIntoButton.Name = "AddIntoButton";
            this.AddIntoButton.Size = new System.Drawing.Size(85, 32);
            this.AddIntoButton.TabIndex = 16;
            this.AddIntoButton.Text = "添加";
            this.AddIntoButton.UseVisualStyleBackColor = true;
            this.AddIntoButton.Click += new System.EventHandler(this.AddIntoButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(1459, 577);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(85, 32);
            this.DeleteButton.TabIndex = 17;
            this.DeleteButton.Text = "删除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UartSendButton
            // 
            this.UartSendButton.Location = new System.Drawing.Point(678, 481);
            this.UartSendButton.Name = "UartSendButton";
            this.UartSendButton.Size = new System.Drawing.Size(110, 36);
            this.UartSendButton.TabIndex = 18;
            this.UartSendButton.Text = "文本发送";
            this.UartSendButton.UseVisualStyleBackColor = true;
            this.UartSendButton.Click += new System.EventHandler(this.UartSendButton_Click);
            // 
            // UartHexSendButton
            // 
            this.UartHexSendButton.Location = new System.Drawing.Point(678, 523);
            this.UartHexSendButton.Name = "UartHexSendButton";
            this.UartHexSendButton.Size = new System.Drawing.Size(110, 36);
            this.UartHexSendButton.TabIndex = 19;
            this.UartHexSendButton.Text = "HEX发送";
            this.UartHexSendButton.UseVisualStyleBackColor = true;
            this.UartHexSendButton.Click += new System.EventHandler(this.UartHexSendButton_Click);
            // 
            // ConditionOutputCheckBox
            // 
            this.ConditionOutputCheckBox.AutoSize = true;
            this.ConditionOutputCheckBox.Enabled = false;
            this.ConditionOutputCheckBox.Location = new System.Drawing.Point(1369, 471);
            this.ConditionOutputCheckBox.Name = "ConditionOutputCheckBox";
            this.ConditionOutputCheckBox.Size = new System.Drawing.Size(162, 25);
            this.ConditionOutputCheckBox.TabIndex = 20;
            this.ConditionOutputCheckBox.Text = "条件输出开关";
            this.ConditionOutputCheckBox.UseVisualStyleBackColor = true;
            this.ConditionOutputCheckBox.CheckedChanged += new System.EventHandler(this.ConditionOutputCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1375, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 21);
            this.label5.TabIndex = 21;
            this.label5.Text = "条件输出";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 458);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 21);
            this.label6.TabIndex = 22;
            this.label6.Text = "消息发送";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1732, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 21);
            this.label7.TabIndex = 23;
            this.label7.Text = "快捷指令";
            // 
            // QuickSend1
            // 
            this.QuickSend1.Location = new System.Drawing.Point(1852, 31);
            this.QuickSend1.Name = "QuickSend1";
            this.QuickSend1.Size = new System.Drawing.Size(38, 57);
            this.QuickSend1.TabIndex = 24;
            this.QuickSend1.Text = "📤";
            this.QuickSend1.UseVisualStyleBackColor = true;
            this.QuickSend1.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendTextBox1
            // 
            this.QuickSendTextBox1.Location = new System.Drawing.Point(1736, 31);
            this.QuickSendTextBox1.Name = "QuickSendTextBox1";
            this.QuickSendTextBox1.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox1.TabIndex = 25;
            this.QuickSendTextBox1.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // COMForwardButton
            // 
            this.COMForwardButton.Enabled = false;
            this.COMForwardButton.Location = new System.Drawing.Point(960, 544);
            this.COMForwardButton.Name = "COMForwardButton";
            this.COMForwardButton.Size = new System.Drawing.Size(151, 34);
            this.COMForwardButton.TabIndex = 44;
            this.COMForwardButton.Text = "开启端口转发";
            this.COMForwardButton.UseVisualStyleBackColor = true;
            this.COMForwardButton.Click += new System.EventHandler(this.COMForwardButton_Click);
            // 
            // OffCOMForwardButton
            // 
            this.OffCOMForwardButton.Enabled = false;
            this.OffCOMForwardButton.Location = new System.Drawing.Point(960, 582);
            this.OffCOMForwardButton.Name = "OffCOMForwardButton";
            this.OffCOMForwardButton.Size = new System.Drawing.Size(151, 34);
            this.OffCOMForwardButton.TabIndex = 45;
            this.OffCOMForwardButton.Text = "关闭端口转发";
            this.OffCOMForwardButton.UseVisualStyleBackColor = true;
            this.OffCOMForwardButton.Click += new System.EventHandler(this.OffCOMForwardButton_Click);
            // 
            // ComForwardStatus
            // 
            this.ComForwardStatus.BackColor = System.Drawing.Color.White;
            this.ComForwardStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComForwardStatus.Location = new System.Drawing.Point(960, 509);
            this.ComForwardStatus.Multiline = true;
            this.ComForwardStatus.Name = "ComForwardStatus";
            this.ComForwardStatus.ReadOnly = true;
            this.ComForwardStatus.Size = new System.Drawing.Size(219, 29);
            this.ComForwardStatus.TabIndex = 46;
            this.ComForwardStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ComForwardStatus.WordWrap = false;
            // 
            // TextSaveButton
            // 
            this.TextSaveButton.Location = new System.Drawing.Point(1209, 544);
            this.TextSaveButton.Name = "TextSaveButton";
            this.TextSaveButton.Size = new System.Drawing.Size(108, 34);
            this.TextSaveButton.TabIndex = 47;
            this.TextSaveButton.Text = "文本保存";
            this.TextSaveButton.UseVisualStyleBackColor = true;
            this.TextSaveButton.Click += new System.EventHandler(this.TextSaveButton_Click);
            // 
            // HexSaveButton
            // 
            this.HexSaveButton.Location = new System.Drawing.Point(1209, 583);
            this.HexSaveButton.Name = "HexSaveButton";
            this.HexSaveButton.Size = new System.Drawing.Size(108, 34);
            this.HexSaveButton.TabIndex = 48;
            this.HexSaveButton.Text = "HEX保存";
            this.HexSaveButton.UseVisualStyleBackColor = true;
            this.HexSaveButton.Click += new System.EventHandler(this.HexSaveButton_Click);
            // 
            // UartFIleSendButton
            // 
            this.UartFIleSendButton.Location = new System.Drawing.Point(678, 565);
            this.UartFIleSendButton.Name = "UartFIleSendButton";
            this.UartFIleSendButton.Size = new System.Drawing.Size(110, 36);
            this.UartFIleSendButton.TabIndex = 51;
            this.UartFIleSendButton.Text = "文件发送";
            this.UartFIleSendButton.UseVisualStyleBackColor = true;
            this.UartFIleSendButton.Click += new System.EventHandler(this.UartFIleSendButton_Click);
            // 
            // CounterTextBox
            // 
            this.CounterTextBox.BackColor = System.Drawing.Color.White;
            this.CounterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CounterTextBox.Location = new System.Drawing.Point(1209, 484);
            this.CounterTextBox.Multiline = true;
            this.CounterTextBox.Name = "CounterTextBox";
            this.CounterTextBox.ReadOnly = true;
            this.CounterTextBox.Size = new System.Drawing.Size(145, 49);
            this.CounterTextBox.TabIndex = 52;
            this.CounterTextBox.Text = "RX:0\r\nTX:0";
            // 
            // DataClear
            // 
            this.DataClear.Location = new System.Drawing.Point(1323, 544);
            this.DataClear.Name = "DataClear";
            this.DataClear.Size = new System.Drawing.Size(31, 73);
            this.DataClear.TabIndex = 53;
            this.DataClear.Text = "清除";
            this.DataClear.UseVisualStyleBackColor = true;
            this.DataClear.Click += new System.EventHandler(this.DataClear_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 28);
            this.button1.TabIndex = 54;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // ConditionOutputDelay
            // 
            this.ConditionOutputDelay.Location = new System.Drawing.Point(1573, 468);
            this.ConditionOutputDelay.MaxLength = 5;
            this.ConditionOutputDelay.Name = "ConditionOutputDelay";
            this.ConditionOutputDelay.Size = new System.Drawing.Size(67, 31);
            this.ConditionOutputDelay.TabIndex = 55;
            this.ConditionOutputDelay.Text = "50";
            this.ConditionOutputDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConditionOutputDelay_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1525, 473);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 21);
            this.label8.TabIndex = 56;
            this.label8.Text = "延迟";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // QuickSendHexCheckBox1
            // 
            this.QuickSendHexCheckBox1.AutoSize = true;
            this.QuickSendHexCheckBox1.Location = new System.Drawing.Point(1736, 63);
            this.QuickSendHexCheckBox1.Name = "QuickSendHexCheckBox1";
            this.QuickSendHexCheckBox1.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox1.TabIndex = 57;
            this.QuickSendHexCheckBox1.Text = "HEX模式";
            this.QuickSendHexCheckBox1.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox1.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1645, 471);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 21);
            this.label9.TabIndex = 58;
            this.label9.Text = "ms";
            // 
            // EditConditionButton
            // 
            this.EditConditionButton.Location = new System.Drawing.Point(1639, 577);
            this.EditConditionButton.Name = "EditConditionButton";
            this.EditConditionButton.Size = new System.Drawing.Size(85, 32);
            this.EditConditionButton.TabIndex = 59;
            this.EditConditionButton.Text = "修改";
            this.EditConditionButton.UseVisualStyleBackColor = true;
            this.EditConditionButton.Click += new System.EventHandler(this.EditConditionButton_Click);
            // 
            // ReadConditionButton
            // 
            this.ReadConditionButton.Location = new System.Drawing.Point(1550, 577);
            this.ReadConditionButton.Name = "ReadConditionButton";
            this.ReadConditionButton.Size = new System.Drawing.Size(85, 32);
            this.ReadConditionButton.TabIndex = 60;
            this.ReadConditionButton.Text = "读取";
            this.ReadConditionButton.UseVisualStyleBackColor = true;
            this.ReadConditionButton.Click += new System.EventHandler(this.ReadConditionButton_Click);
            // 
            // ConditionUpButton
            // 
            this.ConditionUpButton.Location = new System.Drawing.Point(1683, 467);
            this.ConditionUpButton.Name = "ConditionUpButton";
            this.ConditionUpButton.Size = new System.Drawing.Size(19, 32);
            this.ConditionUpButton.TabIndex = 61;
            this.ConditionUpButton.Text = "↑";
            this.ConditionUpButton.UseVisualStyleBackColor = true;
            this.ConditionUpButton.Click += new System.EventHandler(this.ConditionUpButton_Click);
            // 
            // ConditionDownButton
            // 
            this.ConditionDownButton.Location = new System.Drawing.Point(1705, 467);
            this.ConditionDownButton.Name = "ConditionDownButton";
            this.ConditionDownButton.Size = new System.Drawing.Size(19, 32);
            this.ConditionDownButton.TabIndex = 62;
            this.ConditionDownButton.Text = "↓";
            this.ConditionDownButton.UseVisualStyleBackColor = true;
            this.ConditionDownButton.Click += new System.EventHandler(this.ConditionDownButton_Click);
            // 
            // QuickSendHexCheckBox2
            // 
            this.QuickSendHexCheckBox2.AutoSize = true;
            this.QuickSendHexCheckBox2.Location = new System.Drawing.Point(1736, 126);
            this.QuickSendHexCheckBox2.Name = "QuickSendHexCheckBox2";
            this.QuickSendHexCheckBox2.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox2.TabIndex = 65;
            this.QuickSendHexCheckBox2.Text = "HEX模式";
            this.QuickSendHexCheckBox2.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox2.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox2
            // 
            this.QuickSendTextBox2.Location = new System.Drawing.Point(1736, 94);
            this.QuickSendTextBox2.Name = "QuickSendTextBox2";
            this.QuickSendTextBox2.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox2.TabIndex = 64;
            this.QuickSendTextBox2.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend2
            // 
            this.QuickSend2.Location = new System.Drawing.Point(1852, 94);
            this.QuickSend2.Name = "QuickSend2";
            this.QuickSend2.Size = new System.Drawing.Size(38, 57);
            this.QuickSend2.TabIndex = 63;
            this.QuickSend2.Text = "📤";
            this.QuickSend2.UseVisualStyleBackColor = true;
            this.QuickSend2.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox3
            // 
            this.QuickSendHexCheckBox3.AutoSize = true;
            this.QuickSendHexCheckBox3.Location = new System.Drawing.Point(1736, 189);
            this.QuickSendHexCheckBox3.Name = "QuickSendHexCheckBox3";
            this.QuickSendHexCheckBox3.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox3.TabIndex = 68;
            this.QuickSendHexCheckBox3.Text = "HEX模式";
            this.QuickSendHexCheckBox3.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox3.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox3
            // 
            this.QuickSendTextBox3.Location = new System.Drawing.Point(1736, 157);
            this.QuickSendTextBox3.Name = "QuickSendTextBox3";
            this.QuickSendTextBox3.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox3.TabIndex = 67;
            this.QuickSendTextBox3.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend3
            // 
            this.QuickSend3.Location = new System.Drawing.Point(1852, 157);
            this.QuickSend3.Name = "QuickSend3";
            this.QuickSend3.Size = new System.Drawing.Size(38, 57);
            this.QuickSend3.TabIndex = 66;
            this.QuickSend3.Text = "📤";
            this.QuickSend3.UseVisualStyleBackColor = true;
            this.QuickSend3.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox4
            // 
            this.QuickSendHexCheckBox4.AutoSize = true;
            this.QuickSendHexCheckBox4.Location = new System.Drawing.Point(1736, 252);
            this.QuickSendHexCheckBox4.Name = "QuickSendHexCheckBox4";
            this.QuickSendHexCheckBox4.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox4.TabIndex = 71;
            this.QuickSendHexCheckBox4.Text = "HEX模式";
            this.QuickSendHexCheckBox4.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox4.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox4
            // 
            this.QuickSendTextBox4.Location = new System.Drawing.Point(1736, 220);
            this.QuickSendTextBox4.Name = "QuickSendTextBox4";
            this.QuickSendTextBox4.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox4.TabIndex = 70;
            this.QuickSendTextBox4.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend4
            // 
            this.QuickSend4.Location = new System.Drawing.Point(1852, 220);
            this.QuickSend4.Name = "QuickSend4";
            this.QuickSend4.Size = new System.Drawing.Size(38, 57);
            this.QuickSend4.TabIndex = 69;
            this.QuickSend4.Text = "📤";
            this.QuickSend4.UseVisualStyleBackColor = true;
            this.QuickSend4.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox5
            // 
            this.QuickSendHexCheckBox5.AutoSize = true;
            this.QuickSendHexCheckBox5.Location = new System.Drawing.Point(1736, 315);
            this.QuickSendHexCheckBox5.Name = "QuickSendHexCheckBox5";
            this.QuickSendHexCheckBox5.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox5.TabIndex = 74;
            this.QuickSendHexCheckBox5.Text = "HEX模式";
            this.QuickSendHexCheckBox5.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox5.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox5
            // 
            this.QuickSendTextBox5.Location = new System.Drawing.Point(1736, 283);
            this.QuickSendTextBox5.Name = "QuickSendTextBox5";
            this.QuickSendTextBox5.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox5.TabIndex = 73;
            this.QuickSendTextBox5.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend5
            // 
            this.QuickSend5.Location = new System.Drawing.Point(1852, 283);
            this.QuickSend5.Name = "QuickSend5";
            this.QuickSend5.Size = new System.Drawing.Size(38, 57);
            this.QuickSend5.TabIndex = 72;
            this.QuickSend5.Text = "📤";
            this.QuickSend5.UseVisualStyleBackColor = true;
            this.QuickSend5.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox6
            // 
            this.QuickSendHexCheckBox6.AutoSize = true;
            this.QuickSendHexCheckBox6.Location = new System.Drawing.Point(1736, 378);
            this.QuickSendHexCheckBox6.Name = "QuickSendHexCheckBox6";
            this.QuickSendHexCheckBox6.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox6.TabIndex = 77;
            this.QuickSendHexCheckBox6.Text = "HEX模式";
            this.QuickSendHexCheckBox6.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox6.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox6
            // 
            this.QuickSendTextBox6.Location = new System.Drawing.Point(1736, 346);
            this.QuickSendTextBox6.Name = "QuickSendTextBox6";
            this.QuickSendTextBox6.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox6.TabIndex = 76;
            this.QuickSendTextBox6.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend6
            // 
            this.QuickSend6.Location = new System.Drawing.Point(1852, 346);
            this.QuickSend6.Name = "QuickSend6";
            this.QuickSend6.Size = new System.Drawing.Size(38, 57);
            this.QuickSend6.TabIndex = 75;
            this.QuickSend6.Text = "📤";
            this.QuickSend6.UseVisualStyleBackColor = true;
            this.QuickSend6.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox7
            // 
            this.QuickSendHexCheckBox7.AutoSize = true;
            this.QuickSendHexCheckBox7.Location = new System.Drawing.Point(1736, 441);
            this.QuickSendHexCheckBox7.Name = "QuickSendHexCheckBox7";
            this.QuickSendHexCheckBox7.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox7.TabIndex = 80;
            this.QuickSendHexCheckBox7.Text = "HEX模式";
            this.QuickSendHexCheckBox7.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox7.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox7
            // 
            this.QuickSendTextBox7.Location = new System.Drawing.Point(1736, 409);
            this.QuickSendTextBox7.Name = "QuickSendTextBox7";
            this.QuickSendTextBox7.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox7.TabIndex = 79;
            this.QuickSendTextBox7.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend7
            // 
            this.QuickSend7.Location = new System.Drawing.Point(1852, 409);
            this.QuickSend7.Name = "QuickSend7";
            this.QuickSend7.Size = new System.Drawing.Size(38, 57);
            this.QuickSend7.TabIndex = 78;
            this.QuickSend7.Text = "📤";
            this.QuickSend7.UseVisualStyleBackColor = true;
            this.QuickSend7.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox8
            // 
            this.QuickSendHexCheckBox8.AutoSize = true;
            this.QuickSendHexCheckBox8.Location = new System.Drawing.Point(1736, 505);
            this.QuickSendHexCheckBox8.Name = "QuickSendHexCheckBox8";
            this.QuickSendHexCheckBox8.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox8.TabIndex = 83;
            this.QuickSendHexCheckBox8.Text = "HEX模式";
            this.QuickSendHexCheckBox8.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox8.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox8
            // 
            this.QuickSendTextBox8.Location = new System.Drawing.Point(1736, 473);
            this.QuickSendTextBox8.Name = "QuickSendTextBox8";
            this.QuickSendTextBox8.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox8.TabIndex = 82;
            this.QuickSendTextBox8.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend8
            // 
            this.QuickSend8.Location = new System.Drawing.Point(1852, 473);
            this.QuickSend8.Name = "QuickSend8";
            this.QuickSend8.Size = new System.Drawing.Size(38, 57);
            this.QuickSend8.TabIndex = 81;
            this.QuickSend8.Text = "📤";
            this.QuickSend8.UseVisualStyleBackColor = true;
            this.QuickSend8.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // QuickSendHexCheckBox9
            // 
            this.QuickSendHexCheckBox9.AutoSize = true;
            this.QuickSendHexCheckBox9.Location = new System.Drawing.Point(1736, 568);
            this.QuickSendHexCheckBox9.Name = "QuickSendHexCheckBox9";
            this.QuickSendHexCheckBox9.Size = new System.Drawing.Size(111, 25);
            this.QuickSendHexCheckBox9.TabIndex = 86;
            this.QuickSendHexCheckBox9.Text = "HEX模式";
            this.QuickSendHexCheckBox9.UseVisualStyleBackColor = true;
            this.QuickSendHexCheckBox9.CheckedChanged += new System.EventHandler(this.QuickSendHexCheckBox_Changed);
            // 
            // QuickSendTextBox9
            // 
            this.QuickSendTextBox9.Location = new System.Drawing.Point(1736, 536);
            this.QuickSendTextBox9.Name = "QuickSendTextBox9";
            this.QuickSendTextBox9.Size = new System.Drawing.Size(110, 31);
            this.QuickSendTextBox9.TabIndex = 85;
            this.QuickSendTextBox9.TextChanged += new System.EventHandler(this.QuickSendTextBox_TextChanged);
            // 
            // QuickSend9
            // 
            this.QuickSend9.Location = new System.Drawing.Point(1852, 536);
            this.QuickSend9.Name = "QuickSend9";
            this.QuickSend9.Size = new System.Drawing.Size(38, 57);
            this.QuickSend9.TabIndex = 84;
            this.QuickSend9.Text = "📤";
            this.QuickSend9.UseVisualStyleBackColor = true;
            this.QuickSend9.Click += new System.EventHandler(this.QuickSendButton_Click);
            // 
            // SerialPinSyncCheckBox
            // 
            this.SerialPinSyncCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.SerialPinSyncCheckBox.AutoSize = true;
            this.SerialPinSyncCheckBox.BackColor = System.Drawing.Color.Red;
            this.SerialPinSyncCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SerialPinSyncCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SerialPinSyncCheckBox.Enabled = false;
            this.SerialPinSyncCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.GreenYellow;
            this.SerialPinSyncCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.SerialPinSyncCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SerialPinSyncCheckBox.Location = new System.Drawing.Point(1119, 544);
            this.SerialPinSyncCheckBox.Name = "SerialPinSyncCheckBox";
            this.SerialPinSyncCheckBox.Size = new System.Drawing.Size(62, 73);
            this.SerialPinSyncCheckBox.TabIndex = 87;
            this.SerialPinSyncCheckBox.Text = "同步\r\nDTR\r\nRTS";
            this.SerialPinSyncCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SerialPinSyncCheckBox.UseVisualStyleBackColor = false;
            this.SerialPinSyncCheckBox.CheckedChanged += new System.EventHandler(this.SerialPinSyncCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1893, 632);
            this.Controls.Add(this.SerialPinSyncCheckBox);
            this.Controls.Add(this.QuickSendHexCheckBox9);
            this.Controls.Add(this.QuickSendTextBox9);
            this.Controls.Add(this.QuickSend9);
            this.Controls.Add(this.QuickSendHexCheckBox8);
            this.Controls.Add(this.QuickSendTextBox8);
            this.Controls.Add(this.QuickSend8);
            this.Controls.Add(this.QuickSendHexCheckBox7);
            this.Controls.Add(this.QuickSendTextBox7);
            this.Controls.Add(this.QuickSend7);
            this.Controls.Add(this.QuickSendHexCheckBox6);
            this.Controls.Add(this.QuickSendTextBox6);
            this.Controls.Add(this.QuickSend6);
            this.Controls.Add(this.QuickSendHexCheckBox5);
            this.Controls.Add(this.QuickSendTextBox5);
            this.Controls.Add(this.QuickSend5);
            this.Controls.Add(this.QuickSendHexCheckBox4);
            this.Controls.Add(this.QuickSendTextBox4);
            this.Controls.Add(this.QuickSend4);
            this.Controls.Add(this.QuickSendHexCheckBox3);
            this.Controls.Add(this.QuickSendTextBox3);
            this.Controls.Add(this.QuickSend3);
            this.Controls.Add(this.QuickSendHexCheckBox2);
            this.Controls.Add(this.QuickSendTextBox2);
            this.Controls.Add(this.QuickSend2);
            this.Controls.Add(this.ConditionDownButton);
            this.Controls.Add(this.ConditionUpButton);
            this.Controls.Add(this.ReadConditionButton);
            this.Controls.Add(this.EditConditionButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.QuickSendHexCheckBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ConditionOutputDelay);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DataClear);
            this.Controls.Add(this.CounterTextBox);
            this.Controls.Add(this.UartFIleSendButton);
            this.Controls.Add(this.HexSaveButton);
            this.Controls.Add(this.TextSaveButton);
            this.Controls.Add(this.ComForwardStatus);
            this.Controls.Add(this.OffCOMForwardButton);
            this.Controls.Add(this.COMForwardButton);
            this.Controls.Add(this.QuickSendTextBox1);
            this.Controls.Add(this.QuickSend1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ConditionOutputCheckBox);
            this.Controls.Add(this.UartHexSendButton);
            this.Controls.Add(this.UartSendButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddIntoButton);
            this.Controls.Add(this.OutputHexCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.ConditionHexCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ConditionTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendRichTextBox);
            this.Controls.Add(this.RXHEXrichTextBox);
            this.Controls.Add(this.RXrichTextBox);
            this.Controls.Add(this.ConnectStatus);
            this.Controls.Add(this.COMComboBox);
            this.Controls.Add(this.OffWirelessUartButton);
            this.Controls.Add(this.OnWirelessUartButton);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WUART";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OnWirelessUartButton;
        private System.Windows.Forms.Button OffWirelessUartButton;
        private System.Windows.Forms.ComboBox COMComboBox;
        private System.Windows.Forms.TextBox ConnectStatus;
        private System.Windows.Forms.RichTextBox RXrichTextBox;
        private System.Windows.Forms.RichTextBox RXHEXrichTextBox;
        private System.Windows.Forms.RichTextBox SendRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox ConditionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ConditionHexCheckBox;
        private System.Windows.Forms.CheckBox OutputHexCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button AddIntoButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button UartSendButton;
        private System.Windows.Forms.Button UartHexSendButton;
        private System.Windows.Forms.CheckBox ConditionOutputCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button QuickSend1;
        private System.Windows.Forms.TextBox QuickSendTextBox1;
        private System.Windows.Forms.Button COMForwardButton;
        private System.Windows.Forms.Button OffCOMForwardButton;
        private System.Windows.Forms.TextBox ComForwardStatus;
        private System.Windows.Forms.Button TextSaveButton;
        private System.Windows.Forms.Button HexSaveButton;
        private System.Windows.Forms.Button UartFIleSendButton;
        private System.Windows.Forms.TextBox CounterTextBox;
        private System.Windows.Forms.Button DataClear;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ConditionOutputDelay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button EditConditionButton;
        private System.Windows.Forms.Button ReadConditionButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button ConditionUpButton;
        private System.Windows.Forms.Button ConditionDownButton;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox2;
        private System.Windows.Forms.TextBox QuickSendTextBox2;
        private System.Windows.Forms.Button QuickSend2;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox3;
        private System.Windows.Forms.TextBox QuickSendTextBox3;
        private System.Windows.Forms.Button QuickSend3;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox4;
        private System.Windows.Forms.TextBox QuickSendTextBox4;
        private System.Windows.Forms.Button QuickSend4;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox5;
        private System.Windows.Forms.TextBox QuickSendTextBox5;
        private System.Windows.Forms.Button QuickSend5;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox6;
        private System.Windows.Forms.TextBox QuickSendTextBox6;
        private System.Windows.Forms.Button QuickSend6;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox7;
        private System.Windows.Forms.TextBox QuickSendTextBox7;
        private System.Windows.Forms.Button QuickSend7;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox8;
        private System.Windows.Forms.TextBox QuickSendTextBox8;
        private System.Windows.Forms.Button QuickSend8;
        private System.Windows.Forms.CheckBox QuickSendHexCheckBox9;
        private System.Windows.Forms.TextBox QuickSendTextBox9;
        private System.Windows.Forms.Button QuickSend9;
        private System.Windows.Forms.CheckBox SerialPinSyncCheckBox;
    }
}