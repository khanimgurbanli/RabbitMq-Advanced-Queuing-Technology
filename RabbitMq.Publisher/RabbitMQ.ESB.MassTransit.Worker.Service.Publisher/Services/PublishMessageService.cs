
using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQ.ESB.MassTransit.Worker.Service.Publisher.Services
{
    public class PublishMessageService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{++i}. message"
                };

                _publishEndpoint.Publish(message);
            }
        }
    }
}
