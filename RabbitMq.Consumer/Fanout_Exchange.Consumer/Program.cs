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
    exchange: "fanaout-exchange",
    type: ExchangeType.Fanout);

//declare n count queue name and send message gell all. You will see to send message all queue 
Console.WriteLine("Enter queue name: ");
string _queueName = Console.ReadLine(); 

//queue declare
channel.QueueDeclare(
    queue: _queueName,
    exclusive: false);

//bind queue 
channel.QueueBind(
    queue: _queueName,
    exchange: "fanaout-exchange",
    routingKey: string.Empty //The routing key value must be empty because it does not differentiate queues as a publisher configuration
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