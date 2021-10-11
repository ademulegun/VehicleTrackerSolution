using System;

namespace VehicleTracker.Core.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message) : base(message)
        {

        }
    }
}
