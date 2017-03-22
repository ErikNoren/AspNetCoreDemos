using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public class PizzaTimeRepository
    {
        private readonly PizzaTimeContext _context;

        public PizzaTimeRepository(PizzaTimeContext context)
        {
            _context = context;
        }

        public IQueryable<Pizza> GetAllPizza()
        {
            return _context
                .PizzaAndToppings
                .GroupBy(t => t.Pizza)
                .Select(g => new Pizza() { Id = g.Key.Id, Name = g.Key.Name, Toppings = g.Select(t => t.Topping) });
        }

        public Pizza GetPizzaById(int pizzaId)
        {
            return GetAllPizza()
                .FirstOrDefault(p => p.Id == pizzaId);
        }

        public void AddPizza(Pizza pizza)
        {
            foreach (var topping in pizza.Toppings)
            {
                if (topping.Id == 0)
                    _context.Toppings.Add(topping);
                else
                    _context.Entry<Topping>(topping).State = EntityState.Unchanged;
            }

            _context.SaveChanges();

            pizza.PizzaAndToppings = pizza.Toppings.Select(t => new PizzaAndToppings() { Pizza = pizza, Topping = t }).ToArray();
            
            _context.Pizzas.Add(pizza);

            _context.SaveChanges();
        }
    }
}
