using RabbitMQ.Domain;

namespace RabbitMQ.Shared.Infrastructure
{
    public interface IRabbitConfigurationProvider
    {
        RabbbitConfiguration GetConfiguration();
    }
}