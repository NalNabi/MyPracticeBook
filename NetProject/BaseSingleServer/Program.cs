using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BaseSingleCS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                _socket.Bind(new IPEndPoint(0, 2025));
                _socket.Listen(0);

                Socket _accpet = _socket.Accept();

                byte[] _buffer = Encoding.Default.GetBytes("Hello User");
                _accpet.Send(_buffer, 0, _buffer.Length, 0);

                _buffer = new byte[255];
                int _recv = _accpet.Receive(_buffer, 0, _buffer.Length, 0);
                Array.Resize(ref _buffer, _recv);

                Console.WriteLine("Received : {0}", Encoding.Default.GetString(_buffer));
                Console.Read();

                _socket.Close();
                _accpet.Close();
            }

            /*
            // Bind socket create
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(0, 1234));
            _socket.Listen(100);

            // User socket create
            Socket _accepted = _socket.Accept();
            _buffer = new byte[_accepted.SendBufferSize];
            int _bytesRead = _accepted.Receive(_buffer);
            byte[] _formatted = new byte[_bytesRead];
            for (int _i = 0; _i < _bytesRead; _i++)
            {
                _formatted[_i] = _buffer[_i];
            }
            string _strData = Encoding.ASCII.GetString(_formatted);
            Console.Write(_strData + "\r\n");
            Console.Read();

            // User socket delete
            _accepted.Close();

            // Bind socket delete
            _socket.Close();
            */
        }
    }
}
