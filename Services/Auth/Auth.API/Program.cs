using Auth.API.Data;
using Auth.API.Data.Models;
using Auth.API.Services.Token;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Endpoints;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using TokenHandler = Auth.API.Services.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);



#region Carter
builder.Services.AddCarter();
#endregion
#region Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
//});

#endregion
#region MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
#endregion

#region Endpoints
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
#endregion

#region Auth Services
builder.Services.AddDbContext<AuthDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Database")!);
    
});



builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.User.AllowedUserNameCharacters = null;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false 
        };
    });

//builder.Services.AddAuthorization();

builder.Services.AddScoped<ITokenHandler, TokenHandler>();
#endregion

#region Logging
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
#endregion



#region Exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

var app = builder.Build();

#region Auth Database
if (builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        dbContext.Database.Migrate();
    }
}
#endregion

#region Swagger
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
//    c.RoutePrefix = string.Empty;
//});
#endregion
#region Exception
app.UseExceptionHandler(options => { });
#endregion

#region Auth
app.UseAuthentication();
//app.UseAuthorization();
#endregion
#region Endpoints
//app.MapEndpoints();
#endregion
app.MapCarter();





app.Run();
