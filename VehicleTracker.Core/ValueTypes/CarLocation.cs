using VehicleTracker.Core.Exceptions;

namespace VehicleTracker.Core.Entities
{
    public record CarLocation
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public CarLocation(string longitude, string latitude)
        {
            if (string.IsNullOrEmpty(longitude))
                throw new InvalidEntityLocation("Invalid longitude");
            if (string.IsNullOrEmpty(latitude))
                throw new InvalidEntityLocation("Invalid latitude");
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
