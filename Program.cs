using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfigurationChanges.ConfigProvider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                .UseStartup<Startup>();
    }
}
