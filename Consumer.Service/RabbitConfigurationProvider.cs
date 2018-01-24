using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Domain;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;

namespace Consumer.Service
{
    public class RabbitConfigurationProvider : IRabbitConfigurationProvider
    {
        private RabbbitConfiguration _configuration;
        //configuration supposed to come from config file
        public RabbbitConfiguration GetConfiguration()
        {
            return this._configuration ?? (this._configuration = new RabbbitConfiguration
            {
                HostName = "rabbitMQ",
                Port = 5672,
                //UserName = "nazmul",
                //Password = "P2qN9MVEv2Gn"
                UserName = "guest",
                Password = "guest"
            });

        }
    }
}
