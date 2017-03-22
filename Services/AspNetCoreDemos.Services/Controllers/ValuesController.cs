using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDemos.Services.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly AsyncWorkerService _workerService;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(AsyncWorkerService workerService, ILogger<ValuesController> logger)
        {
            _workerService = workerService;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            for (int i = 1; i <= 5; i++)
            {
                _workerService.TryQueueWork(state => {
                    var log = _logger;
                    int delay = (int)state;

                    log.LogInformation($"Work item: Wait for {delay} seconds.");
                    Task.Delay(TimeSpan.FromSeconds(delay)).Wait();
                }, i);
            }

            return "Queued 5 work items. Stop the web server to see if it blocks the shutdown. (Note: only stopping Kestrel via command prompt will do a graceful shutdown. Stopping VS debugger terminates the process.";
        }
    }
}
