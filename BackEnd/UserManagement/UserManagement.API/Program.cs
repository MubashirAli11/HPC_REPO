using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UserManagement.API.CommandValidators;
using UserManagement.API.ExceptionHandler;
using UserManagement.Core.Entities;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

RegisterServices(builder.Services);

AddApiVersioningConfigured(builder.Services);

void RegisterServices(IServiceCollection services)
{

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<UserManagementDbContext>(opt => opt.UseInMemoryDatabase("UserManagementDB"));

    services.AddMvc()
   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginCommandValidator>());

    services.AddControllers().AddNewtonsoftJson();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddMediatR(Assembly.GetExecutingAssembly());

    services.AddScoped<AuthenticationServices>();

    services.AddScoped<JwtSecurityTokenHandler>();

    services.AddIdentity<UserEntity, IdentityRole<string>>()
          .AddEntityFrameworkStores<UserManagementDbContext>()
          .AddUserManager<UserManager<UserEntity>>()
          .AddDefaultTokenProviders();

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

await SeedUsers();

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


async Task SeedUsers()
{

    var options = new DbContextOptionsBuilder<UserManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "UserManagementDB")
             .Options;

    using (var dbcontext = new UserManagementDbContext(options))
    {
        UserEntity superAdmin = new UserEntity("SuperAdmin", "super_admin@gmail.com",
                                "3456789", UserManagement.Core.Enums.UserTypes.SuperAdmin);
        superAdmin.AddId();

        PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();

        superAdmin.PasswordHash = passwordHasher.HashPassword(superAdmin, "SuperAdmin@123");

        superAdmin.SecurityStamp = superAdmin.PasswordHash;

        dbcontext.Add(superAdmin);


        UserEntity user = new UserEntity("Admin", "admin@gmail.com", "3456789",
                          UserManagement.Core.Enums.UserTypes.Admin);
        user.AddId();
   
        user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

        user.SecurityStamp = user.PasswordHash;

        dbcontext.Add(user);

       await dbcontext.SaveChangesAsync();
    }
}


