using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestREA.Models;

namespace TestREA.Controllers
{
    public class ReaSiteController : Controller
    {
        private readonly AppDbContext _context;

        public ReaSiteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaSite
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaSites.Include(r => r.IdDirectionNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaSite/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaSite = await _context.ReaSites
                .Include(r => r.IdDirectionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaSite == null)
            {
                return NotFound();
            }

            return View(reaSite);
        }

        // GET: ReaSite/Create
        public IActionResult Create()
        {
            ViewData["IdDirection"] = new SelectList(_context.ReaDirections, "Id", "Libelle");
            return View();
        }

        // POST: ReaSite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomSite,IdDirection,CodeSite,CodeDepartement,Adresse,Archive")] ReaSite reaSite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaSite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDirection"] = new SelectList(_context.ReaDirections, "Id", "Libelle", reaSite.IdDirection);
            return View(reaSite);
        }

        // GET: ReaSite/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaSite = await _context.ReaSites.FindAsync(id);
            if (reaSite == null)
            {
                return NotFound();
            }
            ViewData["IdDirection"] = new SelectList(_context.ReaDirections, "Id", "Libelle", reaSite.IdDirection);
            return View(reaSite);
        }

        // POST: ReaSite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomSite,IdDirection,CodeSite,CodeDepartement,Adresse,Archive")] ReaSite reaSite)
        {
            if (id != reaSite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaSite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaSiteExists(reaSite.Id))
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
            ViewData["IdDirection"] = new SelectList(_context.ReaDirections, "Id", "Libelle", reaSite.IdDirection);
            return View(reaSite);
        }

        // GET: ReaSite/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaSite = await _context.ReaSites
                .Include(r => r.IdDirectionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaSite == null)
            {
                return NotFound();
            }

            return View(reaSite);
        }

        // POST: ReaSite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaSite = await _context.ReaSites.FindAsync(id);
            if (reaSite != null)
            {
                _context.ReaSites.Remove(reaSite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaSiteExists(int id)
        {
            return _context.ReaSites.Any(e => e.Id == id);
        }
    }
}
