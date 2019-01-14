using AttendanceSystem.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AttendanceSystemIdentityDbContext>
    {
        public AttendanceSystemIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AttendanceSystemIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("AttendanceSystemIdentityDbContextConnection");
            builder.UseSqlServer(connectionString);
            return new AttendanceSystemIdentityDbContext(builder.Options);
        }
    }
}
