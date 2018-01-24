using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RabbitMQ.Domain.Messages
{
    public class Person : IRabbitMessage
    {
        public Person()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
