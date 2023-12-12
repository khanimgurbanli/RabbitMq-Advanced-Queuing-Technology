using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Create connection
ConnectionFactory factory = new();
connectionFactory.Uri = new("secret-key");

//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//when we send messages direct to queue using this configuratin  type:ExchangeType.Direct
channel.ExchangeDeclare(exchange: "direct-exchange", type: ExchangeType.Direct);

for (int i = 0; i < 15; i++)
{
    Console.WriteLine("Message: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "direct-exchange",
        routingKey: "example-queue",
        body: byteMessage);
}

Console.ReadLine();