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
    class Client
    {
        static int g_max_recv_buf = 255;

        public Socket m_socket
        {
            get;
            private set;
        } = null;

        public string m_id
        {
            get;
            private set;
        } = null;

        public IPEndPoint m_ep
        {
            get;
            private set;
        } = null;

        public Client(Socket r_socket)
        {
            m_socket = r_socket;
            m_id = Guid.NewGuid().ToString();
            m_ep = (IPEndPoint)m_socket.RemoteEndPoint;
            m_socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult r_ar)
        {
            try
            {
                m_socket.EndReceive(r_ar);

                byte[] _buf = new byte[g_max_recv_buf];

                int _recv = m_socket.Receive(_buf, _buf.Length, 0);

                if (_recv <= 0)
                {
                    Close();

                    return;
                }

                if (_recv < _buf.Length)
                {
                    Array.Resize<byte>(ref _buf, _recv);
                }

                if (Received != null)
                {
                    Received(this, _buf);
                }

                m_socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, ReceiveCallback, null);
            }
            catch (Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Exception", MessageBoxButtons.OK);
                Close();
            }
        }

        public void Close()
        {
            if (Disconnected != null)
            {
                Disconnected(this);
            }

            m_socket.Close();
            m_socket.Dispose();
        }

        public delegate void ClientReceiveHandler(Client r_client, byte[] r_data);
        public delegate void ClientDisconnectHandler(Client r_client);

        public event ClientReceiveHandler Received;
        public event ClientDisconnectHandler Disconnected;
    }
}
