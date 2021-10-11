using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracker.Infrastructure.Messaging
{
    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly ILogger<BusService> _logger;

        public BusService(IBusControl busControl, ILogger<BusService> logger)
        {
            _busControl = busControl;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started bus for Tracker");

            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped bus for Tracker");

            return _busControl.StopAsync(cancellationToken);
        }
    }
}
