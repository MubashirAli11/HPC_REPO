using Microsoft.EntityFrameworkCore;
using Ship.Core.Entities;
using Ship.Core.IRepositories;
using Ship.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Infrastructure.Repositories
{
    public class ShipRepository : GenericRepository<ShipEntity, int>, IShipRepository
    {
        public ShipRepository(ShipDataContext context) : base(context)
        {
        }

        public override async Task<bool> Update(int id, ShipEntity entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    throw new Exception("Can't find record");

                existingUser.Update(entity.Name, entity.Length, entity.Width, entity.Code);

                return true;
            }
            catch (Exception)
            {
                //TODO:
                //Add exceptions
                return false;
            }
        }


        public async Task<bool> Exists(string code)
        {
            return await dbSet.AnyAsync(x => x.Code == code);
        }
    }
}
