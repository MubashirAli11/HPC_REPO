using Ship.Core.IRepositories;
using Ship.Infrastructure.Context;
using Ship.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ShipDataContext _context;

        public IShipRepository ShipRepository { get; private set; }

        public UnitOfWork(ShipDataContext context)
        {
            _context = context;
            this.ShipRepository = new ShipRepository(context);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
