using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

//Activate connection and open channel
using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Create Queue and declare durable: true when close RbbitMq server protect queue and all messages 
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//when close RabbitMQ server being queues durability 1.
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 20; i++)
{
    await Task.Delay(200);
    //Send message to queue as a byte array
    byte[] message = Encoding.UTF8.GetBytes("test" + "-" + i);
    //when close RabbitMQ server being queues durability declare basicProperties 2.
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
}

Console.ReadLine();
