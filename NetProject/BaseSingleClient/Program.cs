using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BaseSingleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint _ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2025);
                _socket.Connect(_ep);

                Console.Write("Enter Message : ");
                string _message = Console.ReadLine();
                byte[] _buffer = Encoding.Default.GetBytes(_message);
                _socket.Send(_buffer, 0, _buffer.Length, 0);

                _buffer = new byte[255];
                int _recv = _socket.Receive(_buffer, 0, _buffer.Length, 0);

                Array.Resize(ref _buffer, _recv);

                Console.WriteLine("Received {0}", Encoding.Default.GetString(_buffer));
                Console.Read();

                _socket.Close();
            }

            /*
            using (_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

                try
                {
                    _socket.Connect(_endPoint);
                }
                catch
                {
                    Console.WriteLine("Unable to connect");
                }

                Console.Write("Enter Text : ");
                string _text = Console.ReadLine();
                byte[] _data = Encoding.ASCII.GetBytes(_text);

                _socket.Send(_data);
                Console.WriteLine("Data send !!!");
                Console.WriteLine("Press any key to continue...");
                Console.Read();

                _socket.Close();
            }
            */
        }
    }
}
