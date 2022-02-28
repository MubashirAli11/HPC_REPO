﻿using Microsoft.EntityFrameworkCore;
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
    public class ShipUpdateTest
    {
        [Fact]
        public async void Update_Ship_With_Invalid_Code_Format()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
             .Options;

            var context = new ShipDataContext(options);

            string expectedOutput = "Code should follow this format AAAA-1111-A1 A: Alphabets 1: 0-9";

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            //Add new record


            unitOfWork.ShipRepository.Add(new Core.Entities.ShipEntity("Ship1", 10, 10, "aBgH-3421-k8"));
            await unitOfWork.SaveChangesAsync();


            UpdateShipCommand command = new UpdateShipCommand();

            command.Code = "ahjs-hjkd-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;
            command.Id = 1;

            UpdateShipCommandHandler commandHandler = new UpdateShipCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.False(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }

        [Fact]
        public async void Update_Ship_With_Valid_Code_Format()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            //Add new record

            unitOfWork.ShipRepository.Add(new Core.Entities.ShipEntity("Ship1", 10, 10, "aBgH-3421-k8"));
            await unitOfWork.SaveChangesAsync();

            UpdateShipCommand command = new UpdateShipCommand();

            command.Code = "ahjs-3421-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;
            command.Id = 1;

            UpdateShipCommandHandler commandHandler = new UpdateShipCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            string expectedOutput = "Success";

            Assert.True(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }

        [Fact]
        public async void Update_Ship_With_Not_Existing_Id()
        {

            var options = new DbContextOptionsBuilder<ShipDataContext>()
             .UseInMemoryDatabase(databaseName: "ShipDB")
             .Options;

            var context = new ShipDataContext(options);

            IUnitOfWork unitOfWork = new UnitOfWork(context);

            UpdateShipCommand command = new UpdateShipCommand();

            command.Code = "ahjs-3421-a0";
            command.Name = "ship1";
            command.Width = 10;
            command.Length = 10;
            command.Id = 1;

            UpdateShipCommandHandler commandHandler = new UpdateShipCommandHandler(unitOfWork);

            var respone = await commandHandler.Handle(command, new System.Threading.CancellationToken());


            string expectedOutput = "Can't find record";

            Assert.False(respone.IsSuccess);
            Assert.Equal(respone.Message, expectedOutput);
        }
    }
}
