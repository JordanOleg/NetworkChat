using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserClient.Other
{
    internal static class ConsoleValidater
    {
        public static string ValidateNameUser()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("How are you name?");
                    string? name = Console.ReadLine();
                    string result = string.IsNullOrEmpty(name) ? throw new Exception() : name;
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Name null or empty");
                    continue;
                }
            }
        }
        public static IPAddress ValidateIP()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("What is the server IP?");
                    string? ip = Console.ReadLine();
                    IPAddress ipServer = IPAddress.Parse(ip);
                    return ipServer;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("IP null or invalid");
                    continue;
                }
            }
        }
        public static int ValidatePort()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("What is the server port?");
                    string? port = Console.ReadLine();
                    int portServer = int.Parse(port);
                    return portServer;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Port null or invalid");
                    continue;
                }
            }
        }

    }
}
