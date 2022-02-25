using Ship.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Core.IRepositories
{
    public interface IShipRepository : IGenericRepository<ShipEntity, int>
    {
    }
}
