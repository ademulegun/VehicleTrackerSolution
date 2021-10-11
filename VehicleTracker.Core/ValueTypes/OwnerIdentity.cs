using VehicleTracker.Core.Exceptions;

namespace VehicleTracker.Core.ValueTypes
{
    public record OwnerIdentity
    {
        public string UserId { get; set; }
        protected OwnerIdentity() { }
        public OwnerIdentity(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidEntityIdException("Id is notpassed or is invalid");
            UserId = userId;
        }
    }
}
