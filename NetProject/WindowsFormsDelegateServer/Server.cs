using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WindowsFormsDelegateServer
{
    public class Server
    {
        private string m_ip = null;
        private int m_port = 0;

        private Socket m_listen = null;

        public Server(string r_ip, int r_port)
        {
            m_ip = r_ip;
            m_port = r_port; 
            
            m_listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            try
            {
                m_listen.Bind(new IPEndPoint(IPAddress.Parse(m_ip), m_port));
                m_listen.Listen(0);
                m_listen.BeginAccept(CallbackAccept, null);
            }
            catch (SocketException _exp)
            {
                MessageBox.Show(_exp.Message);

                Application.Exit();
            }
        }

        public void Stop()
        {
            if (m_listen is null)
            {
                return;
            }

            m_listen.Close();
            m_listen.Dispose();

            m_listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void CallbackAccept(IAsyncResult r_ar)
        {
            try
            {
                Socket _accept = m_listen.EndAccept(r_ar);

                if (Accepted != null)
                {
                    Accepted(_accept);
                }
                
                m_listen.BeginAccept(CallbackAccept, null);
            }
            catch (Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public delegate void SocketAcceptHandler(Socket r_accept);
        public event SocketAcceptHandler Accepted;
    }
}
