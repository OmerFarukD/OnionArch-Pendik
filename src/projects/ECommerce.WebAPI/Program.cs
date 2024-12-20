using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Serilog.Loggers;
using Core.Security;
using Core.Security.Encryption;
using Core.Security.JWT;
using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.CloudinaryServices;
using ECommerce.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<LoggerServiceBase, FileLogger>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(opt=> opt.Configuration="localhost:6381");
builder.Services.AddApplicationServiceDependencies();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddSecurityServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureDependencies(builder.Configuration);



const string tokenOptionsConfigurationName = "TokenOptions";
TokenOptions tokenOptions = builder.Configuration.GetSection(tokenOptionsConfigurationName).Get<TokenOptions>()
                            ?? throw new InvalidOperationException(
                                $"{tokenOptionsConfigurationName} section bulunamadı");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
        }
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.ConfigureCustomExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // kimlik doğrulama 
app.UseAuthorization(); // yetkile

app.MapControllers();

app.Run();
