using UserClient.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary.Settings.GlobalSettings;
using System.Net.Sockets;
using System.Net;


namespace UserClient.Network
{
    internal static class NetworkClient
    {
        public static Socket PingServer(IPEndPoint ipEndPoint, out SupportedProtocols protocols)
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(ipEndPoint);
                    protocols = SupportedProtocols.TCP;
                    return socket;
                }
            }
            catch { }
            try
            {
                using(Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);
                    protocols = SupportedProtocols.UDP;
                    return socket;
                }
            }
            catch { }
            protocols = SupportedProtocols.Unknown;
            throw new ProtocolViolationException("Server not support TCP or UDP");
        }
        public static Socket? InitializationSocket(SupportedProtocols protocol)
        {
            using Socket? socket = protocol switch
            {
                SupportedProtocols.TCP => new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp),
                SupportedProtocols.UDP => new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp),
                SupportedProtocols.None => null,
                SupportedProtocols.Unknown => null,
                _ => throw new Exception("Protocol not supported")
            };
            return socket;
        }
        public static Socket InitializationSocket(IPEndPoint ipEndPoint, SupportedProtocols protocol)
        {
            try
            {
                Socket? socket = InitializationSocket(protocol);
                if(socket is null)
                {
                    socket = PingServer(ipEndPoint, out _);
                }
                socket.Connect(ipEndPoint);
                return socket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static SupportedProtocols ParseProtocol()
        {
            while (true)
            {
                Console.WriteLine("Do you know using server protocol? (TCP/UDP)");
                Console.WriteLine("TCP: tcp | UDP: udp | I don`t know: r");
                string? protocol = Console.ReadLine();
                if (VariableProject.protocols.TryGetValue(protocol, out SupportedProtocols value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("The protocol is incorrect or I do not support it.");
                    continue;
                }
            }
        }
    }
}
