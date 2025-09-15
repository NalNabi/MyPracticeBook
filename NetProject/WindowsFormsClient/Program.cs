using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsClient
{
    static class Program
    {


        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormClient());
        }
    }

    public class Framework
    {
        public enum State
        {
            Default,
            Init,
            Update
        }

        public State _state { get; set; } = State.Default;

        private string _ip = string.Empty;
        private int _port = 0;

        private FormClient _app = null;
        private Socket _socket = null;

        private byte[] _buffer = null;
        private readonly object _lock_send = new object();
        public Framework()
        {
            ;
        }

        public bool Init(FormClient rApp, string strIP, int port)
        {
            switch (_state)
            {
                case State.Default:
                    {
                        _app = rApp;
                        _ip = strIP;
                        _port = port;

                        _state = State.Init;

                        _app.Chat("[Framework] Init");

                        return true;
                    }
            }

            _app.Chat("[Framework] Init error !!!");

            return false;
        }

        public bool On()
        {
            switch (_state)
            {
                case State.Init:
                    {
                        // connect server
                        try
                        {
                            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                            IPEndPoint _ep = new IPEndPoint(IPAddress.Parse(_ip), _port);

                            _socket.Connect(_ep);
                        }
                        catch (SocketException _ex)
                        {
                            _socket = null;

                            _app.Chat("[Framework] Socket error : " + _ex.Message);

                            break;
                        }
                        catch (Exception _ex)
                        {
                            _socket = null;

                            _app.Chat("[Framework] System error : " + _ex.Message);

                            break;
                        }

                        _state = State.Update;

                        // start network thread
                        try
                        {
                            new Thread(()=>Run()).Start();
                        }
                        catch (Exception _ex)
                        {
                            _app.Chat("[Framework] System error : " + _ex.Message);

                            break;
                        }   

                        _app.Chat("[Framework] On");

                        return true;
                    }
            }

            // close network
            try
            {
                if (_socket is null)
                {
                    // pass
                }
                else
                {
                    _socket.Close();
                }
            }
            catch(SocketException _ex)
            {
                _app.Chat("[Framework] Socket error : " + _ex.Message);
            }

            _socket = null;

            _app.Chat("[Framework] On error !!!");

            return false;
        }
        public bool Off()
        {
            switch (_state)
            {
                case State.Update:
                    {
                        // close network
                        try
                        {
                            _socket.Close();
                        }
                        catch (SocketException _ex)
                        {
                            _socket = null;

                            _app.Chat("[Framework] Socket error : " + _ex.Message);
                        }

                        _state = State.Init;

                        _app.Chat("[Framework] Off");

                        return true;
                    }
            }

            _app.Chat("[Framework] Off error !!!");

            return false;
        }

        public void Run()
        {
            _app.Chat("[Framework] Run start");

            while (_state == State.Update)
            {
                Send();

                Thread.Sleep(100);
            }

            _app.Chat("[Framework] Run stop");

            _buffer = null;
        }

        public void Write(string strChat)
        {
            lock(_lock_send)
            {
                byte[] _data = Encoding.Default.GetBytes(strChat);
                _buffer = new byte[sizeof(int) + _data.Length];
                Array.Copy(BitConverter.GetBytes(_data.Length), _buffer, sizeof(int));
                Array.Copy(_data, 0, _buffer, sizeof(int), _data.Length);
            }
        }

        void Send()
        {
            int _send = 0;

            lock (_lock_send)
            {
                if (_buffer is null)
                {
                    return;
                }

                if (_buffer.Length > 0)
                {
                    _send = _socket.Send(_buffer, 0, _buffer.Length, 0);

                    _buffer = null;
                }
            }

            if (_send > 0)
            {
                _app.Chat("[Framework] Send : " + _send + " bytes");
            }
        }
    }
}
