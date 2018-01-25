using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Domain.Messages;

namespace Consumer.Service
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.Migrate();

        }

        public DbSet<Person> Persons { get; set; }
    }
}
