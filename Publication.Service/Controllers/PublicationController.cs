using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Domain.Entities;
using RabbitMQ.Shared.Infrastructure;

namespace Publication.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PublicationController : Controller
    {
        private readonly IRabbitMQPublisher _rabbitMqPublisher;

        public PublicationController(IRabbitMQPublisher rabbitMqPublisher)
        {
            this._rabbitMqPublisher = rabbitMqPublisher;
        }
        // GET: api/Publication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        

        // POST: api/Publication
        [Consumes("application/json")]
        [HttpPost]
        public void Post([FromBody]Person person)
        {
            Console.WriteLine("==============Got the person to publish============");
            Console.WriteLine($"Name: {person.Name} Address:{person.Address}");
            this._rabbitMqPublisher.Publish(person, ExchangeType.Direct, "docker.test.exchange", "docker.test.queue");
        }
    }
}
