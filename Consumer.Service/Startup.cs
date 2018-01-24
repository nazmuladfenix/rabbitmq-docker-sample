using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Domain.Messages;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;

namespace Consumer.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IServiceProvider _serviceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("ConnectionString");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddMvc();
            this.ConfigureDependencies(services);
            var sp = services.BuildServiceProvider();
            this._serviceProvider = sp;
            var rabbitReceiver = sp.GetService<IRabbitMQReceiver>();
            rabbitReceiver.CreateReceiver<Person>(this.HandleMessage, "docker.test.queue", ExchangeType.Direct, "docker.test.exchange");
        }

        private bool HandleMessage(Person person)
        {
            var dbContext = this._serviceProvider.GetService<ApplicationDbContext>();
            dbContext.Persons.Add(person);
            dbContext.SaveChanges();
            return true;
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<IRabbitConfigurationProvider, RabbitConfigurationProvider>();
            RabbitDependencies.RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}