using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PaymentSystems.FrameWork;
using PaymentSystems.WebAPI.Application;
using PaymentSystems.WebAPI.Infrastructure;

namespace PaymentSystems.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton(
                ConfigureMongo(
                    Configuration["MongoDb:ConnectionString"],
                    Configuration["MongoDb:Database"]
                )
            );

            services.AddSingleton(
                ctx
                    => ConfigureEventStore(
                        Configuration["EventStore:ConnectionString"],
                        ctx.GetService<ILoggerFactory>()
                    )
            );

            services.AddSingleton<IAggregateStore, EsDbAggregateStore>();

            // Application
            //BookingEventMappings.MapEvents();
            
            services.AddSingleton<PaymentCommandService>();
            services.AddSingleton<AccountCommandServices>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentSystems.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentSystems.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static IMongoDatabase ConfigureMongo(string connectionString, string database) {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            return new MongoClient(settings).GetDatabase(database);
        }

        static EventStoreClient ConfigureEventStore(string connectionString, ILoggerFactory loggerFactory) {
            var settings = EventStoreClientSettings.Create(connectionString);
            settings.ConnectionName = "paymentApp";
            settings.LoggerFactory  = loggerFactory;
            return new EventStoreClient(settings);
        }

    }
}
