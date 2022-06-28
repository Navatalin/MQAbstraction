using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQAbstraction.Implementations;
using MQAbstraction.Models;

namespace MQAbstraction.Factory
{
    public class MessageQueueFactory : IMessageQueueFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageQueueFactory> _logger;
        private readonly IServiceProvider _serviceProvider;
        public MessageQueueFactory(IConfiguration configuration, ILogger<MessageQueueFactory> logger, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public IMessageQueue? GetMessageQueue()
        {
            var serverType = Enum.Parse(typeof(ServerTypes), _configuration["MessageQueue:ServerType"]);
            if (serverType != null)
            {
                switch (serverType)
                {
                    case ServerTypes.RabbitMq:
                        return (IMessageQueue?)_serviceProvider.GetService(typeof(RabbitMq));
                    case ServerTypes.AzureServiceBus:
                        return (IMessageQueue?)_serviceProvider.GetService(typeof(AzureServiceBus));
                    case ServerTypes.Kafka:
                        return (IMessageQueue?)_serviceProvider.GetService(typeof(Kafka));
                    default:
                        _logger.LogError($"Unable to find implementation for ServerType {serverType}");
                        throw new Exception("Unknown server type");
                }
            }
            _logger.LogError("Unable to determine service type");
            throw new NullReferenceException("Server Type Missing");

        }
    }


    public interface IMessageQueueFactory
    {
        IMessageQueue? GetMessageQueue();
    }
}
