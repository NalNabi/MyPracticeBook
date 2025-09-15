using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsServer
{
    public partial class FormServer : Form
    {
        public enum State
        {
            Default,
            Init,
            Update
        }

        static readonly object _lock = new object();

        private State _state = State.Default;
        private Framework _framework = new Framework();

        public FormServer()
        {
            InitializeComponent();

            Init();
        }

        public bool Init()
        {
            switch(_state)
            {
                case State.Default:
                    {
                        if (_framework.Init(this, "127.0.0.1", 2025) == false)
                        {
                            return false;
                        }

                        _state = State.Init;

                        Chat("[Form] Init");

                        return true;
                    }
            }

            Chat("[Form] Init error !!!");

            return false;
        }

        public bool On()
        {
            switch (_state)
            {
                case State.Init:
                    {
                        _state = State.Update;

                        return true;
                    }
            }

            return false;
        }

        public void Run()
        {
            Chat("[Form] Run start");

            while (_state == State.Update)
            {
                _framework.Run();

                Thread.Sleep(1);

                break;
            }

            Chat("[Form] Run stop");
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            switch (_framework._state)
            {
                case Framework.State.Init:
                    {
                        if (_framework.On() == false)
                        {
                            break;
                        }

                        _state = State.Update;

                        new Thread(() => Run()).Start();

                        break;
                    }

                case Framework.State.Update:
                    {
                        if (_framework.Off() == false)
                        {
                            break;
                        }

                        _state = State.Init;

                        break;
                    }
            }

            switch (_framework._state)
            {
                case Framework.State.Init:
                    {
                        buttonListen.Text = "Listen";

                        Chat("[Form] Closed !!!");

                        break;
                    }

                case Framework.State.Update:
                    {
                        buttonListen.Text = "Close";

                        Chat("[Form] Opened !!!");

                        break;
                    }
            }
        }

        public void Chat(string strChat)
        {
            // inline local func
            void ListBoxAdd(string strData)
            {
                listBoxReceive.Items.Add(strData);

                while (listBoxReceive.Items.Count > 10)
                {
                    listBoxReceive.Items.RemoveAt(0);
                }
            }

            lock (_lock)
            {
                if (listBoxReceive.InvokeRequired)
                {
                    listBoxReceive.Invoke(new MethodInvoker(delegate
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

        
    }
}
