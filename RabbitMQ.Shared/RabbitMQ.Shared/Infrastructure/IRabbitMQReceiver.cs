using System;

namespace RabbitMQ.Shared.Infrastructure
{
    public interface IRabbitMQReceiver
    {
        void CreateReceiver<T>(Func<T, bool> handleMessage, string queueName, string exchangeType, string exchangeName, string routingKey = "default");
    }
}