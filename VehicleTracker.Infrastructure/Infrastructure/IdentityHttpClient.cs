using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Options;

namespace VehicleTracker.Infrastructure.Infrastructure
{
    public class IdentityHttpClient
    {
        public readonly HttpClient _client;
        public IdentityHttpClient(HttpClient client)
        {
            _client = client;
        }
    }
}
