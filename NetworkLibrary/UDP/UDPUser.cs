using NetworkLibrary.AbstractModels;
using NetworkLibrary.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.UDP
{
    public class UDPUser : IUser
    {
        public IPEndPoint ServerIPEndPoint { get; private set; }
        public Socket SocketServer { get; private set; }
        int sendBufferSize;
        int receiveBufferSize;
        public UDPUser(IPEndPoint serverIPEndPoint)
        {
            ServerIPEndPoint = serverIPEndPoint;
            SocketServer = new Socket(SocketType.Dgram, ProtocolType.Udp);
            sendBufferSize = SocketServer.SendBufferSize;
            receiveBufferSize = SocketServer.ReceiveBufferSize;
        }
        public UDPUser(IPEndPoint serverIPEndPoint, Socket socket) : this(serverIPEndPoint)
        {
            ServerIPEndPoint = serverIPEndPoint;
            SocketServer = socket;
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
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await SocketServer.SendAsync(bytes);
        }
        public async Task SendingMessageAsync(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await SocketServer.SendAsync(bytes);
        }
    }
}
