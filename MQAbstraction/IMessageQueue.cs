using MQAbstraction.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQAbstraction
{
    public interface IMessageQueue
    {
        void SendMessage(string message);
        Task<string> ReceiveMessage();
        void Disconnect();
        void SubscribeToTopics(ITopicHandler handler);
    }
}
