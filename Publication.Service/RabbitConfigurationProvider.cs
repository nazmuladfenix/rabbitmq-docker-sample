using Microsoft.Extensions.Configuration;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;

namespace Publication.Service
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
