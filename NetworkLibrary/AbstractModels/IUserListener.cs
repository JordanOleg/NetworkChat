using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.AbstractModels
{
    public interface IUserListener
    {
        Task ReceiveMessageAsync();
        Task Disconect();
        void Close();
    }
}
