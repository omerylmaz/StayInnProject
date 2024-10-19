using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("yarp.json", optional: false, reloadOnChange: true);

//builder.Configuration.AddJsonFile($"yarp.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseRateLimiter();

app.MapReverseProxy();

app.UseCors("AllowAllOrigins");

app.UseStaticFiles();

app.Run();
