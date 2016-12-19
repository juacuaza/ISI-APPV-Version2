using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ISI.Models
{
    public class Estadistica
    {
        [Display(Name = "Codigo estadistica")]
        public int EstadisticaID { get; set; }

        [Required]
        [Display(Name = "Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Total Gastado")]
        public double TotalSpent { get; set; }
    }
}