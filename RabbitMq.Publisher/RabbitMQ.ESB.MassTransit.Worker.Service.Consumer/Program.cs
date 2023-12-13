
using MassTransit;
using RabbitMQ.ESB.MassTransit.Worker.Service.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configuration =>
        {
            configuration.AddConsumer<ExampleMessageConsumer>();

            configuration.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("secret-key");

                _configurator.ReceiveEndpoint("example-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();