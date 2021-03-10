using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LOR.Pizzeria.Models;

namespace LOR.Pizzeria.DAL
{
    public class PizzeriaInitializer : DropCreateDatabaseAlways<PizzeriaContext>
    {
        private void AddIngredientsToPizza(Pizza pizza, string[] ingredientNames, Dictionary<string, Ingredient> ingredients)
        {
            foreach (var pizzaIngred in ingredientNames.Select(ingred => ingredients[ingred]))
            {
                pizza.Ingredients.Add(pizzaIngred);
            }
        }

        protected override void Seed(PizzeriaContext context)
        {
            var brisbane = new Location() { Name = "Brisbane" };
            var sydney = new Location() { Name = "Sydney" };

            var locations = new List<Location>() {
                brisbane,
                sydney,
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();

            var ingredients = new string[] { "mushrooms", "cheese", "ham", "mozarella", "olives", "pastrami", "onion", "garlic", "oregano", "chili peppers", "chicken" }
                .ToDictionary(ingred => ingred, ingred => new Ingredient() { Name = ingred });
            context.Ingredients.AddRange(ingredients.Values);
            context.SaveChanges();

            foreach(var loc in locations)
            {
                foreach(var ingred in ingredients.Values)
                {
                    context.LocationIngredients.Add(new LocationIngredient() { Location = loc, Ingredient = ingred, Price = 2 });
                }
            }

            /*var locationIngredients = locations.ToDictionary(loc => loc.Name, loc => ingredients.Values.ToDictionary(ingred => ingred.Name, ingred => new LocationIngredient() { LocationID = loc.ID, IngredientID = ingred.ID, Price = 2 }));
            context.LocationIngredients.AddRange(locationIngredients.Values.SelectMany(x => x.Values));*/

            var standardBake = new BakingMethod() { Duration = 30, Temperature = 200 };
            var margheritaBake = new BakingMethod() { Duration = 15, Temperature = 200 };
            context.BakingMethods.AddRange(new BakingMethod[] { standardBake, margheritaBake });
            context.SaveChanges();

            var standardCutStyle = new CutStyle() { NumberOfSlices = 8 };
            var florenzaCutStyle = new CutStyle() { NumberOfSlices = 6, ExtraInstructions = "with a special knife" };


            var capriciosa = new Pizza() { Name = "Capriciosa", BakingMethod = standardBake, CutStyle = standardCutStyle };
            var florenza = new Pizza() { Name = "Florenza", BakingMethod = standardBake, CutStyle = florenzaCutStyle };
            var margherita = new Pizza() { Name = "Margherita", BakingMethod = margheritaBake, CutStyle = standardCutStyle };
            var inferno = new Pizza() { Name = "Inferno", BakingMethod = standardBake, CutStyle = standardCutStyle };

            AddIngredientsToPizza(capriciosa, new string[] { "mushrooms", "cheese", "ham", "mozarella" }, ingredients);
            AddIngredientsToPizza(florenza, new string[] { "olives", "pastrami", "mozarella", "onion" }, ingredients);
            AddIngredientsToPizza(margherita, new string[] { "mozarella", "onion", "garlic", "oregano" }, ingredients);
            AddIngredientsToPizza(inferno, new string[] { "chili peppers", "mozarella", "chicken", "cheese" }, ingredients);

            context.Pizzas.AddRange(new Pizza[] { capriciosa, florenza, margherita, inferno });
            context.SaveChanges();

            context.PizzaMenuItems.Add(new PizzaMenuItem() { PizzaID = capriciosa.ID, LocationID = brisbane.ID, BasePrice = 20 });
            context.PizzaMenuItems.Add(new PizzaMenuItem() { PizzaID = florenza.ID, LocationID = brisbane.ID, BasePrice = 21 });
            context.PizzaMenuItems.Add(new PizzaMenuItem() { PizzaID = margherita.ID, LocationID = brisbane.ID, BasePrice = 22 });

            context.PizzaMenuItems.Add(new PizzaMenuItem() { PizzaID = capriciosa.ID, LocationID = sydney.ID, BasePrice = 30 });
            context.PizzaMenuItems.Add(new PizzaMenuItem() { PizzaID = inferno.ID, LocationID = sydney.ID, BasePrice = 31 });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
