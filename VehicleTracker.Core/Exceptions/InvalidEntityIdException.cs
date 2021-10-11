using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Core.Exceptions
{
    public class InvalidEntityIdException: Exception
    {
        public InvalidEntityIdException(string message): base(message)
        {

        }
    }
}
