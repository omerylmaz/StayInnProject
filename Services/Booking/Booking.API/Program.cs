using Booking.API;
using Booking.Application;
using Booking.Infrastructure;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app
    .UseApiServices()
    .UseInfrastructureServices(builder.Configuration);

app.Run();
