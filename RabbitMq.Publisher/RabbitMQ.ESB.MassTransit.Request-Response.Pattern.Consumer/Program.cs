using MassTransit;
using RabbitMQ.ESB.MassTransit.Request_Response.Pattern.Consumer.Consumers;

string rabbitMQUrl = "secret-key";
string requestQueue = "example-queie";

IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUrl);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await busControl.StartAsync();

Console.ReadLine();