using System.Net.Sockets;
using System.Text;

namespace TamoCRM.Core.Commons
{
    public class SocketClient
    {
        public string Host { get;set; }
        public int Port { get;set; }
        
        public SocketClient(string host, int port)
        {
            Host = host;
            Port = port;
        }
        
        public void SendMessage(string msg)
        {
            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(Host, Port);
            clientSocket.Send(Encoding.ASCII.GetBytes(msg + "\r\n"));
            clientSocket.Close();
        }
    }
}
