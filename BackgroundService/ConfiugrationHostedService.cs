using DynamicConfigurationChanges.ConfigProvider;
using DynamicConfigurationChanges.ConfigurationChanges;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicConfigurationChanges.HostedService
{
    public class ConfiugrationHostedService : BackgroundService
    {
        private readonly ILogger<ConfiugrationHostedService> _logger;
        private readonly IOptions<ConfiugrationApiSettings> _settings;
        private readonly IConfiguration _configuration;




        public ConfiugrationHostedService(IOptions<ConfiugrationApiSettings> settings,
                                         IConfiguration configuration,
                                         ILogger<ConfiugrationHostedService> logger)
        {
            _settings = settings;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"Configuration API call is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($"Configuration API background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"Configuration API task loading configuraiton from API.");

                //Cancel the token upon successful retrieval
                await LoadConfiguration();

                await Task.Delay(_settings.Value.TaskCancellantionTimeoutMS, stoppingToken);
            }

            _logger.LogDebug($"Configuration API background task is complete.");
        }

        private async Task LoadConfiguration()
        {
            //Note: do not use following code in the production. It's demo only.
            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50532/api/",true);                
                var response = await client.GetAsync("values/settings");

                var stringContent  = await response.Content.ReadAsStringAsync();
               
               var array = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings[]>(stringContent);
                var dictionary =  array.ToDictionary(s => s.Key,s=> s.Value);
               UpdateInMemoryCollection(dictionary);

            }

        }

        private void UpdateInMemoryCollection(Dictionary<string, string> newEntries)
        {
            var configProvider = ((ConfigurationRoot)_configuration).Providers.Where(c => c.GetType() == typeof(HttpCustomConfigProvider)).FirstOrDefault();
            ((HttpCustomConfigProvider)configProvider).HttpKeyValuesCollection = newEntries;
        }

        public class Settings
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }


    }
}

