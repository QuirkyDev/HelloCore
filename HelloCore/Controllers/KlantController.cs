using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCore.Controllers
{
    public class KlantController : Controller
    {
        public IActionResult Index()
        {
            List<Klant> klanten = new List<Klant>();

            klanten.Add(new Klant() { KlantID = 1, Naam = "De Neve", Voornaam = "Anneleen", AangemaaktDatum = new System.DateTime(2019, 1, 20) });
            klanten.Add(new Klant() { KlantID = 2, Naam = "Bruyneels", Voornaam = "Nele", AangemaaktDatum = new System.DateTime(2020, 2, 4) });
            klanten.Add(new Klant() { KlantID = 3, Naam = "Naert", Voornaam = "Joris", AangemaaktDatum = new System.DateTime(2020, 1, 5) });

            return View(klanten);
        }
    }
}
