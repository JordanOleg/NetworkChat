using NetworkLibrary.AbstractModels;
using NetworkLibrary.Settings.GlobalSettings;
using NetworkLibrary.TCP;
using NetworkLibrary.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.FabricClasses
{
    public class FabricUser
    {
        public static IUser FabricMethod(SupportedProtocols protocols, IPEndPoint ipEndPoint) =>
            protocols switch
            {
                SupportedProtocols.TCP => new TCPUser(ipEndPoint),
                SupportedProtocols.UDP => new UDPUser(ipEndPoint),
                _ => throw new ArgumentOutOfRangeException()
            };
        public static IUser FabricMethod(SupportedProtocols protocols, IPEndPoint ipEndPoint, Socket socket) =>
           protocols switch
           {
               SupportedProtocols.TCP => new TCPUser(ipEndPoint, socket),
               SupportedProtocols.UDP => new UDPUser(ipEndPoint, socket),
               _ => throw new ArgumentOutOfRangeException()
           };
    }
}
