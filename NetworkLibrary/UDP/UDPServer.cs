using NetworkLibrary.AbstractModels;
using NetworkLibrary.TCP;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;

namespace NetworkLibrary.UDP
{
    public class UDPServer : IServer
    {
        public int Port { get; private set; } 
        public List<UDPUserListener> clients { get; private set; }
        Socket SocketListener { get; set; }
        public UDPServer(IPEndPoint endPoint)
        {
            SocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            SocketListener.Bind(endPoint); 
            SocketListener.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, true);
            clients = new List<UDPUserListener>();
            Port = endPoint.Port;
        }
        public async Task ListenerAsync()
        {
            while (true)
            {
                UdpClient udpClient = new UdpClient();
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                UDPUserListener userListener = new UDPUserListener(udpClient, this, result.RemoteEndPoint);
                AddIfDontContaints(userListener);
                Task.Run(() =>
                {   
                    BroadcastMessageUser(Encoding.UTF8.GetString(result.Buffer), userListener);   
                });
            }
        }
        private void AddIfDontContaints(UDPUserListener user)
        {
            if (!clients.Contains(user))
            {
                clients.Add(user);
            }
        }
        private UDPUserListener IsUDPUserListener(IUserListener user)
        {
            if (!(user is UDPUserListener userListener))
                throw new ArgumentException("User is not an implementation of TCPUserListener");
            return userListener;
        }
        public async Task DisconectAllUsers()
        {
            foreach (UDPUserListener client in clients)
            {
                await DisconectUser(client);
            }
            clients.Clear();
        }
        public async Task DisconectUser(IUserListener userListener)
        {
            UDPUserListener user = IsUDPUserListener(userListener);
            clients.Remove(user);
            await BroadcastAsync($"User {user.Id} disconected", user);
            user.Close();
        }

        public async Task BroadcastAsync(string? message, IUserListener userListener, bool itsAdmin = false)
        {
            UDPUserListener user = IsUDPUserListener(userListener);
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
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");
            UDPUserListener user = IsUDPUserListener(userListener);
            foreach (UDPUserListener client in clients)
            {
                if (user.Id != client.Id)
                {
                    await client.UdpClient.SendAsync(Encoding.UTF8.GetBytes(message));
                }
            }
        }
        public async Task BroadcastMessageAllAsync(string? message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");
            foreach (UDPUserListener client in clients)
            {
                await client.UdpClient.SendAsync(Encoding.UTF8.GetBytes(message));
            }
        }
        public void Start() { }
        public void Stop() { }
    }
}
