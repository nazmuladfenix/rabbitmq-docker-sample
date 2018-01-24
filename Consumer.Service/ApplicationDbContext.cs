using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Service
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }

    public class Person 
    {
        public Person()
        {
            //this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [Key]
        public string Id { get; set; }
    }

}
