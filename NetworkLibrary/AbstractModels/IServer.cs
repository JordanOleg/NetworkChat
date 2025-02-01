using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.AbstractModels
{
    public interface IServer
    {
        void Start();
        void Stop();
        Task ListenerAsync();
        Task DisconectAllUsers();
        Task DisconectUser(IUserListener userListener);
        Task BroadcastAsync(string? message, IUserListener userListener, bool itsAdmin = false);
        Task BroadcastMessageUser(string? message, IUserListener userListener);
        Task BroadcastMessageAllAsync(string? message);
    }
}
