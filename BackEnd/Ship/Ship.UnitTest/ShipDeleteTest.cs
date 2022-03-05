using Microsoft.EntityFrameworkCore;
using Ship.API.CommandHandlers;
using Ship.API.Commands;
using Ship.Core.IRepositories;
using Ship.Infrastructure;
using Ship.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ship.UnitTest
{
    public class ShipDeleteTest
    {
        [Fact]
        public async void Delete_Ship_With_Not_Existing_Id()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB_Delete_Test1")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            //Add new record

            DeleteShipCommand command = new DeleteShipCommand();

            command.Id = 1;

            DeleteShipCommandHandler commandHandler = new DeleteShipCommandHandler(unitOfWork);

            var response = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            string expectedOutput = "Can't find record";

            Assert.False(response.IsSuccess);
            Assert.Equal(response.Message, expectedOutput);
        }

        [Fact]
        public async void Delete_Ship_With_Valid_Id()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
              .UseInMemoryDatabase(databaseName: "ShipDB_Delete_Test2")
              .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            //Add new record

            unitOfWork.ShipRepository.Add(new Core.Entities.ShipEntity("Ship1", 10, 10, "aBgH-3421-n8"));
            await unitOfWork.SaveChangesAsync();

     

            DeleteShipCommand command = new DeleteShipCommand();

            command.Id = 1;

            DeleteShipCommandHandler commandHandler = new DeleteShipCommandHandler(unitOfWork);

            var response = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            string expectedOutput = "Success";

            Assert.True(response.IsSuccess);
            Assert.Equal(response.Message, expectedOutput);
        }
    }
}
