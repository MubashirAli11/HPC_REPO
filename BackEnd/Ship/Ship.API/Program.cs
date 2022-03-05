using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ship.API.Authorization;
using Ship.API.CommandValidators;
using Ship.API.ExceptionHandler;
using Ship.API.Mapper;
using Ship.Core.IRepositories;
using Ship.Infrastructure;
using Ship.Infrastructure.Context;
using Ship.Infrastructure.Repositories;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

RegisterServices(builder.Services);

AddApiVersioningConfigured(builder.Services);

void RegisterServices(IServiceCollection services)
{
    services.AddDbContext<ShipDataContext>(opt => opt.UseInMemoryDatabase("ShipDB"));

    services.AddMvc()
   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateShipCommandValidator>());

    services.AddControllers().AddNewtonsoftJson();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IShipRepository, ShipRepository>();
    services.AddMediatR(Assembly.GetExecutingAssembly());

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ship API 1.0", Version = "1.0" });
        c.CustomSchemaIds(type => type.ToString());
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
    });

    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JwtIssuerSettings:Key"]);
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuerSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtIssuerSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Key)
        };
    });

    services.AddAuthorization()
    .AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultTransformer>();


    services.AddAutoMapper(c => c.AddProfile<Automapper>());

    services.AddCors(options =>
    {
        // options.use
        options.AddPolicy("AllowAllOrigins",
                          corebuilder =>
                          {
                              corebuilder
                                  .AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                          });
    });
}

void AddApiVersioningConfigured(IServiceCollection services)
{
    services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);

    });

}

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
