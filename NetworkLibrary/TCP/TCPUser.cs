using NetworkLibrary.AbstractModels;
using NetworkLibrary.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.TCP
{
    public class TCPUser : IUser
    {
        public IPEndPoint ServerIPEndPoint { get; private set; }
        public Socket SocketServer { get; private set; }
        int sendBufferSize;
        int receiveBufferSize;
        StreamReader reader;
        StreamWriter writer;
        void Initialize()
        {   
            NetworkStream networkStream = new NetworkStream(SocketServer);
            reader = new StreamReader(networkStream);
            writer = new StreamWriter(networkStream);
            sendBufferSize = SocketServer.SendBufferSize;
            receiveBufferSize = SocketServer.ReceiveBufferSize;
        }
        public TCPUser(IPEndPoint serverIPEndPoint)
        {
            SocketServer = new Socket(SocketType.Stream, ProtocolType.Tcp);
            ServerIPEndPoint = serverIPEndPoint;
            Initialize();
        }
        public TCPUser(IPEndPoint serverIPEndPoint, Socket socketServer)
        {
            ServerIPEndPoint = serverIPEndPoint;
            Initialize();
            this.SocketServer = socketServer; 
        }

        public async Task ListenerMessageAsync()
        {
            byte[] buffer = new byte[receiveBufferSize];
            int index = await SocketServer.ReceiveAsync(buffer);
            string message = Encoding.UTF8.GetString(buffer, 0, index);
            UIConsole.Print(message);
        }

        public async Task SendingMessageAsync(char[] message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        public async Task SendingMessageAsync(string message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }
    }
}
