using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("secret-key");

//Activate connection and open channel
using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declare exchnage
channel.ExchangeDeclare(exchange: "header-exchange", type: ExchangeType.Headers);


//send message
for (int i = 0; i < 15; i++)
{
    await Task.Delay(200);
    byte[] messages = Encoding.UTF8.GetBytes($"header-exchange-type {i}");
    Console.WriteLine("Enter header value: ");
    string value = Console.ReadLine();

    IBasicProperties properties = channel.CreateBasicProperties();
    properties.Headers = new Dictionary<string, object>()
    {
        ["no"] = value
    }; 
    
    channel.BasicPublish(
        exchange: "header-exchange",
        routingKey: string.Empty,  //The routing key value must be empty
        body: messages,
        basicProperties: properties);
}

Console.ReadKey();