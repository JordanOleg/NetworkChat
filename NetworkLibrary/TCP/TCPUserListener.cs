using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary.AbstractModels;

namespace NetworkLibrary.TCP
{
    public class TCPUserListener : IUserListener
    {
        public TcpClient TcpClient { get; private set; }
        public TCPServer Server { get; private set; }
        public StreamReader Reader { get; private set; }
        public StreamWriter Writer { get; private set; }
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        private void Initialize()
        {
            NetworkStream stream = TcpClient.GetStream();
            Reader = new StreamReader(stream);
            Writer = new StreamWriter(stream);
            Id = Guid.NewGuid();
        }
        public TCPUserListener(TcpClient tcpClient, TCPServer server)
        {
            TcpClient = tcpClient;
            Server = server;
            Initialize();
        }
        private async Task InitializeName()
        {
            while (true)
            {
                string? name = await Reader.ReadLineAsync();
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
                    string? message = await Reader.ReadLineAsync();
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
                Close();
            }
        }
        public async Task Disconect()
        {
            await Server.DisconectUser(this);
        }
        public void Close()
        {
            TcpClient.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}
