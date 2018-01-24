using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Shared.Infrastructure;

namespace RabbitMQ.Shared
{
    public class ChannelProvider : IChannelProvider
    {
        private readonly IRabbitConfigurationProvider _rabbitConfigurationProvider;

        public ChannelProvider(IRabbitConfigurationProvider rabbitConfigurationProvider)
        {
            this._rabbitConfigurationProvider = rabbitConfigurationProvider;
        }
        public IModel GetChannel()
        {
            var configuration = this._rabbitConfigurationProvider.GetConfiguration();

            //var factory = new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    Port = 5672,
            //    UserName = "nazmul",
            //    Password = "P2qN9MVEv2Gn"
            //};
            var factory = new ConnectionFactory
            {
                HostName = configuration.HostName,
                //Port = configuration.Port,
                UserName = configuration.UserName,
                Password = configuration.Password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            connection.AutoClose = true;
            return channel;
        }
    }
}
