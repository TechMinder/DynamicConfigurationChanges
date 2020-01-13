using DynamicConfigurationChanges.ConfigProvider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DynamicConfigurationChanges
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                
                .ConfigureAppConfiguration((hostingContext, config) => {

                    config.AddInMemoryCollection(new Dictionary<string, string>() { { "external:websitename", "localhost" }, { "external:owner", "self" } });
                    config.AddHttpConfiguration();
                })
                .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                .UseStartup<Startup>();
    }
}
