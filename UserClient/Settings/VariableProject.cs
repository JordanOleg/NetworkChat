using NetworkLibrary.Settings.GlobalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserClient.Settings
{
    internal static class VariableProject
    {
        public static Dictionary<string, SupportedProtocols> protocols = new Dictionary<string, SupportedProtocols>
        {
            { "tcp", SupportedProtocols.TCP },
            { "udp", SupportedProtocols.UDP },
            { "r", SupportedProtocols.None }
        };

    }
}
