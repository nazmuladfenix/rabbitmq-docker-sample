using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Domain;
using RabbitMQ.Shared.Infrastructure;

namespace RabbitMQ.Shared
{
    public class RabbitMQReceiver : IRabbitMQReceiver
    {
        private readonly IChannelProvider _channelProvider;

        public RabbitMQReceiver(IChannelProvider channelProvider)
        {
            this._channelProvider = channelProvider;
        }

        public void CreateReceiver<T>(Func<T, bool> handleMessage, string queueName, string exchangeType, string exchangeName, string routingKey = "default")
        {
            var channel = this._channelProvider.GetChannel();
            channel.ExchangeDeclare(exchangeName, exchangeType, false, false, null);

            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey, arguments: null);

            var consumer = new EventingBasicConsumer(channel)
            {
                ConsumerTag = "consumerTag"
            };
            consumer.Received += (model, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonConvert.DeserializeObject<T>(message);
                var success = handleMessage(obj);
                if (!success)
                {

                }
            };

            //channel.BasicAck(deliveryTag, false);
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer, consumerTag: consumer.ConsumerTag, noLocal: true, exclusive: false, arguments: null);
        }
    }
}
