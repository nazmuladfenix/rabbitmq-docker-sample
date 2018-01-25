using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;

namespace Publication.Service
{
    public class RabbitConfigurationProvider : IRabbitConfigurationProvider
    {
        private RabbbitConfiguration _configuration;

        public RabbbitConfiguration GetConfiguration()
        {
            return this._configuration ?? (this._configuration = new RabbbitConfiguration
            {
                HostName = "172.16.0.3",
                Port = 5672,
                UserName = "nazmul",
                Password = "P2qN9MVEv2Gn"
                //UserName = "guest",
                //Password = "guest"
            });
        }
    }
}
