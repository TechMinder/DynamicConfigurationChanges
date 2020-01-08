using Microsoft.Extensions.Configuration;

namespace DynamicConfigurationChanges.ConfigProvider
{
    public static class HttpBuilderExtension
    {
        public static IConfigurationBuilder AddHttpConfiguration(
       this IConfigurationBuilder builder)
        {
            return builder.Add(new HttpConfigSource());
        }
    }
}
