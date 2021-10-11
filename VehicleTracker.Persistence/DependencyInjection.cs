using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.Infrastructure.Identity;

namespace VehicleTracker.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VehicleTrackerDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("VehicleTrackerConnection"),
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
            return services;
        }
    }
}
