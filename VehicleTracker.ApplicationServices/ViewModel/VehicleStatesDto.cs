using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.ApplicationServices.ViewModel
{
    public class VehicleStatesDto
    {
        public Guid Id { get; set; }
        public DateTime SyncDate { get; set; }
        public Guid VehicleId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Created { get; set; }

        public static Expression<Func<VehicleState, VehicleStatesDto>> Projection
        {
            get
            {
                return c => new VehicleStatesDto
                {
                    Id = c.Id,
                    SyncDate = c.SyncDate,
                    VehicleId = c.VehicleId,
                    Longitude = c.CarLocation.Longitude,
                    Latitude = c.CarLocation.Latitude,
                    Created = c.Created
                };
            }
        }
    }
}
