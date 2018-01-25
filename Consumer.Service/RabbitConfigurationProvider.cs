using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Domain;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;

namespace Consumer.Service
{
    public class RabbitConfigurationProvider : IRabbitConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public RabbitConfigurationProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._rabbitConfiguration = this.LoadConfig();
        }

        private RabbbitConfiguration LoadConfig()
        {
            var rabbitConfiguration = new RabbbitConfiguration();
            this._configuration.Bind("RabbitMQConfig", rabbitConfiguration);
            return rabbitConfiguration;
        }

        private RabbbitConfiguration _rabbitConfiguration;
        public RabbbitConfiguration GetConfiguration()
        {
            return this._rabbitConfiguration ?? (this._rabbitConfiguration = this.LoadConfig());

        }
    }
}
