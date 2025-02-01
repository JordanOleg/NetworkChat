using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary.AbstractModels;
using NetworkLibrary.TCP;

namespace NetworkLibrary.UDP
{
    public class UDPUserListener : IUserListener
    {
        public UdpClient UdpClient { get; private set; }
        public UDPServer Server { get; private set; }
        public string Name { get; private set; }            
        public Guid Id { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public UDPUserListener(UdpClient udpClient, UDPServer server, IPEndPoint endPoint)
        {
            this.UdpClient = udpClient;
            Server = server;
            Id = Guid.NewGuid();
            EndPoint = endPoint;
        }
        public void Close()
        {
            UdpClient.Close();
        }

        public async Task Disconect()
        {
            await Server.DisconectUser(this);
        }

        private async Task InitializeName()
        {
            while (true)
            {
                UdpReceiveResult udpReceiveResult = await UdpClient.ReceiveAsync();
                string? name = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                if (string.IsNullOrEmpty(name))
                    continue;
                Name = name;
                await Server.BroadcastAsync($"User {Name} connected", this, true);
                break;
            }
        }
        public async Task ReceiveMessageAsync()
        {
            try
            {
                await InitializeName();
                while (true)
                {
                    UdpReceiveResult udpReceiveResult = await UdpClient.ReceiveAsync();
                    string? message = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                    if (string.IsNullOrEmpty(message))
                        continue;
                    await Server.BroadcastAsync(message, this);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                await Disconect();
                Close();
            }
        }
    }
}
