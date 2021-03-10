using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        /*[Required]
        public int LocationID { get; set; }

        public virtual Location Location { get; set; }*/

        public virtual ICollection<PizzaOrder> PizzaOrders { get; set; }

        public Order()
        {
            PizzaOrders = new HashSet<PizzaOrder>();
        }

        public void makeOrder()
        {
            foreach(var orderedPizza in PizzaOrders)
            {
                orderedPizza.Prepare();
                orderedPizza.Bake();
                orderedPizza.Cut();
                orderedPizza.Box();
            }
        }

        public void PrintReceipt()
        {
            foreach(var pizzaOrder in PizzaOrders)
            {
                Console.WriteLine($"{pizzaOrder.PizzaMenuItem.Pizza.Name.PadRight(21)}{pizzaOrder.PizzaMenuItem.BasePrice}");
                foreach(var extra in pizzaOrder.ExtraToppings)
                {
                    Console.WriteLine($"    - {extra.Ingredient.Name.PadRight(18, ' ')}{extra.Price}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Total price: " + PizzaOrders.Sum(pizzaOrder => pizzaOrder.PizzaMenuItem.BasePrice + pizzaOrder.ExtraToppings.Select(x => x.Price).Sum()));
        }
    }
}
