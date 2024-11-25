using Azure.Messaging.ServiceBus;
using FileUploadServiceApi.Infrastructure.Messaging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadServiceApi.Infrastructure.Messaging
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ServiceBusReceiver _serviceBusReceiver;

        public ServiceBusService(IOptions<ServiceBusConfiguration> serviceBusConfig)
        {
            // Initialize Service Bus Client and Sender/Receiver
            _serviceBusClient = new ServiceBusClient(serviceBusConfig.Value.ConnectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(serviceBusConfig.Value.QueueName);
            _serviceBusReceiver = _serviceBusClient.CreateReceiver(serviceBusConfig.Value.QueueName);
        }

        // Method to send a message to the Service Bus
        public async Task SendMessageAsync(string messageBody)
        {
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await _serviceBusSender.SendMessageAsync(message);
        }

        // Method to receive a message from the Service Bus
        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync()
        {
            // Receive a message (you can customize the way you want to receive)
            var receivedMessage = await _serviceBusReceiver.ReceiveMessageAsync(TimeSpan.FromSeconds(10));
            return receivedMessage;
        }

        // Close connections when finished
        public async Task CloseAsync()
        {
            await _serviceBusSender.CloseAsync();
            await _serviceBusReceiver.CloseAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
