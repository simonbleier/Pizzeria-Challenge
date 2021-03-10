using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class PizzaOrder
    {
        [Key]
        public int ID { get; set; }
        public int PizzaMenuItemID { get; set; }
        public virtual ICollection<LocationIngredient> ExtraToppings { get; set; }
        
        public virtual PizzaMenuItem PizzaMenuItem { get; set; }

        public PizzaOrder()
        {
            ExtraToppings = new HashSet<LocationIngredient>();
        }

        public void Prepare()
        {
            Console.WriteLine("Preparing " + PizzaMenuItem.Pizza.Name + "...");
            Console.Write("Adding ");
            foreach (var ingred in PizzaMenuItem.Pizza.Ingredients)
            {
                Console.Write(ingred.Name + " ");
            }
            foreach (var locationIngred in ExtraToppings)
            {
                Console.Write(locationIngred.Ingredient.Name + " ");
            }
            Console.WriteLine();
        }


        public void Bake()
        {
            Console.WriteLine($"Baking pizza for {PizzaMenuItem.Pizza.BakingMethod.Duration} minutes at {PizzaMenuItem.Pizza.BakingMethod.Temperature} degrees...");
        }

        public void Cut()
        {
            Console.WriteLine($"Cutting pizza into {PizzaMenuItem.Pizza.CutStyle.NumberOfSlices} slices{(PizzaMenuItem.Pizza.CutStyle.ExtraInstructions != null ? " " + PizzaMenuItem.Pizza.CutStyle.ExtraInstructions : "")}...");
        }

        public void Box()
        {
            Console.WriteLine("Putting pizza into a nice box...");
        }
    }
}
