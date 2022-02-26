using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ship.API.CommandHandlers;
using Ship.API.Commands;
using Ship.Core.IRepositories;
using Ship.Infrastructure;
using Ship.Infrastructure.Context;
using Xunit;

namespace Ship.UnitTest
{
    public class ShipCreateTest
    {
        [Fact]
        public async void Crate_Ship_With_Invalid_Code_Format()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
             .Options;

            var context = new ShipDataContext(options);

            string expectedOutput = "Code should follow this format AAAA-1111-A1 A: Alphabets 1: 0-9";

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            CreateShipCommand command = new CreateShipCommand();

            command.Code = "ahjs-hjkd-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;

            CreateShipCommandHandler commandHandler = new CreateShipCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.True(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }
    }
}