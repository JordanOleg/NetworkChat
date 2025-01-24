using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace NetworkLibrary.TCP
{
    public class TCPServerUser
    {
        List<TCPClientUser> clients;
        TcpListener Listener;
        public TCPServerUser(IPEndPoint endPoint)
        {
            Listener = new TcpListener(endPoint);
            clients = new List<TCPClientUser>();
        }
        public async Task ListenerAsync()
        {

            try
            {
                Start();
                while (true)
                {
                    TcpClient client = await Listener.AcceptTcpClientAsync();
                    TCPClientUser tcpClientUser = new TCPClientUser(client, this);
                    clients.Add(tcpClientUser);
                    Task.Run(() => tcpClientUser.ReceiveMessageAsync());

                }
            }
            catch
            {

            }
            finally
            {
                Stop();
            }
        }
        public async Task BroadcastAsync(string? message, TCPClientUser user)
        {
            foreach (TCPClientUser client in clients)
            {
                if (user.Id != client.Id)
                    await client.Writer.WriteLineAsync(message);
            }
        }
        public void Start()
        {
            Listener.Start();
        }
        public void Stop()
        {
            Listener.Stop();
        }
    }
}
