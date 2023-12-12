using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//declare queue name
string requestQueueName = "request-response-design-example";

// You can customize the configuration here 
channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

//Read pusblisher message
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: requestQueueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (model, ea) =>
{
    var body = ea.Body.Span;
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);

    byte[] responseMessage = Encoding.UTF8.GetBytes($"Successfully complated operation for this message : {message}");

    //response message
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = ea.BasicProperties.CorrelationId;

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: ea.BasicProperties.ReplyTo,
        basicProperties: properties,
        body: responseMessage);
        body: responseMessage);
};

Console.Read();