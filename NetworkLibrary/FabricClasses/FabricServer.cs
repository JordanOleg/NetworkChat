using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary.AbstractModels;
using NetworkLibrary.Settings.GlobalSettings;
using NetworkLibrary.TCP;
using NetworkLibrary.UDP;

namespace NetworkLibrary.FabricClasses
{
    public class FabricServer
    {
        public static IServer FabricMethod(SupportedProtocols protocols, IPEndPoint ipEndPoint) =>
            protocols switch
            {
                SupportedProtocols.TCP => new TCPServer(ipEndPoint),
                SupportedProtocols.UDP => new UDPServer(ipEndPoint),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
