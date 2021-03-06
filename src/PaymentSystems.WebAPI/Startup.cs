
using System.Text.Json;
using EventStore.Client;
using Eventuous;
using Eventuous.EventStoreDB;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PaymentSystems.WebAPI.Application;
using PaymentSystems.WebAPI.Features;
using PaymentSystems.WebAPI.Features.Accounts;
using PaymentSystems.WebAPI.Features.Payments;
using PaymentSystems.WebAPI.Integration;

namespace PaymentSystems.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        IConfiguration Configuration { get; }

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

            services.AddSingleton(new EventStoreClient(
                EventStoreClientSettings.Create(Configuration["EventStore:ConnectionString"])
            ));
            services.AddSingleton<IEventSerializer>(
                new DefaultEventSerializer(
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                )
            );
     
            
            services.AddSingleton<IAggregateStore, AggregateStore>();
            services.AddSingleton<IEventStore, EsdbEventStore>();


            services.AddSingleton<ICheckpointStore, MongoCheckpointStore>();
            // Application
            //BookingEventMappings.MapEvents();
            
            //Command Services
            services.AddSingleton<PaymentCommandService>();
            services.AddSingleton<AccountCommandService>();
            services.AddSingleton<TransactionsCommandService>();

            //Reactors
            services.AddHostedService<AccountReactorSubscription>();
            //services.AddHostedService<IntegrationReactorSubscription>();
            services.AddHostedService<TransactionReactorSubscription>();
            
            //Projections
            services.AddHostedService<AccountProjectionSubscription>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentSystems.WebAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentSystems.WebAPI v1"));
            }

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
