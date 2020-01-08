using Microsoft.Extensions.Configuration;

namespace DynamicConfigurationChanges.ConfigProvider
{
    public class HttpConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new HttpCustomConfigProvider();
        }
    }
}
