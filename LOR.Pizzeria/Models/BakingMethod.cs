using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class BakingMethod
    {
        [Key]
        public int ID { get; set; }
        public int Duration { get; set; }
        public int Temperature { get; set; }
    }
}
