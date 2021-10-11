using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Core.Events
{
    public static class UserEvents
    {
        public class UserCreated
        {
            public Guid UserId{get;set;}
            public string Name { get; set; }
        }
    }
}
