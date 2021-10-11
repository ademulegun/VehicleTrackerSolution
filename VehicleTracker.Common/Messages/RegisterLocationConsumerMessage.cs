using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Common.Messages
{
    public class RegisterLocationConsumerMessage
    {
        public string UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
