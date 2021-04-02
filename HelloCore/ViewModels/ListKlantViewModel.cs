using HelloCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.ViewModels
{
    public class ListKlantViewModel
    {
        public string NaamSearch { get; set; }
        public string VoornaamSearch { get; set; }
        public List<Klant> Klanten { get; set; }
    }
}
