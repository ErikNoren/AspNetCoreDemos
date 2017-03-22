using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreDemos.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemos.EntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    public class PizzaController: Controller
    {
        private readonly PizzaTimeRepository _pizzaProvider;

        public PizzaController(PizzaTimeRepository pizzaProvider)
        {
            _pizzaProvider = pizzaProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pizzaProvider.GetAllPizza());
        }

        [HttpGet("{id}", Name="GetPizzaById")]
        public IActionResult Get(int id)
        {
            return Ok(_pizzaProvider.GetPizzaById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pizza pizza)
        {
            try
            {
                _pizzaProvider.AddPizza(pizza);

                return CreatedAtRoute("GetPizzaById", new { id = pizza.Id }, pizza);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
