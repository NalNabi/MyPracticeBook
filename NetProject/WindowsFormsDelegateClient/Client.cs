using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WindowsFormsDelegateClient
{
    class Client
    {
        private string m_ip;
        private int m_port;
        private Socket m_socket = null;
        private bool m_connected = false;

        public Client(string r_ip, int r_port)
        {
            try
            {
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                m_ip = r_ip;
                m_port = r_port;
            }
            catch (Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Error", MessageBoxButtons.OK);

                Application.Exit();
            }
        }

        public void Start()
        {
            try
            {
                m_socket.Connect(IPAddress.Parse(m_ip), m_port);

                m_connected = true;
            }
            catch (Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Error", MessageBoxButtons.OK);

                Application.Exit();
            }
        }

        public void Stop()
        {
            try
            {
                if (m_connected)
                {
                    m_socket.Close();

                    m_connected = false;
                }

                m_socket.Dispose();

                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Error", MessageBoxButtons.OK);

                Application.Exit();
            }
        }

        public void Send(byte[] r_data)
        {
            if (m_connected == false)
            {
                MessageBox.Show("Is not connected", "Error", MessageBoxButtons.OK);
            }

            m_socket.Send(r_data, 0, r_data.Length, 0);
        }
    }
}
