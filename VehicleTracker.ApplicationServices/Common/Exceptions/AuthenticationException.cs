using System;

namespace VehicleTracker.ApplicationServices.Common.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {

        }
    }
}
