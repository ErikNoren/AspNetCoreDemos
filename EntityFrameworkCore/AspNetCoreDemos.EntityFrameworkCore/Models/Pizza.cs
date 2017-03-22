using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaAndToppings = new HashSet<PizzaAndToppings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<PizzaAndToppings> PizzaAndToppings { get; set; }

        [NotMapped]
        public IEnumerable<Topping> Toppings { get; set; }
    }
}
