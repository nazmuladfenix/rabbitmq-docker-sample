using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Domain.Entities
{
    public class Person : IRabbitMessage
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
