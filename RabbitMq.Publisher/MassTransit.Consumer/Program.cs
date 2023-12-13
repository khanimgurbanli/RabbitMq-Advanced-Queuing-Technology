using MassTransit;
using MassTransit.Consumer.Consumers;

string rabbitMqUri = "amqps://kblsyoto:LOXUfvXRVIlEwxTNLbpRdR5zUncQ8E3N@fish.rmq.cloudamqp.com/kblsyoto";
string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadLine();