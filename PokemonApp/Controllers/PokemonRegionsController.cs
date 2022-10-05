using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    public class PokemonRegionsController : Controller
    {
        private readonly PokemonDbContext _context;

        public PokemonRegionsController(PokemonDbContext context)
        {
            _context = context;
        }

        // GET: PokemonRegions
        public async Task<IActionResult> Index()
        {
              return View(await _context.PokemonRegions.ToListAsync());
        }

        // GET: PokemonRegions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PokemonRegions == null)
            {
                return NotFound();
            }

            var pokemonRegion = await _context.PokemonRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonRegion == null)
            {
                return NotFound();
            }

            return View(pokemonRegion);
        }

        // GET: PokemonRegions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PokemonRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PokemonRegion pokemonRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemonRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemonRegion);
        }

        // GET: PokemonRegions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PokemonRegions == null)
            {
                return NotFound();
            }

            var pokemonRegion = await _context.PokemonRegions.FindAsync(id);
            if (pokemonRegion == null)
            {
                return NotFound();
            }
            return View(pokemonRegion);
        }

        // POST: PokemonRegions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PokemonRegion pokemonRegion)
        {
            if (id != pokemonRegion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemonRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonRegionExists(pokemonRegion.Id))
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
            return View(pokemonRegion);
        }

        // GET: PokemonRegions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PokemonRegions == null)
            {
                return NotFound();
            }

            var pokemonRegion = await _context.PokemonRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonRegion == null)
            {
                return NotFound();
            }

            return View(pokemonRegion);
        }

        // POST: PokemonRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PokemonRegions == null)
            {
                return Problem("Entity set 'PokemonDbContext.PokemonRegions'  is null.");
            }
            var pokemonRegion = await _context.PokemonRegions.FindAsync(id);
            if (pokemonRegion != null)
            {
                _context.PokemonRegions.Remove(pokemonRegion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonRegionExists(int id)
        {
          return _context.PokemonRegions.Any(e => e.Id == id);
        }
    }
}
