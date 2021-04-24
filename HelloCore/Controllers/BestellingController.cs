using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCore.Data;
using HelloCore.Models;
using HelloCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloCore.Controllers
{
    [Authorize]
    public class BestellingController : Controller
    {

        private readonly HelloCoreContext _context;

        public BestellingController(HelloCoreContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        //Index (bestelling)
        //Lijst
        public async Task<IActionResult> Index()
        {
            ListBestellingViewModel viewModel = new ListBestellingViewModel();
            viewModel.Bestellingen = await _context.Bestellingen
                .Include(b => b.Klant)
                .ToListAsync();
            return View(viewModel);
        }

        [AllowAnonymous]
        //Search
        public async Task<IActionResult> Search(ListBestellingViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.ArtikelSearch))
            {
                viewModel.Bestellingen = await _context.Bestellingen.Include(b => b.Klant)
                    .Where(b => b.Artikel.Contains(viewModel.ArtikelSearch)).ToListAsync();
            }
            else
            {
                viewModel.Bestellingen = await _context.Bestellingen.Include(b => b.Klant).ToListAsync();
            }

            return View("Index", viewModel);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bestelling = await _context.Bestellingen
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingID == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        //Toevoegen (bestelling)
        //GET
        public IActionResult Create()
        {
            CreateBestellingViewModel viewModel = new CreateBestellingViewModel();
            viewModel.Bestelling = new Bestelling();
            viewModel.Klanten = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam");
            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBestellingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.KlantenLijst = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam", viewModel.Bestelling.KlantID);
            return View(viewModel);
        }

        //Bewerken (bestelling)
        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EditBestellingViewModel viewModel = new EditBestellingViewModel();
            viewModel.Bestelling = await _context.Bestellingen.FindAsync(id);
            if (viewModel.Bestelling == null)
            {
                return NotFound();
            }
            viewModel.Klanten = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam", viewModel.Bestelling.KlantID);
            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBestellingViewModel viewModel)
        {
            if (id != viewModel.Bestelling.BestellingID)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(viewModel.Bestelling.BestellingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.Klanten = new SelectList(_context.Klanten, "KlantID", "VolledigeNaam", viewModel.Bestelling.KlantID);
            return View(viewModel);
        }

        //Verwijderen (bestelling)
        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellingen
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingID == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestellingen.FindAsync(id);
            _context.Bestellingen.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //BestellingExists
        private bool BestellingExists(int id)
        {
            return _context.Bestellingen.Any(e => e.BestellingID == id);
        }

    }
}
