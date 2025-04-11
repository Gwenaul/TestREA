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
    public class ReaAcceController : Controller
    {
        private readonly AppDbContext _context;

        public ReaAcceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaAcce
        public async Task<IActionResult> Index(int page = 1, int nombreLignes = 10)
        {

            var appDbContextTotal = await _context.ReaAcces.CountAsync();
            var appDbContextRestreint = await _context.ReaAcces
                .Skip((page - 1) * nombreLignes)
                .Take(nombreLignes)
                .ToListAsync();

            ViewBag.Page = page;
            ViewBag.NombreLignes = nombreLignes;
            ViewBag.AppDbContextTotal = appDbContextTotal;

            return View(appDbContextRestreint);
        }

        // GET: ReaAcce/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAcce = await _context.ReaAcces
                .Include(r => r.IdApplicationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaAcce == null)
            {
                return NotFound();
            }

            return View(reaAcce);
        }

        // GET: ReaAcce/Create
        public IActionResult Create()
        {
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Id");
            return View();
        }

        // POST: ReaAcce/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdApplication,DateConnexion,IdUtilisateur")] ReaAcce reaAcce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaAcce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Id", reaAcce.IdApplication);
            return View(reaAcce);
        }

        // GET: ReaAcce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAcce = await _context.ReaAcces.FindAsync(id);
            if (reaAcce == null)
            {
                return NotFound();
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Id", reaAcce.IdApplication);
            return View(reaAcce);
        }

        // POST: ReaAcce/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdApplication,DateConnexion,IdUtilisateur")] ReaAcce reaAcce)
        {
            if (id != reaAcce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaAcce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaAcceExists(reaAcce.Id))
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
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Id", reaAcce.IdApplication);
            return View(reaAcce);
        }

        // GET: ReaAcce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAcce = await _context.ReaAcces
                .Include(r => r.IdApplicationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaAcce == null)
            {
                return NotFound();
            }

            return View(reaAcce);
        }

        // POST: ReaAcce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaAcce = await _context.ReaAcces.FindAsync(id);
            if (reaAcce != null)
            {
                _context.ReaAcces.Remove(reaAcce);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaAcceExists(int id)
        {
            return _context.ReaAcces.Any(e => e.Id == id);
        }
    }
}
