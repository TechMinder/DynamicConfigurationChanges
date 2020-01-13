using DynamicConfigurationChanges.ConfigProvider;
using DynamicConfigurationChanges.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicConfigurationChanges.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ServiceClient _serviceClient;
        private SingletonClass _singletonClass;
        private IConfiguration _configuration;
        public ValuesController(ServiceClient serviceClient, SingletonClass singletonClass, IConfiguration configuration)
        {
            _serviceClient = serviceClient;
            _singletonClass = singletonClass;
            _configuration = configuration;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            //run with Chrome or Edge browser to see the result in the browser
            var response = $"DI Scope Transient:\r\n" +
                            $"IOptions:{_serviceClient.GetUrl()}\r\n" +
                            $"IOptionsMonitor: {_serviceClient.GetUpdatedUrl()}\r\n" +
                            $"IOptionsSnapshot:{_serviceClient.GetSnapshotUrl()}\r\n" +
                             $"IOptionsSnapshot.HttpSource:{_serviceClient.GetSnapshotHttpSourceKey()}\r\n" +
                            $"IOptionsMonitor.HttpSource:{_serviceClient.GetMonitorHttpSourceKey()}" +
                           $"\r\n\r\nDI Scope Singleton:\r\n" +
                           $"IOption:{_singletonClass.GetUrl()}\r\n" +
                           $"IOptionsMonitor: {_singletonClass.GetUpdatedUrl()}\r\n" +
                           $"IOptionsSnapshot:{_singletonClass.GetSnapshotUrl()}\r\n" +
                           $"IOptions.HttpSource:{_singletonClass.GetOptionHttpSourceKey()}\r\n" +
                           $"IOptionsMonitor.HttpSource:{_singletonClass.GetMonitorHttpSourceKey()}";


            return response;
        }

        [HttpGet]
        [Route("settings")]
        public ActionResult<string> GetSettings()
        {
            return Ok(new [] { new { Key= "external:customprovider", Value= "azure" }, new { Key = "external:url", Value = "http://azure.com/vault" } });
        }
        [HttpPost]
        [Route("{provider}")]
        public ActionResult<string> Post([FromRoute]string provider)
        {
            //HttpCustomConfigProvider.HttpKeyValuesCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "external:customprovider", provider } }; ;
             var configProvider= ((ConfigurationRoot)_configuration).Providers.Where(c=> c.GetType() == typeof(HttpCustomConfigProvider)).FirstOrDefault();
            ((HttpCustomConfigProvider)configProvider).HttpKeyValuesCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "external:customprovider", provider } }; 
            return Ok();
        }
    }
}
