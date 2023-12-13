

using MassTransit;
using RabbitMQ.ESB.MassTransit.Worker.Service.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configuration =>
        {
            configuration.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("secret-key");
            });
        });

        services.AddHostedService<PublishMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });
    })
    .Build();

host.Run();