using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

string replyQueueName = channel.QueueDeclare().QueueName;
string corelationId = Guid.NewGuid().ToString();

//Create request message and send
IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = corelationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{
    await Task.Delay(200);
    //send message
    byte[] messages = Encoding.UTF8.GetBytes($"request-response-design-message {i}");

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: replyQueueName,
        body: messages,
        basicProperties: properties);
}

//Listen response queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer
    );

//Read messages
consumer.Received += (model, ea) =>
{
    if (ea.BasicProperties.CorrelationId == corelationId)
    {
        var body = ea.Body.Span;
        string message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Response: {message}");
    }
};


Console.Read();