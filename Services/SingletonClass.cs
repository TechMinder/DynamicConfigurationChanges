﻿using HandlerDynamicConfigurationChanges.ConfigurationChanges;
using Microsoft.Extensions.Options;

namespace HandlerDynamicConfigurationChanges.Services
{
    public class SingletonClass
    {

        private IOptions<ExternalConfig> _options;
        private IOptionsMonitor<ExternalConfig> _optionsMonitor;
        private IOptionsSnapshot<ExternalConfig> _optionsSnapshot;

        public SingletonClass(IOptions<ExternalConfig> options,
                            IOptionsMonitor<ExternalConfig> optionsMonitor
                            )
        {
            _options = options;
            _optionsMonitor = optionsMonitor;
            
        }

        public string GetUrl()
        {
            return _options.Value.Url;
        }

        public string GetUpdatedUrl()
        {
            return _optionsMonitor.CurrentValue.Url;
        }

        public string GetSnapshotUrl()
        {
            return "Snapshot cannot be injected to Singleton";
        }
    }
}