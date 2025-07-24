namespace WirelessSTM32ISP
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DownloadProgramButton = new System.Windows.Forms.Button();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.DownloadProgress = new System.Windows.Forms.ProgressBar();
            this.SelectBinButton = new System.Windows.Forms.Button();
            this.DownloadStopButton = new System.Windows.Forms.Button();
            this.EraseFlashButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "BIN Files (*.bin)|*.bin";
            // 
            // FilePath
            // 
            this.FilePath.BackColor = System.Drawing.Color.White;
            this.FilePath.ForeColor = System.Drawing.Color.Silver;
            this.FilePath.Location = new System.Drawing.Point(106, 7);
            this.FilePath.Margin = new System.Windows.Forms.Padding(2);
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Size = new System.Drawing.Size(246, 31);
            this.FilePath.TabIndex = 0;
            this.FilePath.Text = "请拖入文件或点击右侧按钮";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bin文件：";
            // 
            // DownloadProgramButton
            // 
            this.DownloadProgramButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.DownloadProgramButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.HotPink;
            this.DownloadProgramButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadProgramButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownloadProgramButton.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.DownloadProgramButton.Location = new System.Drawing.Point(9, 42);
            this.DownloadProgramButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadProgramButton.Name = "DownloadProgramButton";
            this.DownloadProgramButton.Size = new System.Drawing.Size(241, 35);
            this.DownloadProgramButton.TabIndex = 2;
            this.DownloadProgramButton.Text = "下载程序";
            this.DownloadProgramButton.UseVisualStyleBackColor = true;
            this.DownloadProgramButton.Click += new System.EventHandler(this.DownloadProgramButton_Click);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.BackColor = System.Drawing.Color.White;
            this.StatusTextBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusTextBox.Location = new System.Drawing.Point(9, 81);
            this.StatusTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(389, 31);
            this.StatusTextBox.TabIndex = 3;
            this.StatusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DownloadProgress
            // 
            this.DownloadProgress.Location = new System.Drawing.Point(9, 116);
            this.DownloadProgress.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadProgress.Name = "DownloadProgress";
            this.DownloadProgress.Size = new System.Drawing.Size(389, 16);
            this.DownloadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.DownloadProgress.TabIndex = 4;
            // 
            // SelectBinButton
            // 
            this.SelectBinButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.SelectBinButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.HotPink;
            this.SelectBinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectBinButton.Font = new System.Drawing.Font("宋体", 3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectBinButton.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.SelectBinButton.Location = new System.Drawing.Point(358, 8);
            this.SelectBinButton.Margin = new System.Windows.Forms.Padding(2);
            this.SelectBinButton.Name = "SelectBinButton";
            this.SelectBinButton.Size = new System.Drawing.Size(40, 30);
            this.SelectBinButton.TabIndex = 5;
            this.SelectBinButton.Text = "···";
            this.SelectBinButton.UseVisualStyleBackColor = true;
            this.SelectBinButton.Click += new System.EventHandler(this.SelectBinButton_Click);
            // 
            // DownloadStopButton
            // 
            this.DownloadStopButton.Enabled = false;
            this.DownloadStopButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.DownloadStopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.HotPink;
            this.DownloadStopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadStopButton.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.DownloadStopButton.Location = new System.Drawing.Point(328, 42);
            this.DownloadStopButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadStopButton.Name = "DownloadStopButton";
            this.DownloadStopButton.Size = new System.Drawing.Size(70, 35);
            this.DownloadStopButton.TabIndex = 6;
            this.DownloadStopButton.Text = "停止";
            this.DownloadStopButton.UseVisualStyleBackColor = true;
            this.DownloadStopButton.Click += new System.EventHandler(this.DownloadStopButton_Click);
            // 
            // EraseFlashButton
            // 
            this.EraseFlashButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.EraseFlashButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.HotPink;
            this.EraseFlashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraseFlashButton.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.EraseFlashButton.Location = new System.Drawing.Point(254, 42);
            this.EraseFlashButton.Margin = new System.Windows.Forms.Padding(2);
            this.EraseFlashButton.Name = "EraseFlashButton";
            this.EraseFlashButton.Size = new System.Drawing.Size(70, 35);
            this.EraseFlashButton.TabIndex = 7;
            this.EraseFlashButton.Text = "擦除";
            this.EraseFlashButton.UseVisualStyleBackColor = true;
            this.EraseFlashButton.Click += new System.EventHandler(this.EraseFlashButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.DownloadProgramButton;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 136);
            this.Controls.Add(this.EraseFlashButton);
            this.Controls.Add(this.DownloadStopButton);
            this.Controls.Add(this.SelectBinButton);
            this.Controls.Add(this.DownloadProgress);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.DownloadProgramButton);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STM32Prog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DownloadProgramButton;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.ProgressBar DownloadProgress;
        private System.Windows.Forms.Button SelectBinButton;
        private System.Windows.Forms.Button DownloadStopButton;
        private System.Windows.Forms.Button EraseFlashButton;
    }
}