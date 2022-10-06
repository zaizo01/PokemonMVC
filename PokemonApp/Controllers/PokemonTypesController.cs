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
    public class PokemonTypesController : Controller
    {
        private readonly PokemonDbContext _context;

        public PokemonTypesController(PokemonDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
              return View(await _context.PokemonTypes.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

       
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PokemonType pokemonType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemonType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemonType);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes.FindAsync(id);
            if (pokemonType == null)
            {
                return NotFound();
            }
            return View(pokemonType);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PokemonType pokemonType)
        {
            if (id != pokemonType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemonType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonTypeExists(pokemonType.Id))
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
            return View(pokemonType);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PokemonTypes == null)
            {
                return Problem("Entity set 'PokemonDbContext.PokemonTypes'  is null.");
            }
            var pokemonType = await _context.PokemonTypes.FindAsync(id);
            if (pokemonType != null)
            {
                _context.PokemonTypes.Remove(pokemonType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonTypeExists(int id)
        {
          return _context.PokemonTypes.Any(e => e.Id == id);
        }
    }
}
