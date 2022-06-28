using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp;
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
    internal class AzureServiceBus : IMessageQueue
    {
        public string ConnectionString { get;  }
        public string QueueName { get; }
        public Topics Topics { get; }

        private readonly ILogger<AzureServiceBus> _logger;
        private readonly IConfiguration _configuration;

        ITopicHandler? _handler;
        
        public AzureServiceBus(ILogger<AzureServiceBus> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            ConnectionString = configuration["MessageQueue:ConnectionString"];
            QueueName = configuration["MessageQueue:QueueName"];
            Topics = new Topics(configuration["MessageQueue:Topics"]);
        }

        public async void SendMessage(string message)
        {
            await using var client = new ServiceBusClient(ConnectionString);
            ServiceBusSender serviceBusSender = client.CreateSender(QueueName);
            ServiceBusMessage serviceBusMessage = new(message);
            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }

        public async Task<string> ReceiveMessage()
        {
            await using var client = new ServiceBusClient(ConnectionString);
            ServiceBusReceiver serviceBusReceiver = client.CreateReceiver(QueueName);
            ServiceBusReceivedMessage serviceBusReceivedMessage = await serviceBusReceiver.ReceiveMessageAsync();

            string body = serviceBusReceivedMessage.Body.ToString();
            return body;
        }

        public async void SubscribeToTopics(ITopicHandler handler)
        {
            _handler = handler;
            await using var client = new ServiceBusClient(ConnectionString);

            //Get these options from Config
            var options = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = true
            };
            
            foreach(var topic in Topics.TopicPairs)
            {
                var processor = client.CreateProcessor(topic.Key, options);
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync();
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            //Handle message and return back to complete message
            string body = args.Message.Body.ToString();
            string correlationId = args.Message.CorrelationId;
            if (_handler != null)
            {
                var result = _handler.Handle(body, correlationId);
                //If result processed correctly then complete, else send to deadletter
                if (result)
                {
                    await args.CompleteMessageAsync(args.Message);
                }
                else
                {
                    await args.DeadLetterMessageAsync(args.Message);
                }
            }
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError($"Error Source: {args.ErrorSource} NameSpace: {args.FullyQualifiedNamespace} EntityPath: {args.EntityPath} Exception: {args.Exception}");
            return Task.CompletedTask;
        }
    }
}
