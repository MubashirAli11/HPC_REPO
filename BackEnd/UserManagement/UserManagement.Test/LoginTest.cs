using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.API.CommandHandlers;
using UserManagement.API.Commands;
using UserManagement.Core.Entities;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Context;
using Xunit;

namespace UserManagement.Test
{
    public class LoginTest
    {
        [Fact]
        public async void Invalid_Email_Test()
        {
            var options = new DbContextOptionsBuilder<UserManagementDbContext>()
            .UseInMemoryDatabase(databaseName: "UserManagementDB")
            .Options;

            var context = new UserManagementDbContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);


            var user = new Core.Entities.UserEntity("Admin", "admin@gmail.com", "3456789");

            PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();

            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

            user.SecurityStamp = user.PasswordHash;

            unitOfWork.UserRepository.Add(user);

            await unitOfWork.SaveChangesAsync();

            LoginCommand command = new LoginCommand();

            command.Code = "aBgH-3421-k8";
            command.Name = "ship2";
            command.Width = 20;
            command.Length = 20;

            LoginCommandHandler commandHandler = new LoginCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());


            string expectedOutput = "Record with same code already exists";

            Assert.False(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }


        [Fact]
        public void Invalid_Password_Test()
        {

        }

        [Fact]
        public void User_Not_Exist_Test()
        {

        }

        [Fact]
        public void Successful_Login_Test()
        {

        }
    }
}