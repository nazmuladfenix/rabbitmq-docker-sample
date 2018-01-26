using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Domain.Entities;
using RabbitMQ.Shared;
using RabbitMQ.Shared.Infrastructure;


namespace Consumer.Service
{
    public class Startup
    {
        private const string QueueName = "docker.test.queue";
        private const string ExchangeName = "docker.test.exchange";

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
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("Consumer.Service")));
            services.AddMvc();
            this.ConfigureDependencies(services);
            var sp = services.BuildServiceProvider();
            this._serviceProvider = sp;
            var rabbitReceiver = sp.GetService<IRabbitMQReceiver>();

            try
            {
                Thread.Sleep(15000);

                rabbitReceiver.CreateReceiver<Person>(this.HandleMessage, QueueName, ExchangeType.Direct, ExchangeName);
                Console.WriteLine("==========Rabbit receiver up=========");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Person", action = "Get" }
                );
            });
        }
    }
}