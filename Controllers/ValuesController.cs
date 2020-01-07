using DynamicConfigurationChanges.Services;
using Microsoft.AspNetCore.Mvc;

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
            var response = $"DI Scope Transient:\r\nIOptions:{_serviceClient.GetUrl()}\r\nIOptionsMonitor: {_serviceClient.GetUpdatedUrl()}\r\nIOptionsSnapshot:{_serviceClient.GetSnapshotUrl()}" +
                           $"\r\n\r\nDI Scope Singleton:\r\nIOption:{_singletonClass.GetUrl()}\r\nIOptionsMonitor: {_singletonClass.GetUpdatedUrl()}\r\nIOptionsSnapshot:{_singletonClass.GetSnapshotUrl()}";

            return response;
        }

       
    }
}
