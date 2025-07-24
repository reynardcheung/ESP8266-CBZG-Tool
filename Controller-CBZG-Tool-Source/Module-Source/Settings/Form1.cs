using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Settings
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private FlowLayoutPanel? mainPanel;

        public Form1(NetworkStream _stream)
        {
            InitializeComponent();
            stream = _stream;
            InitializeCustomComponents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeCustomComponents()
        {
            mainPanel = new FlowLayoutPanel
            {
                Name = "mainPanel",
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = SystemColors.Window,
                Padding = new Padding(15)
            };

            var toolPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = SystemColors.Control
            };

            Controls.AddRange(new Control[] { mainPanel, toolPanel });
        }

        public void LoadJsonConfiguration(string json)
        {
            if (mainPanel == null)
            {
                return;
            }
            mainPanel.SuspendLayout();
            mainPanel.Controls.Clear();

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    foreach (JsonProperty section in root.EnumerateObject())
                    {
                        CreateSectionPanel(section);
                    }
                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON解析错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateGroupBoxWidths();
            mainPanel.ResumeLayout(true);

        }

        private void CreateSectionPanel(JsonProperty section)
        {
            var groupBox = new GroupBox
            {
                Text = FormatSectionName(section.Name),
                AutoSize = true,
                MinimumSize = new Size(400, 50),
                Margin = new Padding(5, 10, 5, 15),
                Font = new Font("微软雅黑", 9.75f, FontStyle.Bold)
            };

            var contentPanel = new TableLayoutPanel
            {
                ColumnCount = 4,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            if (mainPanel == null)
            {
                return;
            }

            ConfigureColumns(contentPanel);
            AddSettingsToPanel(contentPanel, section.Value);
            AddVisualSeparator(groupBox);

            groupBox.Controls.Add(contentPanel);
            mainPanel.Controls.Add(groupBox);
        }

        private void ConfigureColumns(TableLayoutPanel panel)
        {
            panel.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            }
        }

        private void AddSettingsToPanel(TableLayoutPanel panel, JsonElement sectionValue)
        {
            int rowIndex = 0;
            foreach (JsonProperty setting in sectionValue.EnumerateObject())
            {
                AddSettingControl(panel, setting, ref rowIndex);
            }
        }

        private void AddSettingControl(TableLayoutPanel panel, JsonProperty setting, ref int rowIndex)
        {
            int actualRow = rowIndex / 2;
            if (rowIndex % 2 == 0) panel.RowCount++;

            var lbl = new Label
            {
                Text = $"{setting.Name}:",
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                AutoSize = true,
                Margin = new Padding(0, 5, 5, 5),
                Font = new Font("微软雅黑", 9f)
            };

            var txt = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Width = 150,
                Margin = new Padding(0, 5, 15, 5),
                Tag = GetValueType(setting.Value),
                Font = new Font("微软雅黑", 9f)
            };

            SetControlValue(txt, setting.Value);

            if (txt.Tag.ToString() == "number")
            {
                txt.KeyPress += (s, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
                        e.Handled = true;
                };
            }

            int col = (rowIndex % 2) * 2;
            panel.Controls.Add(lbl, col, actualRow);
            panel.Controls.Add(txt, col + 1, actualRow);
            rowIndex++;
        }

        private void SetControlValue(TextBox txt, JsonElement value)
        {
            if (value.ValueKind == JsonValueKind.Number)
            {
                if (value.TryGetInt32(out int intVal))
                    txt.Text = intVal.ToString();
                else if (value.TryGetDouble(out double dblVal))
                    txt.Text = dblVal.ToString("F2");
            }
            else if (value.ValueKind == JsonValueKind.String)
            {
                txt.Text = value.GetString() ?? "";
            }
        }

        private string GetValueType(JsonElement value)
        {
            return value.ValueKind == JsonValueKind.Number ? "number" : "string";
        }

        private string FormatSectionName(string name)
        {
            return name.Replace('_', ' ').ToUpper();
        }

        private void AddVisualSeparator(Control container)
        {
            var separator = new Label
            {
                Height = 1,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(200, 200, 200)
            };
            container.Controls.Add(separator);
        }

        private void UpdateGroupBoxWidths()
        {
            if (mainPanel == null)
            {
                return;
            }
            int scrollbarWidth = mainPanel.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;
            int targetWidth = mainPanel.ClientSize.Width - scrollbarWidth - 25;

            foreach (Control ctrl in mainPanel.Controls)
            {
                if (ctrl is GroupBox gb)
                {
                    gb.Width = Math.Max(targetWidth, gb.MinimumSize.Width);
                }
            }
        }

        private string GetCurrentConfig()
        {
            if (mainPanel == null)
            {
                return "{}";
            }
            var config = new JsonObject();

            foreach (GroupBox groupBox in mainPanel.Controls)
            {
                var section = new JsonObject();
                TableLayoutPanel? panel = null;

                foreach (Control ctrl in groupBox.Controls)
                {
                    if (ctrl is TableLayoutPanel tlp)
                    {
                        panel = tlp;
                        break;
                    }
                }

                if (panel == null)
                {
                    continue;
                }

                for (int i = 0; i < panel.RowCount * 2; i++)
                {
                    int row = i / 2;
                    int col = (i % 2) * 2;

                    var lbl = panel.GetControlFromPosition(col, row) as Label;
                    var txt = panel.GetControlFromPosition(col + 1, row) as TextBox;

                    if (lbl != null && txt != null)
                    {
                        if (txt.Tag?.ToString() == "number")
                        {
                            if (long.TryParse(txt.Text, out long num))
                                section[lbl.Text.TrimEnd(':')] = num;
                        }
                        else
                        {
                            section[lbl.Text.TrimEnd(':')] = txt.Text;
                        }
                    }
                }

                config[groupBox.Text.Replace(" ", "_").ToLower()] = section;
            }

            return config.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
        }

        void LockControls(bool isLock)
        {
            this.ReadSettings.Enabled = isLock;
            this.SaveSettings.Enabled = isLock;
        }

        private async void ReadSetting(Stream stream)
        {
            string Command = "{ESPREADINFO}";
            byte[] CommandBytes = Encoding.UTF8.GetBytes(Command);
            byte[] ReadBytes = new byte[2048];
            int ReadNum = 0;
            LockControls(false);
            await stream.WriteAsync(CommandBytes, 0, CommandBytes.Length);
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(10000);  

                try
                {
                    ReadNum = await Task.Run(() =>
                    {
                        return stream.ReadAsync(ReadBytes, 0, ReadBytes.Length, cts.Token).Result;
                    }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("错误: 操作超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //throw new TimeoutException("操作超时。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"错误: 读取数据时发生错误{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //throw new Exception("读取数据时发生错误。", ex);
                }
            }
            string JsonStr = Encoding.UTF8.GetString(ReadBytes, 0, ReadNum);
            LoadJsonConfiguration(JsonStr);
            LockControls(true);
        }

        private async Task<bool> SaveSettingsToDev()
        {
            string Command = "{ESPSETTING}";
            byte[] CommandBytes = Encoding.UTF8.GetBytes(Command);
            byte[] ReadBytes = new byte[1];
            string SettingConfigStr = GetCurrentConfig();

            await stream.WriteAsync(CommandBytes, 0, CommandBytes.Length);
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(10000);

                try
                {
                    await Task.Run(() =>
                    {
                        return stream.ReadAsync(ReadBytes, 0, ReadBytes.Length, cts.Token).Result;
                    }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("错误: 操作超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    //throw new TimeoutException("操作超时。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"错误: 读取数据时发生错误{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    //throw new Exception("读取数据时发生错误。", ex);
                }
            }

            if (ReadBytes[0] != 'y')
            {
                return false;
            }

            byte[] ConfigBytes = Encoding.UTF8.GetBytes(SettingConfigStr);
            await stream.WriteAsync(ConfigBytes, 0, ConfigBytes.Length);
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(10000);

                try
                {
                    await Task.Run(() =>
                    {
                        return stream.ReadAsync(ReadBytes, 0, ReadBytes.Length, cts.Token).Result;
                    }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("错误: 操作超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    //throw new TimeoutException("操作超时。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"错误: 读取数据时发生错误{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    //throw new Exception("读取数据时发生错误。", ex);
                }
            }

            if (ReadBytes[0] != 'y')
            {
                return false;
            }

            return true;
        }

        private void ReadSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"警告: 请详细阅读操作手册后修改参数", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ReadSetting(stream);
        }

        private async void SaveSettings_Click_1(object sender, EventArgs e)
        {
            LockControls(false);
            bool result = await SaveSettingsToDev();
            if(result)
            {
                MessageBox.Show("信息：已保存至Flash，设备重启后生效", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("错误: 保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LockControls(true);
        }
    }
}
