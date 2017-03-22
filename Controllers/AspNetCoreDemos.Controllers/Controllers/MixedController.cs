using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemos.Controllers.Controllers
{
    public class MixedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/[controller]")]
        public IActionResult IllAdvisedMixingOfApiAndViewMethod()
        {
            return Ok("Just because you can doesn't mean you should. Though this can help with keeping all work related to one concern in one place.");
        }
    }
}