using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DynamicConfigurationChanges.ConfigProvider
{
    public class HttpCustomConfigProvider : ConfigurationProvider
    {
       
        public HttpCustomConfigProvider()
        {
            HttpKeyValuesCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "external:customprovider", "http" } };
        }

        private Dictionary<string, string> _httpKeyValuesCollection;
        public Dictionary<string, string> HttpKeyValuesCollection
        { get { return _httpKeyValuesCollection; } set {

                _httpKeyValuesCollection = value;
                Reload();
                OnReload();
        
            } }
        public override void Load()
        {       
            Data = HttpKeyValuesCollection;

        }

        private void Reload()
        {
            if (Data != null && Data.Count > 0) //merge
            {
                foreach (var item in HttpKeyValuesCollection)
                {
                    if (this.Data.ContainsKey(item.Key))
                        this.Data[item.Key] = item.Value;
                    else
                        this.Set(item.Key, item.Value);
                }

            }
        }

        
    }
}
