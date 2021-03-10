using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class PizzaMenuItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int LocationID { get; set; }
        [Required]
        public int PizzaID { get; set; }

        public float BasePrice { get; set; }

        public virtual Pizza Pizza { get; set; }
        //public virtual Location Location { get; set; }
    }
}
