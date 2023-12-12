using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

//declare queue bind
string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName,
    exchange: exchangeName,
    routingKey: string.Empty);

//Read pusblisher message
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
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