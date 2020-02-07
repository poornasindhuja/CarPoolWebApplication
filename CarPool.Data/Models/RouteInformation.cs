using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data.Models
{
    public class RouteInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Source { get; set; }
        [Required]
        public int Destination { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
