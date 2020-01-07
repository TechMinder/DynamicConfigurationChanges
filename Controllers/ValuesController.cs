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

            var response = $"TransientScope:\r\nOption Value:{_serviceClient.GetUrl()}\r\nMonitor Value: {_serviceClient.GetUpdatedUrl()}\r\nSnapshot Value:{_serviceClient.GetSnapshotUrl()}" +
                           $"\r\nSingleton:\r\nOption Value:{_singletonClass.GetUrl()}\r\nnMonitor Value: {_singletonClass.GetUpdatedUrl()}\r\nSnapshot Value:{_singletonClass.GetSnapshotUrl()}";

            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
