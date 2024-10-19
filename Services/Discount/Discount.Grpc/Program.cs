//using Discount.Grpc.Services;

using Discount.Grpc.Data;
using Discount.Grpc.Data.Validations;
using Discount.Grpc.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ValidationInterceptor>();
});
builder.Services.AddValidatorsFromAssemblyContaining<CouponModelValidation>();

builder.Services.AddDbContext<DiscountContext>(opt => 
opt.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.UseMigration();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
