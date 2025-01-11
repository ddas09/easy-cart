using EasyCart.Shared.Services;
using EasyCart.Shared.Models.Queue;
using EasyCart.Shared.Services.Contracts;

namespace EasyCart.NotificationService;

class Program
{
    static async Task Main(string[] args)
    {
        // Display a message to indicate that the service is running
        Console.WriteLine("Notification Service is running...");

        // Initialize the shared RabbitMQService to interact with RabbitMQ
        var rabbitMQService = new RabbitMQService();

        // Define the queue name where user registration messages will be sent
        string queueName = "user-info";

        // Now listen for messages and simulate notifications
        await HandleUserRegistration(rabbitMQService, queueName);

        // Keep the application running indefinitely until the user presses Enter
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }

    static async Task HandleUserRegistration(IRabbitMQService rabbitMQService, string queueName)
    {
        // Keep listening for messages indefinitely
        await rabbitMQService.ConsumeMessageAsync<UserMessage>(queueName, async (userMessage) =>
        {
            if (userMessage != null)
            {
                // Simulate sending a notification (e.g., email)
                Console.WriteLine($"New User Registered:");
                Console.WriteLine($"User ID: {userMessage.UserId}");
                Console.WriteLine($"Email: {userMessage.Email}");
                Console.WriteLine("Notification: An email has been sent to the user.\n");

                // Here you could perform an asynchronous operation, e.g., sending an email or making an HTTP request
                await Task.Delay(500);
            }
            else
            {
                Console.WriteLine("No message received.");
            }
        });
    }

}

