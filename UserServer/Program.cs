using System.Net.Sockets;
using System.Net;
using NetworkLibrary.Settings.GlobalSettings;
using UserServer.Other;
using UserServer.UI;
using NetworkLibrary.TCP;
using NetworkLibrary.FabricClasses;
using NetworkLibrary.AbstractModels;

SupportedProtocols serverProtocol = ConsoleValidater.ValidateProtocolWhile();
IPAddress ipServer = ConsoleValidater.ValidateIPWhile();
int portServer = ConsoleValidater.ValidatePortWhile();
IPEndPoint serverEndPoint = new IPEndPoint(ipServer, portServer);
UI.StartServer();
IServer server = FabricServer.FabricMethod(serverProtocol, serverEndPoint);
server.Start();
await server.ListenerAsync();
