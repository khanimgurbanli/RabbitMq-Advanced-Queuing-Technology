using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

namespace RabbitMQ.ESB.MassTransit.Request_Response.Pattern.Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            // other operations...
            Console.WriteLine(context.Message.Text);

            await context.RespondAsync<ResponseMessage>(new() { Text = $"{context.Message.MessageNo}. Response to request message" });//Return  result message  about request  message status
        }
    }
}
