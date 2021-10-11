using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleTracker.Common.Model
{
    public class AuthenticationResult
    {
        internal AuthenticationResult(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static AuthenticationResult Success()
        {
            return new AuthenticationResult(true, new string[] { });
        }

        public static AuthenticationResult Failure(IEnumerable<string> errors)
        {
            return new AuthenticationResult(false, errors);
        }
    }
}
