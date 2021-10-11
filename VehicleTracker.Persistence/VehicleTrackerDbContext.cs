using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.Core.Entities;
using VehicleTracker.Infrastructure.Identity;

namespace VehicleTracker.Persistence
{
    public class VehicleTrackerDbContext: IdentityDbContext<ApplicationUser>, IVehicleTrackerDbContext
    {
        public VehicleTrackerDbContext(DbContextOptions<VehicleTrackerDbContext> options): base(options) { }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VehicleState> VehicleState { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}