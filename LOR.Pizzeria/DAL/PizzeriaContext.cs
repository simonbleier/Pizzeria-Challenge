using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LOR.Pizzeria.Models;

namespace LOR.Pizzeria.DAL
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext() : base("PizzeriaContext")
        {
            Database.SetInitializer(new PizzeriaInitializer());
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<LocationIngredient> LocationIngredients { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaMenuItem> PizzaMenuItems { get; set; }
        public DbSet<PizzaOrder> PizzaOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BakingMethod> BakingMethods { get; set; }
        public DbSet<CutStyle> CutStyles { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaMenuItem>()
                .HasRequired(p => p.Location)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PizzaMenuItem>()
                .HasRequired(p => p.Pizza)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
