using HelloCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.ViewModels
{
    public class EditBestellingViewModel
    {
        public Bestelling Bestelling { get; set; }
        public SelectList Klanten { get; set; }
    }
}
