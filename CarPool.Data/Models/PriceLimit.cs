using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data.Models
{
    public class PriceLimit
    {
        [Key]
        public int CarType { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
