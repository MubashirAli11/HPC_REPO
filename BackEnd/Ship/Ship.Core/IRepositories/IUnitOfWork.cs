using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Core.IRepositories
{
    public interface IUnitOfWork
    {
        IShipRepository ShipRepository { get; }
        Task<bool> SaveChangesAsync();
        void Dispose();
    }
}
