using AutoMapper;
using Ship.API.ApiModels;
using Ship.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.API.Mapper
{
    public class Automapper : Profile
    {

        public Automapper()
        {
            CreateMap<ShipEntity, ShipApiModel>();
        }
    }
}
