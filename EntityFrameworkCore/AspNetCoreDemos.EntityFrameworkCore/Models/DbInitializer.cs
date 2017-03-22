using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public static class DbInitializer
    {
        public static void Initialize(PizzaTimeContext context)
        {
            context.Database.EnsureCreated();

            var plain = new Pizza() { Name = "Plain" };
            var hawaiian = new Pizza() { Name = "Hawaiian" };
            var hamburger = new Pizza() { Name = "Hamburger" };
            var meats = new Pizza() { Name = "Meats" };

            context.Pizzas.AddRange(plain, hawaiian, hamburger, meats);
            context.SaveChanges();
            
            var ham = new Topping() { Name = "Ham" };
            var pepperoni = new Topping() { Name = "Pepperoni" };
            var cheese = new Topping() { Name = "Cheese" };
            var beef = new Topping() { Name = "Ground Beef" };
            var sausage = new Topping() { Name = "Sausage" };
            var bacon = new Topping() { Name = "Bacon" };
            var pineapple = new Topping() { Name = "Pineapple" };

            context.Toppings.AddRange(ham, pepperoni, cheese, beef, sausage, bacon, pineapple);
            context.SaveChanges();

            context.PizzaAndToppings.AddRange(new PizzaAndToppings[] {
                new PizzaAndToppings() { PizzaId = plain.Id, ToppingId = cheese.Id },
                new PizzaAndToppings() { PizzaId = hawaiian.Id, ToppingId = cheese.Id },
                new PizzaAndToppings() { PizzaId = hawaiian.Id, ToppingId = ham.Id },
                new PizzaAndToppings() { PizzaId = hawaiian.Id, ToppingId = pineapple.Id },
                new PizzaAndToppings() { PizzaId = hamburger.Id, ToppingId = cheese.Id },
                new PizzaAndToppings() { PizzaId = hamburger.Id, ToppingId = beef.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = cheese.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = ham.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = pepperoni.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = beef.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = sausage.Id },
                new PizzaAndToppings() { PizzaId = meats.Id, ToppingId = bacon.Id }
            });

            context.SaveChanges();
        }
    }
}
