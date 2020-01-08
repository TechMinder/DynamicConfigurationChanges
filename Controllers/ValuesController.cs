using DynamicConfigurationChanges.ConfigProvider;
using DynamicConfigurationChanges.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DynamicConfigurationChanges.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ServiceClient _serviceClient;
        private SingletonClass _singletonClass;
        public ValuesController(ServiceClient serviceClient, SingletonClass singletonClass)
        {
            _serviceClient = serviceClient;
            _singletonClass = singletonClass;
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
        [HttpPost]
        [Route("{provider}")]
        public ActionResult<string> Post([FromRoute]string provider)
        {
            HttpCustomConfigProvider.HttpKeyValuesCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "external:customprovider", provider } }; ;
            return Ok();
        }
    }
}
