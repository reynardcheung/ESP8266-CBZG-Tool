using Settings;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ModNamespace
{
    public interface IMod
    {
        NetworkStream stream { get; set; }
        bool ModMain(NetworkStream stream, Form form);
        bool ModExit();
    }

    public class ModProgram : IMod
    {
        private Form1? MainForm;
        public NetworkStream stream { get; set; }
        public bool ModMain(NetworkStream _stream, Form form)
        {
            if (_stream == null || form == null)
            {
                return false;
            }

            stream = _stream;

            MainForm = new Form1(stream);
            MainForm.Owner = form;
            if (Application.OpenForms.Count == 0)
            {
                Application.Run(MainForm); // 启动窗体  
            }
            else
            {
                MainForm.ShowDialog(); // 在已有的应用程序中显示窗体  
            }
            return true;
        }

        public bool ModExit()
        {
            if (MainForm != null)
            {
                MainForm.Close();
            }
            return true;
        }
    }

}
