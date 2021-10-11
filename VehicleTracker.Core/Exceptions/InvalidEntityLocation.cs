using System;

namespace VehicleTracker.Core.Exceptions
{
    public class InvalidEntityLocation : Exception
    {
        public InvalidEntityLocation(string message) : base(message)
        {

        }
    }
}
