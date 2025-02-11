using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.AbstractModels
{
    public interface IUser
    {
        Task ListenerMessageAsync();
        Task SendingMessageAsync(char[] message);
        Task SendingMessageAsync(string message);
    }
}
