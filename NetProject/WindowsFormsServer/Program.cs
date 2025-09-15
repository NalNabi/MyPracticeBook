using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsServer
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
            Application.Run(new FormServer());
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

        private static int _max_buf_recv = 255;

        public State _state { get; set; } = State.Default;

        private string _ip = string.Empty;
        private int _port = 0;

        private FormServer _app = null;
        private Socket _listen = null;

        public Framework()
        {
            ;
        }

        public bool Init(FormServer rApp, string strIP, int port)
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
                        try
                        {
                            _listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            _listen.Bind(new IPEndPoint(IPAddress.Parse(_ip), _port));
                            _listen.Listen(0);
                        }
                        catch (SocketException _ex)
                        {
                            _listen = null;

                            _app.Chat("Socket error : " + _ex.Message);

                            break;
                        }

                        _state = State.Update;

                        _app.Chat("On");

                        return true;
                    }
            }

            if (_listen is null)
            {
                // pass
            }
            else
            {
                _listen.Close();
            }

            _listen = null;

            _app.Chat("On error !!!");

            return false;
        }

        public bool Off()
        {
            switch (_state)
            {
                case State.Update:
                    {
                        try
                        {
                            _listen.Close();
                        }
                        catch (SocketException _ex)
                        {
                            _app.Chat("Socket error : " + _ex.Message);
                        }
                        finally
                        {
                            _listen = null;
                        }

                        _state = State.Init;

                        _app.Chat("Off");

                        return true;
                    }
            }

            _app.Chat("Off error !!!");

            return false;
        }

        public void Run()
        {
            _app.Chat("[Framework] Run start");

            while (_state == State.Update)
            {
                // on network
                try
                {
                    Socket _accpet = _listen.Accept();

                    _app.Chat("Accept : " + _accpet.RemoteEndPoint.ToString());

                    new Thread(()=>Network(_accpet)).Start(); // every try accpet new user
                }
                catch (SocketException _ex)
                {
                    _app.Chat("Socket error : " + _ex.Message);

                    break;
                }

                Thread.Sleep(1);
            }

            _app.Chat("[Framework] Run stop");
        }

        public void Network(Socket _accpet)
        {
            // on network
            try
            {
                int _recv = 0;
                byte[] _buffer = new byte[Math.Min(_accpet.ReceiveBufferSize, _max_buf_recv)];
                while (_state == State.Update)
                {
                    Array.Clear(_buffer, 0, _buffer.Length);

                    _recv = _accpet.Receive(_buffer, 0, _buffer.Length, 0);

                    int _read_index = 0;
                    int _left_data = _recv;
                    while (_left_data > 0)
                    {
                        int _sz_szdata = sizeof(int);
                        if (_recv - _sz_szdata <= 0) // sizeof(int) : the number of data size
                        {
                            _app.Chat("Socket recv error");

                            break;
                        }

                        // get data size
                        byte[] _szbuf = new byte[_sz_szdata];
                        Array.Copy(_buffer, _szbuf, _sz_szdata);

                        _read_index += _sz_szdata;
                        _left_data -= _sz_szdata;

                        int _szdata = BitConverter.ToInt32(_szbuf, 0);

                        _app.Chat("[Framework] Recv start data size : " + _szdata);

                        MemoryStream _memory = new MemoryStream();
                        if (_left_data >= _szdata)
                        {
                            // complete data
                            _memory.Write(_buffer, _read_index, _szdata);

                            _read_index += _szdata;
                            _left_data -= _szdata;

                            _app.Chat("[Framework] Recv complete 01");
                            _app.Message(Encoding.Default.GetString(_memory.ToArray()));

                            _memory.Close();


                            continue ;
                        }

                        // get first data
                        _memory.Write(_buffer, _read_index, _left_data);

                        _szdata -= _left_data;
                        _left_data = 0;

                        // get second data
                        while (_szdata > 0)
                        {
                            Array.Clear(_buffer, 0, _recv);

                            int _szrecv = Math.Min(_buffer.Length, _szdata);

                            _recv = _accpet.Receive(_buffer, 0, _szrecv, 0);

                            _memory.Write(_buffer, 0, _recv);

                            _szdata -= _recv;

                            _app.Chat("[Framework] Recv sec data : " + _recv + " / left : " + _szdata);
                        }

                        // complete data
                        _app.Chat("[Framework] Recv complete 02");
                        _app.Message(Encoding.Default.GetString(_memory.ToArray()));

                        _memory.Close();
                    }
                }
            }
            catch (SocketException _ex)
            {
                _app.Chat("Socket error : " + _ex.Message);
            }
            catch (Exception _ex)
            {
                _app.Chat("System error : " + _ex.Message);
            }

            // off socket
            try
            {
                _accpet.Close();
            }
            catch (SocketException _ex)
            {
                _app.Chat("Socket error : " + _ex.Message);
            }
            finally
            {
                _accpet = null;
            }
        }
    }
}
