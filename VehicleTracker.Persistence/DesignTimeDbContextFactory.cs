using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace VehicleTracker.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VehicleTrackerDbContext>
    {
        public VehicleTrackerDbContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secrets.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<VehicleTrackerDbContext>();
            var connectionString = configuration.GetConnectionString("VehicleTrackerConnection");
            builder.UseSqlServer(connectionString);
            return new VehicleTrackerDbContext(builder.Options);
        }
    }
}