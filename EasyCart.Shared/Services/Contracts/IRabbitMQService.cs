namespace EasyCart.Shared.Services.Contracts;

public interface IRabbitMQService
{
    Task SendMessageAsync<T>(string queueName, T message);
    
    Task ConsumeMessageAsync<T>(string queueName, Func<T, Task> messageHandler);
}