using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISI.Models
{
    public class VideoClub
    {
        public VideoClub()
        {
            Peliculas = new List<Pelicula>();
        }

        [Display(Name = "Codigo")]
        public int VideoClubID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required]
        [MaxLength(5)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Codigo Postal")]
        public string Zip { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}