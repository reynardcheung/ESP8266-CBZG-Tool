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
        public NetworkStream stream { get; set; }
        public Form MainForm { get; set; }
        public bool ModMain(NetworkStream _stream,Form form)
        {
            if(_stream == null || form == null)
            {
                return false;
            }
            stream = _stream;
            MainForm = form;
            UserClass _UserClass = new UserClass(this);
            _UserClass.UserFunction();
            ModExit();
            return true;
        }

        public bool ModExit()
        {
            UserClass.form.Dispose();
            return true;
        }
    }

    public class UserClass
    {
        public static Form form;
        ModProgram SysParam;
        public UserClass(ModProgram Param)
        {
            SysParam = Param;
        }

        public void UserFunction()
        {
            form = new GPIOManager.Form1(SysParam.stream, SysParam);
            form.Owner = SysParam.MainForm;
            form.ShowDialog();
        }
    }
}
