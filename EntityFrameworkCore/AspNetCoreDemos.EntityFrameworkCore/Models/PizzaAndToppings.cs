using System;
using System.Collections.Generic;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public partial class PizzaAndToppings
    {
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
