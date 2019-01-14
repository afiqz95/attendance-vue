using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class ConfigHelper
    {
        private static IConfigurationBuilder builder;

        public static IConfiguration GetConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appSettings.json", true, reloadOnChange: true).AddJsonFile($"appSettings.{env}.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static string GetSqlConnString()
        {
            return GetConfig().GetConnectionString("DefaultConnection");
        }
    }
}
