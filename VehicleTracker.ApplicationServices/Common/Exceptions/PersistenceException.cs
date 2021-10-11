using System;

namespace VehicleTracker.ApplicationServices.Common.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message) : base(message)
        {

        }
    }
}
