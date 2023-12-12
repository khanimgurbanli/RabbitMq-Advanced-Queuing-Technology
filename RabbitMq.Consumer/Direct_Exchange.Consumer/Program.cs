using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("secrt-key");

//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare exchange type
channel.ExchangeDeclare(exchange: "direct-exchange", type: ExchangeType.Direct);

//Get queueName
string queueName = channel.QueueDeclare().QueueName;

//Exchange binding
channel.QueueBind(queue: queueName, exchange: "direct-exchange", routingKey: "example-queue");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.Span;
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

Console.ReadLine(); 