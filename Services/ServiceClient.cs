using HandlerDynamicConfigurationChanges.ConfigurationChanges;
using Microsoft.Extensions.Options;

namespace HandlerDynamicConfigurationChanges.Services
{
    public class ServiceClient
    {
        private IOptions<ExternalConfig> _options;
        private IOptionsMonitor<ExternalConfig> _optionsMonitor;
        private IOptionsSnapshot<ExternalConfig> _optionsSnapshot;

        public ServiceClient(IOptions<ExternalConfig> options, 
                            IOptionsMonitor<ExternalConfig> optionsMonitor,
                            IOptionsSnapshot<ExternalConfig> optionsSnapshot)
        {
            _options = options;
            _optionsMonitor = optionsMonitor;
            _optionsSnapshot = optionsSnapshot;
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
            return _optionsSnapshot.Value.Url;
        }
    }
}
