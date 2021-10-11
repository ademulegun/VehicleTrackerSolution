using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.Persistence.Configurations
{
    public class VehicleStateDbConfiguration : IEntityTypeConfiguration<VehicleState>
    {
        public void Configure(EntityTypeBuilder<VehicleState> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).HasConversion<int>();
            builder.Property(x => x.SyncDate).HasColumnType("datetime2");
            builder.HasOne(x => x.Vehicle).WithMany(x => x.VehicleDetails).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.VehicleId);
            builder.OwnsOne(x => x.CarLocation).Property(x => x.Longitude).HasColumnType("varchar(50)");
            builder.OwnsOne(x => x.CarLocation).Property(x => x.Latitude).HasColumnType("varchar(50)");
        }
    }
}
