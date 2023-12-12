using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

//Activate connection and open channel
using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//declare exchange type so: it ensures that messages are sent to all queues connected to this exchange (ExchangeType is class)
channel.ExchangeDeclare(
    exchange: "fanaout-exchange",
    type: ExchangeType.Fanout);

for (int i = 0; i < 15; i++)
{
    await Task.Delay(200);
    byte[] messages = Encoding.UTF8.GetBytes($"fanout-exchange-type {i}");

    channel.BasicPublish(
        exchange: "fanaout-exchange",
        routingKey: string.Empty,  //The routing key value must be empty because it does not differentiate queues.
        body:messages);
}

Console.ReadKey();  