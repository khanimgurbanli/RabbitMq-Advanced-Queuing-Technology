using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("secrt-key");

//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "header-exchange",
    type: ExchangeType.Headers);

Console.WriteLine("Enter header name: ");
string value = Console.ReadLine();

//queue declare
string _queueName = channel.QueueDeclare().QueueName;//get rabbit mq genarated default random queue name

//bind queue 
channel.QueueBind(
    queue: _queueName,
    exchange: "header-exchange",
    routingKey: string.Empty,
    new Dictionary<string, object>
    {
        ["no"] = value
    });

//Read pusblisher message
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: _queueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (model, ea) =>
{
    var body = ea.Body.Span;
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};


Console.Read();