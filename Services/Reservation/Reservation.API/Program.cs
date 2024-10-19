using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Reservation.API.Data;
using BuildingBlocks.Messaging;
using Weasel.Core;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(cfg =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database")!;
    cfg.Connection(connectionString!);
    cfg.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    cfg.Schema.For<ReservationModel>().Identity(x => x.Id);
    cfg.CreateDatabasesForTenants(c =>
    {
        var connnectionBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        connnectionBuilder.Database = "postgres";
        string updatedConnectionString = connnectionBuilder.ConnectionString;
        c.MaintenanceDatabase(updatedConnectionString);
        c.ForTenant()
        .CheckAgainstPgDatabase()
            .WithOwner("postgres")
            .WithEncoding("UTF-8")
            .ConnectionLimit(-1)
            .OnDatabaseCreated(_ => { });
    }
        );
}
)
.UseLightweightSessions();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//builder.Services.CreateDatabaseIfNotExists(builder.Configuration);

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.Decorate<IReservationRepository, CachedReservationRepository>();

builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
    //.AddRabbitMQ(opt =>
    //{
    //    var rabbitMqConnectionString = $"{builder.Configuration["MessageBroker:Host"]}?username={builder.Configuration["MessageBroker:UserName"]}&password={builder.Configuration["MessageBroker:Password"]}";
    //    opt.ConnectionUri = new Uri(rabbitMqConnectionString);
    //});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMessageBrokerServices(builder.Configuration);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});
    //.ConfigurePrimaryHttpMessageHandler(() =>
    //{
    //    var handler = new HttpClientHandler
    //    {
    //        ServerCertificateCustomValidationCallback =
    //        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    //    };

    //    return handler;
    //});


//builder.Services.AddGrpcClient<DiscountProtoServiceClient>(o =>
//{
//    o.Address = new Uri("http://discount.grpc:8080");
//})
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var handler = new HttpClientHandler
//        {
//            ServerCertificateCustomValidationCallback =
//            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//        };

//        return handler;
//    });

var app = builder.Build();

app.MapCarter();

app.UseHealthChecks("/health", new HealthCheckOptions() 
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseExceptionHandler(options => { });

app.Run();
