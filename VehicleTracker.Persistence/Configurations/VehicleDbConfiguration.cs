using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.Persistence.Configurations
{
    public class VehicleDbConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.DeviceNumber).IsUnique();
            builder.Property(x => x.DeviceNumber).HasColumnType("varchar(50)").IsRequired(true);
            builder.OwnsOne(x => x.OwnerIdentity).Property(x => x.UserId).HasColumnType("varchar(50)").IsRequired(false);
            builder.HasMany(x => x.VehicleDetails).WithOne(x => x.Vehicle).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.VehicleId).IsRequired(true);
            builder.Property(x => x.Model).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.Color).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.FuelCapacity).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.Speed).HasColumnType("varchar(50)").IsRequired(false);
            builder.Property(x => x.CurrentStatus).HasConversion<int>().IsRequired(true);
            builder.Property(x => x.LastSync).HasColumnType("datetime2").IsRequired(true);
        }
    }
}
