using NetworkLibrary.Settings.GlobalSettings;
using UserClient.Network;
using NetworkLibrary.TCP;
using NetworkLibrary.UDP;
using System.Net;
using System.Text;
using System.Net.Sockets;
using UserClient.Settings;
using UserClient.Other;

Console.WriteLine("Starting...");
string nameUser = ConsoleValidater.ValidateNameUser();
IPAddress ipServer = ConsoleValidater.ValidateIP();
int portServer = ConsoleValidater.ValidatePort();
SupportedProtocols protocolServer = NetworkClient.ParseProtocol();
Console.WriteLine("Connecting to server...");
IPEndPoint endPointServer = new IPEndPoint(ipServer, portServer);
Socket socket = NetworkClient.InitializationSocket(endPointServer, protocolServer);
if (socket == null)
{
    Console.WriteLine("Server not support TCP or UDP");
    return;
}
Console.WriteLine($"Connected to server: {endPointServer}");
Task.Run(() => ListenerMessage(socket));
while(true)
{
    string message = Console.ReadLine();
    if (string.IsNullOrEmpty(message))
        continue;
    socket.Send(Encoding.UTF8.GetBytes(message));
}

static void ListenerMessage(Socket socket)
{
    while (true)
    {
        byte[] buffer = new byte[1024];
        int size = socket.Receive(buffer);
        string message = Encoding.UTF8.GetString(buffer, 0, size);
        Console.WriteLine(message);
    }
}
