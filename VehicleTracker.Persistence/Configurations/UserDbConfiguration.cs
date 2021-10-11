using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.Persistence.Configurations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired(true);
            builder.OwnsOne(x => x.OwnerIdentity).Property(x => x.UserId).HasColumnType("varchar(50)").IsRequired(false);
            builder.Property(x => x.Email).HasColumnType("varchar(100)").IsRequired(false);
        }
    }
}
