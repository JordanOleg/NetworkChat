using NetworkLibrary.Settings.GlobalSettings;
using UserClient.Network;
using NetworkLibrary.UI;
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
SupportedProtocols protocolServer = Network.ParseProtocol();
Console.WriteLine("Connecting to server...");
IPEndPoint endPointServer = new IPEndPoint(ipServer, portServer);
Socket socket = Network.InitializationSocket(endPointServer, protocolServer);
StreamWriter writer = new StreamWriter(new NetworkStream(socket));
StreamReader reader = new StreamReader(new NetworkStream(socket));
if (socket == null)
{
    Console.WriteLine("Server not support TCP or UDP");
    return;
}
Console.WriteLine($"Connected to server: {endPointServer}");
Task.Run(() => ListenerMessage(socket));
while(true)
{
    string? message = Console.ReadLine();
    if (string.IsNullOrEmpty(message))
        continue;
    await writer.WriteLineAsync(message);
    await writer.FlushAsync();
}

static async Task ListenerMessage(Socket socket)
{
    while (true)
    {
        byte[] buffer = new byte[1024];
        int size = await socket.ReceiveAsync(buffer);
        string message = Encoding.UTF8.GetString(buffer, 0, size);
        UIConsole.Print(message);
    }
}