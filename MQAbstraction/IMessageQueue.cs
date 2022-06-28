using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQAbstraction
{
    public interface IMessageQueue
    {
        bool Connect();
        bool IsConnected();
        void SendMessage(string message);
        string ReceiveMessage();
        void Disconnect();
        
    }
}
