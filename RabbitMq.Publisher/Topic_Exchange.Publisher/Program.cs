using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

//Activate connection and open channel
using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare exchnage
channel.ExchangeDeclare(exchange: "topic-exchange", type:ExchangeType.Topic);


//send message
for (int i = 0; i < 15; i++)
{
    await Task.Delay(200);
    byte[] messages = Encoding.UTF8.GetBytes($"topic-exchange-type {i}");

    string topic = Console.ReadLine();  

    channel.BasicPublish(
        exchange: "topic-exchange",
        routingKey: topic,  //The routing key value must be tapic name so it find with this value
        body: messages);
}

Console.ReadKey();