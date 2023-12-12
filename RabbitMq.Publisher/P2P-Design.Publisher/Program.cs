using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare queue name
string queueName = "P2P-design-example";

// You can customize the configuration here 
channel.QueueDeclare(
    queue: queueName,
    durable:false,
    exclusive:false,
    autoDelete:false);

//send message
    byte[] messages = Encoding.UTF8.GetBytes($"p2p-design-message");

    string topic = Console.ReadLine();

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName, 
        body: messages);

Console.ReadKey();