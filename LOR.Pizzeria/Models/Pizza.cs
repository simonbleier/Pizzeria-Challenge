using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOR.Pizzeria.Models
{
    public class Pizza
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int BakingMethodID { get; set; }
        public int CutStyleID { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public Pizza()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public virtual BakingMethod BakingMethod { get; set; }
        public virtual CutStyle CutStyle { get; set; }
    }
}
