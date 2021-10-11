using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Common.Options;
using VehicleTracker.ApplicationServices.ViewModel;
using VehicleTracker.Infrastructure.Identity;
using VehicleTracker.Infrastructure.IdentityServer;
using VehicleTracker.Infrastructure.Infrastructure;
using VehicleTracker.Infrastructure.Messaging;

namespace VehicleTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddSingleton<IDictionary<string, string>>(opts => new Dictionary<string, string>());
            services.Configure<IdentityServerConfig>(configuration.GetSection("IdentityServer"));
            services.Configure<JwtDto>(configuration.GetSection("JWT"));
            services.AddHttpClient<IdentityHttpClient>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IIdentityServerService, IdentityServerService>();
            services.AddTransient<IIdentityUserManagement, IdentityService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.RequireHttpsMetadata = false;
                 o.SaveToken = false;
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     ValidIssuer = configuration["JWT:Issuer"],
                     ValidAudience = configuration["JWT:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                 };
              });
            services.AddTransient<IPublisher, Publisher>();
            services.ConfigureBroker(env, configuration);
            return services;
        }
    }
}
