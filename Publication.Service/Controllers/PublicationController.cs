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

        //// GET: api/Publication/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Publication
        [HttpPost]
        public void Post([FromBody]Person person)
        {
            this._rabbitMqPublisher.Publish(person, ExchangeType.Direct, "docker.test.exchange", "docker.test.queue");
        }

        //// PUT: api/Publication/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
