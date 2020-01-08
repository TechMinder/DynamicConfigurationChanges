using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DynamicConfigurationChanges.ConfigProvider
{
    public class HttpCustomConfigProvider : ConfigurationProvider
    {
        private static HttpCustomConfigProvider _provider;
        public HttpCustomConfigProvider()
        {
            HttpKeyValuesCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "external:customprovider", "http" } };
        }

        private static Dictionary<string, string> _httpKeyValuesCollection;
        public static Dictionary<string, string> HttpKeyValuesCollection
        { get { return _httpKeyValuesCollection; } set {

                _httpKeyValuesCollection = value;
                if (_provider != null)
                    _provider.Data = value;

            } }
        public override void Load()
        {
            _provider = this;

            Data = HttpKeyValuesCollection;
        }

        
    }
}
