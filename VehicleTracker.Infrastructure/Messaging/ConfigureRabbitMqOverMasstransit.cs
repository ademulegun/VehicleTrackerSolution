using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Infrastructure.Subscribers;

namespace VehicleTracker.Infrastructure.Messaging
{
    public static class ConfigureRabbitMqOverMasstransit
    {
        internal static IServiceCollection ConfigureBus(this IServiceCollection services, IConfiguration config)
        {
            var username = config["RabbitMQ:Username"];
            var password = config["RabbitMQ:Password"];
            var rabbitMqUrl = config["RabbitMQ:Url"];

            services.AddMassTransit(x =>
            {
                x.AddConsumer<RegisterLocationConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitMqUrl), h => {
                        h.Username(username);
                        h.Password(password);
                    });
                    cfg.ReceiveEndpoint("CustomerDocumentSubmittedQueue", e =>
                    {
                        e.Durable = true;
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(3, 2000));
                        e.UseCircuitBreaker(cb =>
                        {
                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(3);
                        });
                        e.Consumer<RegisterLocationConsumer>(provider);
                        e.DiscardSkippedMessages();
                        e.DiscardFaultedMessages();
                    });
                }));
            });
            services.AddSingleton<IHostedService, BusService>();
            return services;
        }
    }
}
