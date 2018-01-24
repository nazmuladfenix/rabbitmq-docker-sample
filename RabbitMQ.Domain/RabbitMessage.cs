using System;
using Newtonsoft.Json;

namespace RabbitMQ.Domain
{
    public class RabbitMessage
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IRabbitMessage MessageObject { get; set; }
    }
}
