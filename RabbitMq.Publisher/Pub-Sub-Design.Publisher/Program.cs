using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare exchange name
string exchangeName = "Pub/Sub-design-example";

// You can customize the configuration here 
channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);

for (int i = 0; i < 50; i++)
{
    await Task.Delay(200);
    //send message
    byte[] messages = Encoding.UTF8.GetBytes($"Pub/Sub-design-message {i}");

    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        body: messages);
}

Console.ReadLine();