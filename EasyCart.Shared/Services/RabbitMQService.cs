using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EasyCart.Shared.Services.Contracts;

namespace EasyCart.Shared.Services;

public class RabbitMQService : IRabbitMQService
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMQService()
    {
        // Initialize the connection and channel to RabbitMQ
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }

    // Common method to declare the queue
    private async Task DeclareQueueAsync(string queueName)
    {
        await _channel.QueueDeclareAsync
        (
            queue: queueName, 
            durable: true, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null
        );
    }

    // Send a message to the RabbitMQ queue
    public async Task SendMessageAsync<T>(string queueName, T message)
    {
        // Declare the queue
        await DeclareQueueAsync(queueName);
        
        // Serialize the message to JSON
        var jsonMessage = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        
        // Publish the message to the queue
        await _channel.BasicPublishAsync
        (
            exchange: string.Empty,
            routingKey: queueName,
            body: body
        );
    }

    // Consume the messagew from queue
    public async Task ConsumeMessageAsync<T>(string queueName, Func<T, Task> messageHandler)
    {
        // Declare the queue
        await DeclareQueueAsync(queueName);

        // Create an event-based consumer
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            // Handle received message asynchronously
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var response = JsonConvert.DeserializeObject<T>(message);

            // Call the message handler to process the message asynchronously
            await messageHandler(response);
        };

        // Start consuming from the queue asynchronously
        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: true,
            consumer: consumer
        );
    }

    // Dispose the connection and channel to clean up resources
    public void Dispose()
    {
        _channel?.CloseAsync();
        _connection?.CloseAsync();
    }
}