using FluentValidation;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Weasel.Core;
using Npgsql;
using Catalog.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    //cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

#region Auth Services
var rsa = new RSACryptoServiceProvider(2048);
var publicKey = new RsaSecurityKey(rsa.ExportParameters(false));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });

builder.Services.AddAuthorization();
#endregion

#region Carter
builder.Services.AddCarter();
#endregion

#region Marten
builder.Services.AddMarten(cfg =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database")!;
    cfg.Connection(connectionString!);
    cfg.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

    cfg.CreateDatabasesForTenants(c =>
    {
        var connnectionBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        connnectionBuilder.Database = "catalogdb";
        string updatedConnectionString = connnectionBuilder.ConnectionString;
        c.MaintenanceDatabase(updatedConnectionString);
        c.ForTenant()
        .CheckAgainstPgDatabase()
            .WithOwner("catalogdb")
            .WithEncoding("UTF-8")
            .ConnectionLimit(-1)
            .OnDatabaseCreated(_ => { });
    }
    );
}
)
.UseLightweightSessions();
#endregion

#region Development Environment Settings
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

#endregion

#region Exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

#region HealthChecks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
#endregion

#region Mappster
MappingExtension.RegisterMappings();
#endregion

var app = builder.Build();

app.MapCarter();
#region Auth
app.UseAuthentication();
app.UseAuthorization();
#endregion

#region Exception
app.UseExceptionHandler(options => { });
#endregion

#region HealthChecks
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
#endregion

#region File Storage
app.UseStaticFiles();
#endregion




app.Run();