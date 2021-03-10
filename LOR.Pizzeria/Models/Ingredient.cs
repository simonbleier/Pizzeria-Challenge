using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }

        public Ingredient()
        {
            Pizzas = new HashSet<Pizza>();
        }
    }
}
