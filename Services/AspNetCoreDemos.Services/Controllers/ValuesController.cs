using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemos.Services.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly AsyncWorkerService _workerService;

        public ValuesController(AsyncWorkerService workerService)
        {
            _workerService = workerService;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            for (int i = 0; i < 5; i++)
            {
                _workerService.TryQueueWork(() => {
                    int counter = i;
                    Task.Delay(TimeSpan.FromSeconds(counter)).Wait();
                });
            }

            return "Queued 5 work items. Stop the web server to see if it blocks the shutdown. (Note: only stopping Kestrel via command prompt will do a graceful shutdown. Stopping VS debugger terminates the process.";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
