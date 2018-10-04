using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestSocketClient
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class TCPClient
    {
        public TCPClient(string msg)
        {
            ClientThreadStart(msg);
        }

        private static void ClientThreadStart(string msg)
        {
            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31001));
            clientSocket.Send(Encoding.ASCII.GetBytes(msg + "\r\n"));
            clientSocket.Close();
        }

    }
}
