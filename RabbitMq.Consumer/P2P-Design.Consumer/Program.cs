using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("secrt-key");

//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare queue name
string queueName = "P2P-design-example";

// You can customize the configuration here 
channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: false,
    consumer: consumer);

//working message 
consumer.Received += (model, ea) =>
{
    var body = ea.Body.Span;
    Console.WriteLine(Encoding.UTF8.GetString(body));
};

Console.Read();
