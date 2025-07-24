using System.Net.Sockets;
using System.Text;
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
        public bool ModMain(NetworkStream _stream,Form form)
        {
            if(_stream == null || form == null)
            {
                return false;
            }
            stream = _stream;

            User_Class user_obj = new User_Class(this);
            user_obj.UserFunction();
            return true;
        }

        public bool ModExit()
        {
            return true;
        }
    }

    public class User_Class
    {
        ModProgram SysParam;
        public User_Class(ModProgram Param) 
        {
            SysParam = Param;
        }

        public async void UserFunction()
        {
            byte[] bytes = Encoding.ASCII.GetBytes("{ESPRESTART}");
            try 
            {
                await SysParam.stream.WriteAsync(bytes,0, bytes.Length);
                MessageBox.Show($"模块提示: 已发送重启指令，请刷新再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show($"错误: 网络连接异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
