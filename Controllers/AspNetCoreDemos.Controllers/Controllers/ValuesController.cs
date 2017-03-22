using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Controllers.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController: Controller
    {
        [HttpGet]
        public string GetPlainString()
        {
            return "See how easy it is?";
        }

        [HttpGet("v2/")]
        public IActionResult GetWithActionResult()
        {
            return Ok("Using IActionResult is more flexible though.");
        }
    }
}
