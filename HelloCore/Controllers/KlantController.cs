using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloCore.Data;
using HelloCore.Models;
using HelloCore.ViewModels;

namespace HelloCore.Controllers
{
    public class KlantController : Controller
    {
        private readonly HelloCoreContext _context;

        public KlantController(HelloCoreContext context)
        {
            _context = context;
        }

        // GET: Klant
        public async Task<IActionResult> Index()
        {
            ListKlantViewModel viewModel = new ListKlantViewModel();
            viewModel.Klanten = await _context.Klanten.ToListAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> Search(ListKlantViewModel viewModel)
        {
            IQueryable<Klant> queryableKlanten = _context.Klanten.AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.NaamSearch))
            {
                queryableKlanten = queryableKlanten.Where(k => k.Naam.StartsWith(viewModel.NaamSearch));
            }
            if (!string.IsNullOrEmpty(viewModel.VoornaamSearch))
            {
                queryableKlanten = queryableKlanten.Where(k => k.Voornaam.StartsWith(viewModel.VoornaamSearch));
            }

            viewModel.Klanten = await queryableKlanten.ToListAsync();

            return View("Index", viewModel);
        }

        // GET: Klant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .FirstOrDefaultAsync(m => m.KlantID == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlantID,Naam,Voornaam,AangemaaktDatum")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten.FindAsync(id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlantID,Naam,Voornaam,AangemaaktDatum")] Klant klant)
        {
            if (id != klant.KlantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantExists(klant.KlantID))
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
            return View(klant);
        }

        // GET: Klant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .FirstOrDefaultAsync(m => m.KlantID == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klant = await _context.Klanten.FindAsync(id);
            _context.Klanten.Remove(klant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
            return _context.Klanten.Any(e => e.KlantID == id);
        }
    }
}
