using System;

namespace VehicleTracker.ApplicationServices.Common.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(message)
        {

        }
    }
}
