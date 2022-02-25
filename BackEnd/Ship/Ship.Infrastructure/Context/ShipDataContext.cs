using Microsoft.EntityFrameworkCore;
using Ship.Core.Entities;
using Ship.Infrastructure.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Infrastructure.Context
{
    public class ShipDataContext : DbContext
    {
        public ShipDataContext(DbContextOptions<ShipDataContext> options) : base(options)
        {

        }

        public DbSet<ShipEntity> Ships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShipEntityConfiguration());
        }
    }
}
