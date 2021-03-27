using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Models
{
    public class Klant
    {
        public int KlantID { get; set; }

        [Required]
        public string Naam { get; set; }

        public string Voornaam { get; set; }

        [NotMapped]
        public string VolledigeNaam => $"{Voornaam} {Naam}";

        [DataType(DataType.Date)]
        public DateTime AangemaaktDatum { get; set; }

        public ICollection<Bestelling> Bestellingen { get; set; }
    }
}
