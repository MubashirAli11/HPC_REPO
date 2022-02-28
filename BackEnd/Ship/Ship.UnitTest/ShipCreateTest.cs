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

            Assert.False(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }

        [Fact]
        public async void Crate_Ship_With_Valid_Code_Format()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            CreateShipCommand command = new CreateShipCommand();

            command.Code = "ahjs-3421-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;

            CreateShipCommandHandler commandHandler = new CreateShipCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            string expectedOutput = "Success";

            Assert.True(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }


        [Fact]
        public async void Crate_Ship_With_Existing_Code()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
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

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());


            string expectedOutput = "Record with same code already exists";

            Assert.False(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }
    }
}