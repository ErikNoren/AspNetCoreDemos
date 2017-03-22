using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public partial class Topping
    {
        public Topping()
        {
            PizzaAndToppings = new HashSet<PizzaAndToppings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<PizzaAndToppings> PizzaAndToppings { get; set; }
    }
}
