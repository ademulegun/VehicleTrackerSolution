using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ApplicationServices.Common.Options
{
    public class IdentityServerConfig
    {
        public string Url { get; set; }
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
    }
}
