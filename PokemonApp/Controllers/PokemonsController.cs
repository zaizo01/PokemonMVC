using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;


namespace PokemonApp.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly PokemonDbContext _context;

        public IWebHostEnvironment WebHostEnvironment { get; }

        public PokemonsController(PokemonDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            WebHostEnvironment = webHostEnvironment;
        }

      
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Pokemon> pokemonDbContext = _context.Pokemon
                .Include(p => p.PokemonRegion)
                .Include(p => p.PokemonType);


            if (!String.IsNullOrEmpty(searchString))
            {
                pokemonDbContext = pokemonDbContext.Where(p => p.Name.Contains(searchString));
            }
            return View(await pokemonDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.PokemonRegion)
                .Include(p => p.PokemonType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

     
        public IActionResult Create()
        {
            ViewData["PokemonRegionId"] = new SelectList(_context.PokemonRegions, "Id", "Name");
            ViewData["PokemonTypeId"] = new SelectList(_context.PokemonTypes, "Id", "Name");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PokemonRegionId,PokemonTypeId")] Pokemon pokemon)
        { 
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PokemonRegionId"] = new SelectList(_context.PokemonRegions, "Id", "Name", pokemon.PokemonRegionId);
            ViewData["PokemonTypeId"] = new SelectList(_context.PokemonTypes, "Id", "Name", pokemon.PokemonTypeId);
            return View(pokemon);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["PokemonRegionId"] = new SelectList(_context.PokemonRegions, "Id", "Name", pokemon.PokemonRegionId);
            ViewData["PokemonTypeId"] = new SelectList(_context.PokemonTypes, "Id", "Name", pokemon.PokemonTypeId);
            return View(pokemon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PokemonRegionId,PokemonTypeId")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
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
            ViewData["PokemonRegionId"] = new SelectList(_context.PokemonRegions, "Id", "Name", pokemon.PokemonRegionId);
            ViewData["PokemonTypeId"] = new SelectList(_context.PokemonTypes, "Id", "Name", pokemon.PokemonTypeId);
            return View(pokemon);
        }

    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.PokemonRegion)
                .Include(p => p.PokemonType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pokemon == null)
            {
                return Problem("Entity set 'PokemonDbContext.Pokemon'  is null.");
            }
            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemon.Remove(pokemon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
          return _context.Pokemon.Any(e => e.Id == id);
        }
    }
}
