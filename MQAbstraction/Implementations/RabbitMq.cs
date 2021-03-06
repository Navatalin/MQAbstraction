using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQAbstraction.Handlers;
using MQAbstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQAbstraction.Implementations
{
    internal class RabbitMq : IMessageQueue
    {
        private readonly ILogger<RabbitMq> _logger;
        private readonly IConfiguration _configuration;
        public RabbitMq(ILogger<RabbitMq> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string ConnectionString { get; }
        public string QueueName { get; }
        public Topics Topics { get; }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public Task<string> ReceiveMessage()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToTopics(ITopicHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
