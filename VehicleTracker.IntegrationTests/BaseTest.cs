using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Azure.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using VehicleTracker.ApplicationServices.Common.Exceptions;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Common.Options;
using VehicleTracker.Infrastructure.Identity;
using VehicleTracker.Infrastructure.IdentityServer;
using VehicleTracker.Infrastructure.Infrastructure;
using VehicleTracker.Persistence;
using VehicleTrackerAPI;

namespace VehicleTracker.IntegrationTests
{
    public class BaseTest: WebApplicationFactory<Startup>
    {
        private readonly HttpClient _httpClient;
        public IConfiguration Configuration { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                var connectionString = Configuration.GetConnectionString("VehicleTrackerConnection");
                services.AddDbContext<VehicleTrackerDbContext>(options =>
                    options.UseSqlServer(connectionString,
                        b => b.MigrationsAssembly(typeof(VehicleTrackerDbContext).Assembly.FullName)));
                services.AddScoped<IVehicleTrackerDbContext>(provider => provider.GetService<VehicleTrackerDbContext>());
                services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                }).AddEntityFrameworkStores<VehicleTrackerDbContext>().AddDefaultTokenProviders();
                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                    options.SignIn.RequireConfirmedEmail = true;
                });
                services.AddMediatR(Assembly.GetExecutingAssembly());
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehaviour<,>));
                services.AddSingleton<IDictionary<string, string>>(opts => new Dictionary<string, string>());
                services.Configure<IdentityServerConfig>(Configuration.GetSection("IdentityServer"));
                services.Configure<SignalROptions>(Configuration.GetSection("Signalr"));
                services.AddHttpClient<IdentityHttpClient>();
                services.AddTransient<IIdentityService, IdentityService>();
                services.AddTransient<IIdentityServerService, IdentityServerService>();
                services.AddTransient<IIdentityService, IdentityService>();
            });
        }
    }
}