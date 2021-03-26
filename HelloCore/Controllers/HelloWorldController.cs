using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloCore.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "Dit is de 'Index' Action Method";
        }

        public string Welkom()
        {
            return "Dit is de 'Welkom' Action Method";
        }

        public string Bestelling(int id)
        {
            return "Dit zijn de details van bestelling met id " + id;
        }

        public string Boodschap(string voornaam, string boodschap)
        {
            return "Boodschap van " + voornaam + ": " + boodschap;
        }

    }
}
