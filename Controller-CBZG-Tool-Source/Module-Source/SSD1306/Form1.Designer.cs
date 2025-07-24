namespace SSD1306
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
            this.PixelClear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ReadPictureButton = new System.Windows.Forms.Button();
            this.DeviceSwitchCheckBox = new System.Windows.Forms.CheckBox();
            this.SerialSyncButton = new System.Windows.Forms.Button();
            this.CloseSerialSyncButton = new System.Windows.Forms.Button();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            this.SendTextToScreenButton = new System.Windows.Forms.Button();
            this.FontSizeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FontNameComboBox = new System.Windows.Forms.ComboBox();
            this.XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.BinarizationtrackBar = new System.Windows.Forms.TrackBar();
            this.BinarizationLabel = new System.Windows.Forms.Label();
            this.ReadGIFButton = new System.Windows.Forms.Button();
            this.pixelGrid = new SSD1306.PixelGridEditor();
            this.BitPictureBox = new SSD1306.BinarizedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationtrackBar)).BeginInit();
            this.pixelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BitPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PixelClear
            // 
            this.PixelClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PixelClear.Enabled = false;
            this.PixelClear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PixelClear.FlatAppearance.BorderSize = 3;
            this.PixelClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.PixelClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PixelClear.ForeColor = System.Drawing.Color.Black;
            this.PixelClear.Location = new System.Drawing.Point(480, 12);
            this.PixelClear.Name = "PixelClear";
            this.PixelClear.Size = new System.Drawing.Size(150, 84);
            this.PixelClear.TabIndex = 0;
            this.PixelClear.Text = "清屏\r\n停止GIF";
            this.PixelClear.UseVisualStyleBackColor = true;
            this.PixelClear.Click += new System.EventHandler(this.PixelClear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Image";
            // 
            // ReadPictureButton
            // 
            this.ReadPictureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReadPictureButton.Enabled = false;
            this.ReadPictureButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReadPictureButton.FlatAppearance.BorderSize = 3;
            this.ReadPictureButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.ReadPictureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReadPictureButton.ForeColor = System.Drawing.Color.Black;
            this.ReadPictureButton.Location = new System.Drawing.Point(314, 12);
            this.ReadPictureButton.Name = "ReadPictureButton";
            this.ReadPictureButton.Size = new System.Drawing.Size(160, 39);
            this.ReadPictureButton.TabIndex = 1;
            this.ReadPictureButton.Text = "读取图像";
            this.ReadPictureButton.UseVisualStyleBackColor = true;
            this.ReadPictureButton.Click += new System.EventHandler(this.ReadPictureButton_Click);
            // 
            // DeviceSwitchCheckBox
            // 
            this.DeviceSwitchCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.DeviceSwitchCheckBox.AutoSize = true;
            this.DeviceSwitchCheckBox.BackColor = System.Drawing.Color.White;
            this.DeviceSwitchCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DeviceSwitchCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeviceSwitchCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DeviceSwitchCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DeviceSwitchCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.DeviceSwitchCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeviceSwitchCheckBox.Font = new System.Drawing.Font("宋体", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeviceSwitchCheckBox.ForeColor = System.Drawing.Color.Pink;
            this.DeviceSwitchCheckBox.Location = new System.Drawing.Point(35, 20);
            this.DeviceSwitchCheckBox.Name = "DeviceSwitchCheckBox";
            this.DeviceSwitchCheckBox.Size = new System.Drawing.Size(93, 76);
            this.DeviceSwitchCheckBox.TabIndex = 4;
            this.DeviceSwitchCheckBox.Text = "设备\r\n开关";
            this.DeviceSwitchCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DeviceSwitchCheckBox.UseVisualStyleBackColor = false;
            this.DeviceSwitchCheckBox.CheckedChanged += new System.EventHandler(this.DeviceSwitchCheckBox_CheckedChanged);
            // 
            // SerialSyncButton
            // 
            this.SerialSyncButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SerialSyncButton.Enabled = false;
            this.SerialSyncButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SerialSyncButton.FlatAppearance.BorderSize = 3;
            this.SerialSyncButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.SerialSyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SerialSyncButton.ForeColor = System.Drawing.Color.Black;
            this.SerialSyncButton.Location = new System.Drawing.Point(148, 12);
            this.SerialSyncButton.Name = "SerialSyncButton";
            this.SerialSyncButton.Size = new System.Drawing.Size(160, 39);
            this.SerialSyncButton.TabIndex = 5;
            this.SerialSyncButton.Text = "开启串口同步";
            this.SerialSyncButton.UseVisualStyleBackColor = true;
            this.SerialSyncButton.Click += new System.EventHandler(this.SerialSyncButton_Click);
            // 
            // CloseSerialSyncButton
            // 
            this.CloseSerialSyncButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseSerialSyncButton.Enabled = false;
            this.CloseSerialSyncButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseSerialSyncButton.FlatAppearance.BorderSize = 3;
            this.CloseSerialSyncButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.CloseSerialSyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseSerialSyncButton.ForeColor = System.Drawing.Color.Black;
            this.CloseSerialSyncButton.Location = new System.Drawing.Point(148, 57);
            this.CloseSerialSyncButton.Name = "CloseSerialSyncButton";
            this.CloseSerialSyncButton.Size = new System.Drawing.Size(160, 39);
            this.CloseSerialSyncButton.TabIndex = 6;
            this.CloseSerialSyncButton.Text = "关闭串口同步";
            this.CloseSerialSyncButton.UseVisualStyleBackColor = true;
            this.CloseSerialSyncButton.Click += new System.EventHandler(this.CloseSerialSyncButton_Click);
            // 
            // UserTextBox
            // 
            this.UserTextBox.Enabled = false;
            this.UserTextBox.Location = new System.Drawing.Point(23, 102);
            this.UserTextBox.Multiline = true;
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(451, 92);
            this.UserTextBox.TabIndex = 7;
            // 
            // SendTextToScreenButton
            // 
            this.SendTextToScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendTextToScreenButton.Enabled = false;
            this.SendTextToScreenButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SendTextToScreenButton.FlatAppearance.BorderSize = 3;
            this.SendTextToScreenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.SendTextToScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendTextToScreenButton.ForeColor = System.Drawing.Color.Black;
            this.SendTextToScreenButton.Location = new System.Drawing.Point(480, 102);
            this.SendTextToScreenButton.Name = "SendTextToScreenButton";
            this.SendTextToScreenButton.Size = new System.Drawing.Size(150, 92);
            this.SendTextToScreenButton.TabIndex = 8;
            this.SendTextToScreenButton.Text = "发送文本";
            this.SendTextToScreenButton.UseVisualStyleBackColor = true;
            this.SendTextToScreenButton.Click += new System.EventHandler(this.SendTextToScreenButton_Click);
            // 
            // FontSizeComboBox
            // 
            this.FontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontSizeComboBox.Enabled = false;
            this.FontSizeComboBox.FormattingEnabled = true;
            this.FontSizeComboBox.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.FontSizeComboBox.Location = new System.Drawing.Point(379, 200);
            this.FontSizeComboBox.Name = "FontSizeComboBox";
            this.FontSizeComboBox.Size = new System.Drawing.Size(76, 29);
            this.FontSizeComboBox.TabIndex = 9;
            this.FontSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.FontSizeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(321, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "字号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "X坐标";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Y坐标";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(461, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 15;
            this.label4.Text = "字体";
            // 
            // FontNameComboBox
            // 
            this.FontNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontNameComboBox.Enabled = false;
            this.FontNameComboBox.FormattingEnabled = true;
            this.FontNameComboBox.Location = new System.Drawing.Point(519, 200);
            this.FontNameComboBox.Name = "FontNameComboBox";
            this.FontNameComboBox.Size = new System.Drawing.Size(109, 29);
            this.FontNameComboBox.TabIndex = 16;
            this.FontNameComboBox.SelectedIndexChanged += new System.EventHandler(this.FontNameComboBox_SelectedIndexChanged);
            // 
            // XNumericUpDown
            // 
            this.XNumericUpDown.Enabled = false;
            this.XNumericUpDown.Location = new System.Drawing.Point(86, 201);
            this.XNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.XNumericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.XNumericUpDown.Name = "XNumericUpDown";
            this.XNumericUpDown.Size = new System.Drawing.Size(77, 31);
            this.XNumericUpDown.TabIndex = 18;
            this.XNumericUpDown.Click += new System.EventHandler(this.NumericUpDown_Click);
            // 
            // YNumericUpDown
            // 
            this.YNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YNumericUpDown.Enabled = false;
            this.YNumericUpDown.Location = new System.Drawing.Point(238, 202);
            this.YNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YNumericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.YNumericUpDown.Name = "YNumericUpDown";
            this.YNumericUpDown.Size = new System.Drawing.Size(77, 27);
            this.YNumericUpDown.TabIndex = 19;
            this.YNumericUpDown.Click += new System.EventHandler(this.NumericUpDown_Click);
            // 
            // thresholdTrackBar
            // 
            this.thresholdTrackBar.Enabled = false;
            this.thresholdTrackBar.Location = new System.Drawing.Point(10, 238);
            this.thresholdTrackBar.Maximum = 200;
            this.thresholdTrackBar.Minimum = 1;
            this.thresholdTrackBar.Name = "thresholdTrackBar";
            this.thresholdTrackBar.Size = new System.Drawing.Size(305, 80);
            this.thresholdTrackBar.TabIndex = 20;
            this.thresholdTrackBar.Value = 100;
            this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(112, 285);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(96, 21);
            this.thresholdLabel.TabIndex = 22;
            this.thresholdLabel.Text = "缩放 100";
            // 
            // BinarizationtrackBar
            // 
            this.BinarizationtrackBar.Enabled = false;
            this.BinarizationtrackBar.Location = new System.Drawing.Point(325, 238);
            this.BinarizationtrackBar.Maximum = 255;
            this.BinarizationtrackBar.Name = "BinarizationtrackBar";
            this.BinarizationtrackBar.Size = new System.Drawing.Size(305, 80);
            this.BinarizationtrackBar.TabIndex = 23;
            this.BinarizationtrackBar.Value = 128;
            this.BinarizationtrackBar.Scroll += new System.EventHandler(this.BinarizationtrackBar_Scroll);
            // 
            // BinarizationLabel
            // 
            this.BinarizationLabel.AutoSize = true;
            this.BinarizationLabel.Location = new System.Drawing.Point(437, 285);
            this.BinarizationLabel.Name = "BinarizationLabel";
            this.BinarizationLabel.Size = new System.Drawing.Size(117, 21);
            this.BinarizationLabel.TabIndex = 24;
            this.BinarizationLabel.Text = "二值化 128";
            // 
            // ReadGIFButton
            // 
            this.ReadGIFButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReadGIFButton.Enabled = false;
            this.ReadGIFButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReadGIFButton.FlatAppearance.BorderSize = 3;
            this.ReadGIFButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.ReadGIFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReadGIFButton.ForeColor = System.Drawing.Color.Black;
            this.ReadGIFButton.Location = new System.Drawing.Point(314, 57);
            this.ReadGIFButton.Name = "ReadGIFButton";
            this.ReadGIFButton.Size = new System.Drawing.Size(160, 39);
            this.ReadGIFButton.TabIndex = 25;
            this.ReadGIFButton.Text = "GIF(128*64)";
            this.ReadGIFButton.UseVisualStyleBackColor = true;
            this.ReadGIFButton.Click += new System.EventHandler(this.ReadGIFButton_Click);
            // 
            // pixelGrid
            // 
            this.pixelGrid.Columns = 128;
            this.pixelGrid.Controls.Add(this.BitPictureBox);
            this.pixelGrid.Enabled = false;
            this.pixelGrid.Location = new System.Drawing.Point(1, 324);
            this.pixelGrid.Name = "pixelGrid";
            this.pixelGrid.PixelSize = 5;
            this.pixelGrid.Rows = 64;
            this.pixelGrid.Size = new System.Drawing.Size(640, 320);
            this.pixelGrid.TabIndex = 21;
            // 
            // BitPictureBox
            // 
            this.BitPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BitPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BitPictureBox.IsBinarized = true;
            this.BitPictureBox.Location = new System.Drawing.Point(0, 0);
            this.BitPictureBox.Name = "BitPictureBox";
            this.BitPictureBox.Size = new System.Drawing.Size(140, 102);
            this.BitPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BitPictureBox.TabIndex = 0;
            this.BitPictureBox.TabStop = false;
            this.BitPictureBox.Threshold = 128;
            this.BitPictureBox.Visible = false;
            this.BitPictureBox.ZoomFactor = 1F;
            this.BitPictureBox.LocationChanged += new System.EventHandler(this.BitPictureBox_LocationChanged);
            this.BitPictureBox.DoubleClick += new System.EventHandler(this.BitPictureBox_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(643, 646);
            this.Controls.Add(this.ReadGIFButton);
            this.Controls.Add(this.BinarizationLabel);
            this.Controls.Add(this.BinarizationtrackBar);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.pixelGrid);
            this.Controls.Add(this.thresholdTrackBar);
            this.Controls.Add(this.YNumericUpDown);
            this.Controls.Add(this.XNumericUpDown);
            this.Controls.Add(this.FontNameComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FontSizeComboBox);
            this.Controls.Add(this.SendTextToScreenButton);
            this.Controls.Add(this.UserTextBox);
            this.Controls.Add(this.CloseSerialSyncButton);
            this.Controls.Add(this.SerialSyncButton);
            this.Controls.Add(this.DeviceSwitchCheckBox);
            this.Controls.Add(this.ReadPictureButton);
            this.Controls.Add(this.PixelClear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSD1306";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationtrackBar)).EndInit();
            this.pixelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BitPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PixelClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ReadPictureButton;
        private System.Windows.Forms.CheckBox DeviceSwitchCheckBox;
        private System.Windows.Forms.Button SerialSyncButton;
        private System.Windows.Forms.Button CloseSerialSyncButton;
        private System.Windows.Forms.TextBox UserTextBox;
        private System.Windows.Forms.Button SendTextToScreenButton;
        private System.Windows.Forms.ComboBox FontSizeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox FontNameComboBox;
        private System.Windows.Forms.NumericUpDown XNumericUpDown;
        private System.Windows.Forms.NumericUpDown YNumericUpDown;
        private System.Windows.Forms.TrackBar thresholdTrackBar;
        private PixelGridEditor pixelGrid;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.Label BinarizationLabel;
        private BinarizedPictureBox BitPictureBox;
        private System.Windows.Forms.TrackBar BinarizationtrackBar;
        private System.Windows.Forms.Button ReadGIFButton;
    }
}