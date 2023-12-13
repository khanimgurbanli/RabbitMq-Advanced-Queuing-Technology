using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQ.ESB.MassTransit.Worker.Service.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Recived message: {context.Message.Text}");

            return Task.CompletedTask;  
        }
    }
}
