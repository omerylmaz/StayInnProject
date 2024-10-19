using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Booking.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);
                //.AddRabbitMQ(opt =>
                //    {
                //        var rabbitMqConnectionString = $"{configuration["MessageBroker:Host"]}?username={configuration["MessageBroker:UserName"]}&password={configuration["MessageBroker:Password"]}";
                //        opt.ConnectionUri = new Uri(rabbitMqConnectionString);
                //    });
            services.AddCarter();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapCarter();

            return app;
        }
    }
}
