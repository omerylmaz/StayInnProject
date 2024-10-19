using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Booking.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddFeatureManagement();
            services.AddMessageBrokerServices(configuration, Assembly.GetExecutingAssembly());

            return services;
        }

        public static WebApplication UseApplicationServices(this WebApplication app)
        {
            return app;
        }
    }
}
