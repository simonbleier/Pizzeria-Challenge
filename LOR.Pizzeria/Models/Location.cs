using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PizzaMenuItem> PizzaMenuItems { get; set; }
        public virtual ICollection<LocationIngredient> LocationIngredients { get; set; }
    }
}
