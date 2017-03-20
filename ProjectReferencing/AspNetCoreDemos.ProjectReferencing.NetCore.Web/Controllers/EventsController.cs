using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreDemos.ProjectReferencing.NetCore.Data;

namespace AspNetCoreDemos.ProjectReferencing.NetCore.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_events.GetEvents());
        }

        [HttpGet("{id}", Name = "GetEventRoute")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = _events.FindEvent(id);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Event value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var created = _events.AddEvent(value);

            return CreatedAtRoute("GetEventRoute", created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Event value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != value.Id)
                return BadRequest();

            if (_events.UpdateEvent(id, value))
                return Ok();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_events.DeleteEvent(id))
                return Ok();

            return NotFound();
        }

        public EventsController(IEventStore eventStore)
        {
            _events = eventStore;
        }
        private IEventStore _events;
    }
}
