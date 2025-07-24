namespace Settings
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
            ReadSettings = new Button();
            SaveSettings = new Button();
            SuspendLayout();
            // 
            // ReadSettings
            // 
            ReadSettings.Location = new Point(1, 1);
            ReadSettings.Name = "ReadSettings";
            ReadSettings.Size = new Size(123, 35);
            ReadSettings.TabIndex = 0;
            ReadSettings.Text = "读取设置";
            ReadSettings.UseVisualStyleBackColor = true;
            ReadSettings.Click += ReadSettings_Click;
            // 
            // SaveSettings
            // 
            SaveSettings.Location = new Point(130, 1);
            SaveSettings.Name = "SaveSettings";
            SaveSettings.Size = new Size(143, 35);
            SaveSettings.TabIndex = 1;
            SaveSettings.Text = "保存至Flash";
            SaveSettings.UseVisualStyleBackColor = true;
            SaveSettings.Click += SaveSettings_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(788, 583);
            Controls.Add(SaveSettings);
            Controls.Add(ReadSettings);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "设备设置";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button ReadSettings;
        private Button SaveSettings;
    }
}