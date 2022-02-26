using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ship.API.Behaviors;
using Ship.API.CommandValidators;
using Ship.API.ExceptionHandler;
using Ship.Core.IRepositories;
using Ship.Infrastructure;
using Ship.Infrastructure.Context;
using Ship.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddCors(options =>
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

AddApiVersioningConfigured(builder.Services);

RegisterServices(builder.Services);


builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

void RegisterServices(IServiceCollection services)
{
    services.AddDbContext<ShipDataContext>(opt => opt.UseInMemoryDatabase("ShipDB"));

    services.AddMvc()
   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateShipCommandValidator>());

    services.AddControllers().AddNewtonsoftJson();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IShipRepository, ShipRepository>();
    services.AddMediatR(Assembly.GetExecutingAssembly());

   

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

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();


//app.UseAuthorization();

app.MapControllers();

app.Run();
