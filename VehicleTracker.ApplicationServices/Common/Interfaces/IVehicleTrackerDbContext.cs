using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.ApplicationServices.Common.Interfaces
{
    public interface IVehicleTrackerDbContext
    {
        DbSet<Core.Entities.Vehicle> Vehicle { get; set; }
        DbSet<VehicleState> VehicleState { get; set; }
        DbSet<User> User { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
