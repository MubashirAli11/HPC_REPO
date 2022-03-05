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
        public async void Create_Ship_With_Valid_Code_Format()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB_Create_Test1")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            CreateShipCommand command = new CreateShipCommand();

            command.Code = "ahjl-3621-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;

            CreateShipCommandHandler commandHandler = new CreateShipCommandHandler(unitOfWork);

            var response = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            string expectedOutput = "Success";

            Assert.True(response.IsSuccess);
            Assert.Equal(response.Message, expectedOutput);
        }


        [Fact]
        public async void Create_Ship_With_Existing_Code()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB_Create_Test2")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            unitOfWork.ShipRepository.Add(new Core.Entities.ShipEntity("Ship1", 10, 10, "aBgH-3421-k8"));
            await unitOfWork.SaveChangesAsync();

            CreateShipCommand command = new CreateShipCommand();

            command.Code = "aBgH-3421-k8";
            command.Name = "ship2";
            command.Width = 20;
            command.Length = 20;

            CreateShipCommandHandler commandHandler = new CreateShipCommandHandler(unitOfWork);

            var response = await commandHandler.Handle(command, new System.Threading.CancellationToken());


            string expectedOutput = "Record with same code already exists";

            Assert.False(response.IsSuccess);
            Assert.Equal(response.Message, expectedOutput);
        }
    }
}