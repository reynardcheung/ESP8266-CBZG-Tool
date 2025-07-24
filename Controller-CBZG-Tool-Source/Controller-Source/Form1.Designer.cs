namespace WirelessDownloadTool3._5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AddressComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            PortTextBox = new TextBox();
            Listener = new Button();
            SuspendLayout();
            // 
            // AddressComboBox
            // 
            AddressComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AddressComboBox.FormattingEnabled = true;
            AddressComboBox.Location = new Point(118, 9);
            AddressComboBox.MaxLength = 15;
            AddressComboBox.Name = "AddressComboBox";
            AddressComboBox.Size = new Size(201, 36);
            AddressComboBox.TabIndex = 0;
            AddressComboBox.DropDown += ComboBox1_DropDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold);
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(115, 33);
            label1.TabIndex = 1;
            label1.Text = "监听地址";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold);
            label2.Location = new Point(3, 48);
            label2.Name = "label2";
            label2.Size = new Size(115, 33);
            label2.TabIndex = 2;
            label2.Text = "监听端口";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(118, 50);
            PortTextBox.MaxLength = 5;
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(201, 34);
            PortTextBox.TabIndex = 3;
            PortTextBox.WordWrap = false;
            PortTextBox.TextChanged += TextBox1_TextChanged;
            PortTextBox.KeyPress += TextBox1_KeyPress;
            // 
            // Listener
            // 
            Listener.Location = new Point(12, 90);
            Listener.Name = "Listener";
            Listener.Size = new Size(307, 37);
            Listener.TabIndex = 4;
            Listener.Text = "开启监听";
            Listener.UseVisualStyleBackColor = true;
            Listener.Click += Listener_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(327, 138);
            Controls.Add(Listener);
            Controls.Add(PortTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(AddressComboBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox AddressComboBox;
        private Label label1;
        private Label label2;
        private TextBox PortTextBox;
        private Button Listener;
    }
}