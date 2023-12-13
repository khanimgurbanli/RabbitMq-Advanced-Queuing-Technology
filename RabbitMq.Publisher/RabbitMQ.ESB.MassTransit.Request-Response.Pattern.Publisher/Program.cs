
using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

string rabbitMQUrl = "secret-key";
string requestQueue = "example-queie";

IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUrl);
});

await busControl.StartAsync();  //we must start couse always listening

var request = busControl.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUrl}/{requestQueue}"));

int i = 1;
while (true)
{
    await Task.Delay(200);
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i}. request " });

    Console.WriteLine($"Response recieved :{response.Message.Text}");
}

//So we have used MassTransit with Request-Response pattern

Console.ReadLine(); 