using System.Net.Sockets;
using System.Net;
using NetworkLibrary.Settings.GlobalSettings;
using UserServer.Other;
using UserServer.UI;
using NetworkLibrary.TCP;

SupportedProtocols serverProtocol = ConsoleValidater.ValidateProtocolWhile();
IPAddress ipServer = ConsoleValidater.ValidateIPWhile();
int portServer = ConsoleValidater.ValidatePortWhile();
IPEndPoint serverEndPoint = new IPEndPoint(ipServer, portServer);
UI.StartServer();
if (serverProtocol == SupportedProtocols.TCP)
{
    TCPServerUser server = new TCPServerUser(serverEndPoint);
    await server.ListenerAsync();
}
