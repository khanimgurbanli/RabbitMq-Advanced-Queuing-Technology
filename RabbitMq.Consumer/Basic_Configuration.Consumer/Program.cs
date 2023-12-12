using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("secrt-key");

//Activate connection and open channel
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Create Queue 
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//Read message from queue
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);

//prefetchSize- the size of the largest message received by a consumer in byte type. 0 means Unlimited
//prefetchCount - number of messages that can be processed simultaneously by a consumer
//global - make sure that all or only the consumer end called is valid.
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.Span;
    Console.WriteLine(Encoding.UTF8.GetString(body));

    // message already worked so confirm delete message from queue;
    // multiple: false--> delete just this message from queue. If i will  write multiple:true in this time delete this and previous messages as well 
    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

Console.Read();
