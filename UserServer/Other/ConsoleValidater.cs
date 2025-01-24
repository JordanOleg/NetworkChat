using NetworkLibrary.Settings.GlobalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.Other
{
    internal class ConsoleValidater
    {
        public static int ValidatePortWhile()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("What is the server port?");
                    string? tryPort = Console.ReadLine();
                    int port = int.Parse(tryPort);
                    return port;
                }
                catch
                {
                    Console.WriteLine("Invalid or null");
                    continue;
                }
            }
        }
        public static IPAddress ValidateIPWhile()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("What is the server IP?");
                    string? tryStrIP = Console.ReadLine();
                    IPAddress ip = IPAddress.Parse(tryStrIP);
                    return ip;
                }
                catch
                {
                    Console.WriteLine("Invalid or null");
                    continue;
                }
            }
        }
        public static SupportedProtocols ValidateProtocol(string? protocol)
        {
            return protocol switch
            {
                "tcp" => SupportedProtocols.TCP,
                "udp" => SupportedProtocols.UDP,
                _ => throw new ArgumentException("Invalid protocol")
            };
        }
        public static SupportedProtocols ValidateProtocolWhile()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("How is using protocol?");
                    Console.WriteLine("TCP: tcp | UDP: udp");
                    string? protocol = Console.ReadLine();
                    SupportedProtocols serverProtocol = ValidateProtocol(protocol);
                    return serverProtocol;
                }
                catch
                {
                    Console.WriteLine("Invalid or null");
                    continue;
                }
            }
        }
    }
}
