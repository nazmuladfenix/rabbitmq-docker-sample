using RabbitMQ.Client;

namespace RabbitMQ.Shared.Infrastructure
{
    public interface IChannelProvider
    {
        IModel GetChannel();
    }
}