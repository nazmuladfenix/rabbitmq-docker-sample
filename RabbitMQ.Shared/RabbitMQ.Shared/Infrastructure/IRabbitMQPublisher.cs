namespace RabbitMQ.Shared.Infrastructure
{
    public interface IRabbitMQPublisher
    {
        bool Publish<T>(T message, string exchangeType, string exchangeName,string queuName, string routingKey = "default");
    }
}