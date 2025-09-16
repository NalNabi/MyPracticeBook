using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsDelegateServer
{
    public partial class FormServer : Form
    {
        static string g_ip = "127.0.0.1";
        static int g_port = 10099;
        
        private FormClient m_client = null;

        public enum list_client_index : int
        {
            EP = 0,
            ID,
            Text,
            Update
        }

        private Server _server = null;

        public FormServer()
        {
            InitializeComponent();

            try 
            {
                _server = new Server(g_ip, g_port);
                _server.Accepted += new Server.SocketAcceptHandler(EventAccepted);
                _server.Start();
            }
            catch(Exception _exp)
            {
                MessageBox.Show(_exp.Message, "Exception", MessageBoxButtons.OK);
                
                Application.Exit();
            }
        }

        void EventAccepted(Socket r_socket)
        {
            MessageBox.Show("Socket accpeted", "Notice", MessageBoxButtons.OK);

            Client _client = new Client(r_socket);
            _client.Received += new Client.ClientReceiveHandler(EventReceived);
            _client.Disconnected += new Client.ClientDisconnectHandler(EventDisconnected);

            Invoke((MethodInvoker)delegate
           {
               ListViewItem _item = new ListViewItem();
               _item.Text = _client.m_ep.ToString();
               _item.SubItems.Add(_client.m_id);
               _item.SubItems.Add("-");
               _item.SubItems.Add("-");
               _item.Tag = _client;

               listViewClient.Items.Add(_item);
           });
        }

        void EventReceived(Client r_client, byte[] r_data)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int _i = 0; _i < listViewClient.Items.Count; ++_i)
                {
                    Client _client = listViewClient.Items[_i].Tag as Client;

                    if (Math.Equals(_client.m_id, r_client.m_id))
                    {
                        listViewClient.Items[_i].SubItems[((int)list_client_index.Text)].Text = Encoding.Default.GetString(r_data);
                        listViewClient.Items[_i].SubItems[((int)list_client_index.Update)].Text = DateTime.Now.ToString();

                        break;
                    }
                }
            });
        }
        void EventDisconnected(Client r_client)
        {
            Invoke((MethodInvoker)delegate
               {
                   for (int _i = 0; _i < listViewClient.Items.Count; ++ _i)
                   {
                       Client _client = listViewClient.Items[_i].Tag as Client;

                       if (Math.Equals(_client.m_id, r_client.m_id))
                       {
                           listViewClient.Items.RemoveAt(_i);

                           break;
                       }
                   }
               });
        }

        private void listViewClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    ListViewHitTestInfo _hit = listViewClient.HitTest(e.X, e.Y);

                    if (_hit.SubItem is null)
                    {
                        MessageBox.Show("No hit", "Error", MessageBoxButtons.OK);

                        return;
                    }

                    if (_hit.Item is null)
                    {
                        MessageBox.Show("No hit", "Error", MessageBoxButtons.OK);

                        return;
                    }

                    ListViewItem _item = _hit.Item;

                    if (m_client != null)
                    {
                        m_client.Close();
                    }

                    for (int _i = 0; _i < listViewClient.Items.Count; ++_i)
                    {
                        if (_i != _item.Index)
                        {
                            continue;
                        }

                        Client _client = listViewClient.Items[_i].Tag as Client;

                        m_client = new FormClient(_client.m_ep, _client.m_id, listViewClient.Items[_i].SubItems[((int)list_client_index.Text)].Text);
                        m_client.Show();
                        
                        return;
                    }

                    MessageBox.Show("No client", "Error", MessageBoxButtons.OK);
                }
                catch (Exception _exp)
                {
                    MessageBox.Show(_exp.Message, "Exception", MessageBoxButtons.OK);

                    return;
                }
            });
        }
    }
}