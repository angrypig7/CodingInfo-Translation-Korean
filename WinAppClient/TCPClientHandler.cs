using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WinAppClient
{
    class TCPClientHandler
    {
        IPEndPoint clientAddress;
        IPEndPoint serverAddress;

        TcpClient client;

        NetworkStream networkStream;

        public TCPClientHandler()
        {
            clientAddress = new IPEndPoint(IPAddress.Parse("127.0.0.0"), 0); //수정 필요
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.0"), 0); //수정 필요

            client = new TcpClient(clientAddress);
            client.Connect(serverAddress);

            networkStream = client.GetStream();
        }

        public TCPClientHandler(string clientIP, string serverIP)
        {
            clientAddress = new IPEndPoint(IPAddress.Parse(clientIP), 0); //수정 필요
            serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), 0); //수정 필요

            client = new TcpClient(clientAddress);
            client.Connect(serverAddress);

            networkStream = client.GetStream();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Default.GetBytes(message);
            networkStream.Write(data, 0, data.Length);
        }
    }
}
