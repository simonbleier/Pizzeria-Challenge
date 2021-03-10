using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace LOR.Pizzeria.Models
{
    public class LocationIngredient
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int LocationID { get; set; }
        [Required]
        public int IngredientID { get; set; }
        public float Price { get; set; }

        public virtual Location Location { get; set; }
        public virtual Ingredient Ingredient { get; set; } 
    }
}
