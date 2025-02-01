using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using NetworkLibrary.AbstractModels;
using System.Net;

namespace NetworkLibrary.TCP
{
    public class TCPServer : IServer 
    {
        public List<TCPUserListener> clients { get; private set; }
        public TcpListener Listener { get; private set;} 
        public TCPServer(IPEndPoint endPoint)
        {
            Listener = new TcpListener(endPoint);
            clients = new List<TCPUserListener>();
        }
        public async Task ListenerAsync()
        {

            try
            {
                Start();
                while (true)
                {
                    TcpClient client = await Listener.AcceptTcpClientAsync();
                    TCPUserListener tcpClientUser = new TCPUserListener(client, this);
                    clients.Add(tcpClientUser);
                    Task.Run(() => tcpClientUser.ReceiveMessageAsync());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await DisconectAllUsers();
                Stop();
            }
        }
        private TCPUserListener IsTCPUserListener(IUserListener user)
        {
            if (!(user is TCPUserListener userListener))
                throw new ArgumentException("User is not an implementation of TCPUserListener");
            return userListener;
        }
        public async Task DisconectAllUsers()
        {
            foreach (TCPUserListener client in clients)
            {
                await DisconectUser(client);  
            }
            clients.Clear();
        }
        public async Task DisconectUser(IUserListener userListener)
        {
            TCPUserListener user = IsTCPUserListener(userListener);
            clients.Remove(user);
            await BroadcastAsync($"User {user.Id} disconected", user);
            user.Close();
        }

        public async Task BroadcastAsync(string? message, IUserListener userListener, bool itsAdmin = false)
        {
            TCPUserListener user = IsTCPUserListener(userListener);
            if (itsAdmin)
            {
                await BroadcastMessageAllAsync(message);
            }
            else
            {
                await BroadcastMessageUser(message, user);
            }  
        }
        public async Task BroadcastMessageUser(string? message, IUserListener userListener)
        {
            TCPUserListener user = IsTCPUserListener(userListener);
            foreach (TCPUserListener client in clients)
            {
                if (user.Id != client.Id)
                {
                    await client.Writer.WriteLineAsync(message);
                    await client.Writer.FlushAsync();
                }
            }
        }
        public async Task BroadcastMessageAllAsync(string? message)
        {
            foreach (TCPUserListener client in clients)
            {
                await client.Writer.WriteLineAsync(message);
                await client.Writer.FlushAsync();
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
