using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("secrt-key");
//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//declare this configuration the same publisher so exchange type so: it ensures that messages are sent to all queues connected to this exchange (ExchangeType is class)
channel.ExchangeDeclare(
    exchange: "topic-exchange",
    type: ExchangeType.Topic);

//Assign a topic name, messages will be sent to queues with these topic values in the queue name.
Console.WriteLine("Enter topic name: ");
string topic = Console.ReadLine();

//queue declare
string _queueName = channel.QueueDeclare().QueueName;//get rabbit mq genarated default random queue name

//bind queue 
channel.QueueBind(
    queue: _queueName,
    exchange: "topic-exchange",
    routingKey: topic //The routing key value must be tapic name so it find with this value
    );

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