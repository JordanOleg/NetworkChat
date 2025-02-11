using NetworkLibrary.Settings.GlobalSettings;
using NetworkLibrary.TCP;
using UserClient.Network;
using NetworkLibrary.UI;
using NetworkLibrary.FabricClasses;
using NetworkLibrary.UDP;
using System.Net;
using System.Text;
using System.Net.Sockets;
using UserClient.Settings;
using UserClient.Other;
using NetworkLibrary.AbstractModels;

Console.WriteLine("Starting...");
string nameUser = ConsoleValidater.ValidateNameUser();
IPAddress ipServer = ConsoleValidater.ValidateIP();
int portServer = ConsoleValidater.ValidatePort();
SupportedProtocols protocolServer = Network.ParseProtocol();
Console.WriteLine("Connecting to server...");
IPEndPoint endPointServer = new IPEndPoint(ipServer, portServer);
Socket socket = Network.InitializationSocket(endPointServer, protocolServer);
IUser user = FabricUser.FabricMethod(protocolServer, endPointServer, socket);
Task.Factory.StartNew(() => user.ListenerMessageAsync(), TaskCreationOptions.LongRunning);
Main(user);


static void Main(IUser user)
{
    while (true)
    {
        string? message = Console.ReadLine();
        if (!string.IsNullOrEmpty(message))
            user.SendingMessageAsync(message);
    }
}