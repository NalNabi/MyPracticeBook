using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsClient
{
    public partial class FormClient : Form
    {
        static string Global_Server_IP = "127.0.0.1";
        static int Global_Server_Port = 2025;

        private static readonly object _lock = new object();

        public enum State
        {
            Default,
            Init,
            Update
        }

        State _state = State.Default;

        private Framework _framework = null;

        public FormClient()
        {
            InitializeComponent();

            Init();
        }

        public bool Init()
        {
            switch (_state)
            {
                case State.Default:
                    {                        
                        _framework = new Framework();

                        _framework.Init(this, Global_Server_IP, Global_Server_Port);

                        _state = State.Init;

                        return true;
                    }
            }

            return false;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            switch (_framework._state)
            {
                case Framework.State.Init:
                    {
                        _framework.On();

                        break;
                    }

                case Framework.State.Update:
                    {
                        _framework.Off();

                        break;
                    }
            }
        }

        public void Chat(string strChat)
        {
            void ListBoxAdd(string strData)
            {
                listBoxChat.Items.Add(strData);

                while (listBoxChat.Items.Count > 10)
                {
                    listBoxChat.Items.RemoveAt(10);
                }
            }

            lock (_lock)
            {
                if (listBoxChat.InvokeRequired)
                {
                    listBoxChat.Invoke(new MethodInvoker(delegate
                    {
                        ListBoxAdd(strChat);
                    }));
                }
                else
                {
                    ListBoxAdd(strChat);
                }
            }
        }

        private void ChatSend()
        {
            if (textChat.Text.Length > 0)
            {
                _framework.Write(textChat.Text);

                textChat.Clear();
            }
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {
            ChatSend();
        }

        private void FormClient_KeyDown(object sender, KeyEventArgs e)
        {
            Chat("Key event !!");
        }

        private void textChat_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ChatSend();
            }

            return ;
        }
    }
}
