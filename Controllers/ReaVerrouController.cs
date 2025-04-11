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
    public class ReaVerrouController : Controller
    {
        private readonly AppDbContext _context;

        public ReaVerrouController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaVerrou
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaVerrous.Include(r => r.IdApplicationNavigation).Include(r => r.IdUtilisateurNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaVerrou/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaVerrou = await _context.ReaVerrous
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaVerrou == null)
            {
                return NotFound();
            }

            return View(reaVerrou);
        }

        // GET: ReaVerrou/Create
        public IActionResult Create()
        {
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application");
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet");
            return View();
        }

        // POST: ReaVerrou/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUtilisateur,NbTentative,DateVerrou,IdApplication")] ReaVerrou reaVerrou)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaVerrou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaVerrou.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaVerrou.IdUtilisateur);
            return View(reaVerrou);
        }

        // GET: ReaVerrou/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaVerrou = await _context.ReaVerrous.FindAsync(id);
            if (reaVerrou == null)
            {
                return NotFound();
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaVerrou.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaVerrou.IdUtilisateur);
            return View(reaVerrou);
        }

        // POST: ReaVerrou/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUtilisateur,NbTentative,DateVerrou,IdApplication")] ReaVerrou reaVerrou)
        {
            if (id != reaVerrou.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaVerrou);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaVerrouExists(reaVerrou.Id))
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
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaVerrou.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaVerrou.IdUtilisateur);
            return View(reaVerrou);
        }

        // GET: ReaVerrou/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaVerrou = await _context.ReaVerrous
                .Include(r => r.IdApplicationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaVerrou == null)
            {
                return NotFound();
            }

            return View(reaVerrou);
        }

        // POST: ReaVerrou/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaVerrou = await _context.ReaVerrous.FindAsync(id);
            if (reaVerrou != null)
            {
                _context.ReaVerrous.Remove(reaVerrou);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaVerrouExists(int id)
        {
            return _context.ReaVerrous.Any(e => e.Id == id);
        }
    }
}
