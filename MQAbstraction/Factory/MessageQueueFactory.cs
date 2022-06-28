using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MQAbstraction.Models;

namespace MQAbstraction.Factory
{
    public class MessageQueueFactory : IMessageQueueFactory
    {
        public IMessageQueue GetMessageQueue(IConfiguration configuration)
        {
            var serverName = configuration["MessageQueue:ServerName"];
            var serverPort = configuration["MessageQueue:ServerPort"];
            var serverType = configuration["MessageQueue:ServerType"];
            var queueName = configuration["MessageQueue:QueueName"];
            Topics topics = new Topics(configuration["MessageQueue:Topics"]);
            


        }
    }

    
    public interface IMessageQueueFactory
    {
        IMessageQueue GetMessageQueue(IConfiguration configuration);
    }
}
