using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Infrastructure.Messaging
{
    public static class MessageBusConfiguration
    {
        public static void ConfigureBroker(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            var dictionary = new Dictionary<string, Func<IServiceCollection, IConfiguration, IServiceCollection>>
            {
                { Environments.Development, ConfigureRabbitMqOverMasstransit.ConfigureBus}
            };
            dictionary[env.EnvironmentName](services, configuration);
        }
    }
}
