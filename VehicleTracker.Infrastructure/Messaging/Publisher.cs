using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;

namespace VehicleTracker.Infrastructure.Messaging
{
    public class Publisher : IPublisher
    {
        private readonly IBus _bus;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public Publisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message)
        {
            await Task.Run(() =>
            {
                _bus.Publish(message, _cancellationTokenSource.Token);
            });
        }
    }
}
