using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare queue name
string queueName = "work-queue-design-example";

// You can customize the configuration here 
channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

for (int i = 0; i < 50; i++)
{
    await Task.Delay(200);
    //send message
    byte[] messages = Encoding.UTF8.GetBytes($"work-queue-design-message {i}");

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: messages);
}

Console.ReadLine();