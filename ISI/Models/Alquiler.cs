using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISI.Models
{
    public class Alquiler
    {
        public Alquiler()
        {
            Peliculas = new List<Pelicula>();
        }
        [Display(Name = "Codigo Alquiler")]
        public int AlquilerID { get; set; }

        [Required]
        [Display(Name = "Fecha de Recogida")]
        [DataType(DataType.Date)]
        public DateTime PickUpate { get; set; }

        [Required]
        [Display(Name = "Fecha de Devolución")]
        [DataType(DataType.Date)]
        public DateTime DateOfReturn { get; set; }


        [Required]
        [Display(Name = "Total a Pagar")]
        public double Cost { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
        public virtual ApplicationUser Socio { get; set; }
    }
}