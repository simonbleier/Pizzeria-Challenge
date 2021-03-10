using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LOR.Pizzeria.Models
{
    public class CutStyle
    {
        [Key]
        public int ID { get; set; }
        public int NumberOfSlices { get; set; }
        public string ExtraInstructions { get; set; }
    }
}
