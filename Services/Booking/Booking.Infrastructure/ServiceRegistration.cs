using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Booking.Application.Data;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Booking.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, PublishDomainEventsInterceptor>();

            services.AddScoped<IBookingDbContext, BookingDbContext>(); //TODO Burası ileride silinecek repository pattern gelecek
            services.AddDbContext<BookingDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            return services;
        }

        public static WebApplication UseInfrastructureServices(this WebApplication app, IConfiguration configuration)
        {
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
                context.Database.MigrateAsync().GetAwaiter().GetResult();
                SeedData.AddSeededDatas(context).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
