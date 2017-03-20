using AspNetCoreDemos.SerilogLogging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AspNetCoreDemos.SerilogLogging.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            log.LogInformation("ValuesController.Get() called");

            return Ok(items.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            log.LogInformation("ValuesController.Get({0}) called", id);

            var item = items.Get(id);

            if (item != null)
                return Ok(item);

            log.LogWarning("ItemEntry with Id {0} NOT FOUND", id);
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody]ItemEntry value)
        {
            log.LogInformation("ValuesController.Post called: {0}", JsonConvert.SerializeObject(value));

            return Ok(items.Add(value));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ItemEntry value)
        {
            log.LogInformation("ValuesController.Put({0}, ...) called: {1}", JsonConvert.SerializeObject(value));

            if (id != value.Id)
            {
                log.LogError("Update Id {0} does not match ItemEntry Id {1}", id, value.Id);
                return BadRequest();
            }

            if (items.Update(value))
                return Ok();

            log.LogWarning("ItemEntry with Id {0} NOT FOUND", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            log.LogInformation("ValuesController.Delete({0}) called", id);

            if (items.Delete(id))
                return Ok();

            log.LogWarning("ItemEntry with Id {0} NOT FOUND", id);
            return NotFound();
        }

        public ValuesController(MockDatastore itemStore, ILogger<ValuesController> logger)
        {
            items = itemStore;
            log = logger;
        }
        MockDatastore items;
        ILogger<ValuesController> log;
    }
}
