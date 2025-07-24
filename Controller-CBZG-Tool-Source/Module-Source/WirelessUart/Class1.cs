using System;
using System.Net.Sockets;
using System.Windows.Forms;
using WirelessUart;

namespace ModNamespace
{
    public interface IMod
    {
        NetworkStream stream { get; set; }
        bool ModMain(NetworkStream stream, Form form);
        void  ModExit();
    }

    public class ModProgram : IMod
    {
        private Form1 MainForm;
        public NetworkStream stream { get; set; }
        public bool ModMain(NetworkStream _stream,Form form)
        {
            if(_stream == null || form == null)
            {
                return false;
            }
            stream = _stream;
            MainForm = new Form1(stream,this);
            MainForm.Owner = form;
            MainForm.ShowDialog(); // 在已有的应用程序中显示窗体
            ModExit();
            return true;
        }

        public void ModExit()
        {
            MainForm.Dispose();
            stream = null;
        }
    }

}
