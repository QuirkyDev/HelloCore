using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloCore.Data;
using HelloCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HelloCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly HelloCoreContext _context;

        public BestellingController(HelloCoreContext context)
        {
            _context = context;
        }

        // GET: api/Bestelling
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestellingen()
        {
            return await _context.Bestellingen.ToListAsync();
        }

        // GET: api/Bestelling/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bestelling>> GetBestelling(int id)
        {
            var bestelling = await _context.Bestellingen.FindAsync(id);

            if (bestelling == null)
            {
                return NotFound();
            }

            return bestelling;
        }

        //GET: api/Bestelling/lijst
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("lijst")]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestelllingenLijst()
        {
            return await _context.Bestellingen.ToListAsync();
        }

        // PUT: api/Bestelling/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestelling(int id, Bestelling bestelling)
        {
            if (id != bestelling.BestellingID)
            {
                return BadRequest();
            }

            _context.Entry(bestelling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BestellingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bestelling
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bestelling>> PostBestelling(Bestelling bestelling)
        {
            _context.Bestellingen.Add(bestelling);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBestelling", new { id = bestelling.BestellingID }, bestelling);
        }

        // DELETE: api/Bestelling/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bestelling>> DeleteBestelling(int id)
        {
            var bestelling = await _context.Bestellingen.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }

            _context.Bestellingen.Remove(bestelling);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestellingen.Any(e => e.BestellingID == id);
        }
    }
}
