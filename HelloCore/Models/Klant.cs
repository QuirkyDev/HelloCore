using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        public DateTime AangemaaktDatum { get; set; }

        public ICollection<Bestelling> Bestellingen { get; set; }
    }
}
