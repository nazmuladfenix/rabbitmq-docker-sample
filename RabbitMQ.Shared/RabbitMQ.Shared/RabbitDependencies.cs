using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Shared.Infrastructure;

namespace RabbitMQ.Shared
{
    public class RabbitDependencies
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IChannelProvider, ChannelProvider>();
            services.AddSingleton<IRabbitMQReceiver, RabbitMQReceiver>();
            services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();
        }
    }
}
