using System;
using System.Collections.Generic;
using System.Linq;
using LOR.Pizzeria.DAL;
using LOR.Pizzeria.Models;

namespace LOR.Pizzeria
{
    class Program
    {
        private static readonly PizzeriaContext context = new PizzeriaContext();

        
        static void Main(string[] args)
        {
            var order = new Order();
            var locations = context.Locations.ToArray();
            
            var locationsString = String.Join(" OR ", locations.Select(location => location.Name));

            Console.WriteLine($"Welcome to LOR Pizzeria! Please select the store location: {locationsString}");

            Location store = null;
            while (store == null)
            {
                var storeStr = Console.ReadLine();
                store = locations.FirstOrDefault(loc => String.Equals(loc.Name, storeStr, StringComparison.CurrentCultureIgnoreCase));
                if (store == null)
                {
                    Console.WriteLine("Error reading store name, please try again");
                }
            }

            var menu = store.PizzaMenuItems.ToList();
            var availableIngredients = store.LocationIngredients.ToList();

            string extraPizzaResponse;
            do
            {
                Console.WriteLine("MENU");
                foreach (var item in menu)
                {
                    Console.WriteLine($"{item.Pizza.Name} - {string.Join(", ", item.Pizza.Ingredients.Select(ingred => ingred.Name))} - {item.BasePrice}AUD");
                }
                Console.WriteLine("What can I get you?");

                PizzaMenuItem pizzaItem = null;
                while (pizzaItem == null)
                {
                    var pizzaStr = Console.ReadLine();
                    pizzaItem = menu.FirstOrDefault(pizza => String.Equals(pizza.Pizza.Name, pizzaStr, StringComparison.CurrentCultureIgnoreCase));
                    if (pizzaItem == null)
                    {
                        Console.WriteLine("Error reading pizza name, please try again");
                    }
                }


                var orderedPizza = new PizzaOrder() { PizzaMenuItem = pizzaItem };



                Console.WriteLine("Any extras? Press Enter to skip");
                foreach (var ingred in availableIngredients)
                {
                    Console.WriteLine($"{ingred.Ingredient.Name} - {ingred.Price}");
                }

                while (true)
                {
                    var extrasStr = Console.ReadLine();
                    if (extrasStr == "") break;

                    var mappedExtras =
                        extrasStr
                        .Split(",")
                        .Select(extra => extra.Trim())
                        .Select(extra => new { str = extra, locationIngred = availableIngredients.FirstOrDefault(ingred => string.Equals(ingred.Ingredient.Name, extra, StringComparison.CurrentCultureIgnoreCase)) })
                        .ToList();

                    var errors = mappedExtras.Where(pair => pair.locationIngred == null).ToList();
                    if (errors.Any())
                    {
                        Console.WriteLine($"Error reading extra name(s): {string.Join(", ", errors.Select(e => e.str))}.  Please try again or hit Enter to skip");
                    }
                    else
                    {
                        foreach (var extra in mappedExtras.Select(pair => pair.locationIngred))
                        {
                            orderedPizza.ExtraToppings.Add(extra);
                        }
                        break;
                    }
                }

                order.PizzaOrders.Add(orderedPizza);

                Console.WriteLine("Would you like to add another pizza to your order? (yes/no)");
                extraPizzaResponse = Console.ReadLine();
                while (extraPizzaResponse != "yes" && extraPizzaResponse != "no")
                {
                    Console.WriteLine("Error reading response: please answer with \"yes\" or \"no\"");
                    extraPizzaResponse = Console.ReadLine();
                }
            } while (extraPizzaResponse == "yes");

            context.SaveChanges();

            order.makeOrder();
            order.PrintReceipt();

            Console.WriteLine("\nYour order is ready!");
        }
    }
}
