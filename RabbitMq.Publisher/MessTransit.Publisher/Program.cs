using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMqUri = "amqps://kblsyoto:LOXUfvXRVIlEwxTNLbpRdR5zUncQ8E3N@fish.rmq.cloudamqp.com/kblsyoto";
string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

ISendEndpoint sendEnpoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.WriteLine("Sended message : ");
string message = Console.ReadLine();

await sendEnpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});

Console.ReadLine();