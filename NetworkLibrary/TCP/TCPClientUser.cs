using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.TCP
{
    public class TCPClientUser
    {
        public TcpClient TcpClient;
        public TCPServerUser Server;
        public StreamReader Reader;
        public StreamWriter Writer;
        public Guid Id { get; private set; }
        private void Initialize()
        {
            NetworkStream stream = TcpClient.GetStream();
            Reader = new StreamReader(stream);
            Writer = new StreamWriter(stream);
            Id = Guid.NewGuid();
        }
        public TCPClientUser(TcpClient tcpClient, TCPServerUser server)
        {
            TcpClient = tcpClient;
            Server = server;
            Initialize();
        }

        public async Task ReceiveMessageAsync()
        {
            try
            {
                while (true)
                {
                    string? message = await Reader.ReadLineAsync();
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
        public void Close()
        {
            TcpClient.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}
