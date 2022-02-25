using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ship.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Infrastructure.DbConfiguration
{
    internal class ShipEntityConfiguration : IEntityTypeConfiguration<ShipEntity>
    {
        public void Configure(EntityTypeBuilder<ShipEntity> builder)
        {
            //Defining the primay key

            builder.HasKey(x => x.Id);

            //Added required fields configuration

            builder.Property(x => x.Width).IsRequired();
            builder.Property(x => x.Length).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            //Added max length fields configuration

            builder.Property(x => x.Code).HasMaxLength(12);
            builder.Property(x => x.Name).HasMaxLength(500);
        }
    }
}
