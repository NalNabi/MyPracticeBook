using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDelegateClient
{
    public partial class FormClient : Form
    {
        private Client _client = null;

        static string g_ip = "127.0.0.1";
        static int g_port = 10099;

        public FormClient()
        {
            InitializeComponent();

            _client = new Client(g_ip, g_port);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            _client.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _client.Stop();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                byte[] _data = Encoding.Default.GetBytes(richTextBoxMessage.Text);

                if (_data.Length < 0)
                {
                    return ;
                }

                _client.Send(_data);
            });
        }
    }
}
