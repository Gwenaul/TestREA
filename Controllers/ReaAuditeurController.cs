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
    public class ReaAuditeurController : Controller
    {
        private readonly AppDbContext _context;

        public ReaAuditeurController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaAuditeur
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaAuditeurs.Include(r => r.IdTypeAuditeurNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaAuditeur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAuditeur = await _context.ReaAuditeurs
                .Include(r => r.IdTypeAuditeurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaAuditeur == null)
            {
                return NotFound();
            }

            return View(reaAuditeur);
        }

        // GET: ReaAuditeur/Create
        public IActionResult Create()
        {
            ViewData["IdTypeAuditeur"] = new SelectList(_context.ReaTypeAuditeurs, "Id", "Libelle");
            return View();
        }

        // POST: ReaAuditeur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,MotPasse,CodeTiers,DateCreation,DerniereConnexion,IdTypeAuditeur,DateNaissance,Nom,Prenom,Email,Telephone,Civilite")] ReaAuditeur reaAuditeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaAuditeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeAuditeur"] = new SelectList(_context.ReaTypeAuditeurs, "Id", "Libelle", reaAuditeur.IdTypeAuditeur);
            return View(reaAuditeur);
        }

        // GET: ReaAuditeur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAuditeur = await _context.ReaAuditeurs.FindAsync(id);
            if (reaAuditeur == null)
            {
                return NotFound();
            }
            ViewData["IdTypeAuditeur"] = new SelectList(_context.ReaTypeAuditeurs, "Id", "Libelle", reaAuditeur.IdTypeAuditeur);
            return View(reaAuditeur);
        }

        // POST: ReaAuditeur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,MotPasse,CodeTiers,DateCreation,DerniereConnexion,IdTypeAuditeur,DateNaissance,Nom,Prenom,Email,Telephone,Civilite")] ReaAuditeur reaAuditeur)
        {
            if (id != reaAuditeur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaAuditeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaAuditeurExists(reaAuditeur.Id))
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
            ViewData["IdTypeAuditeur"] = new SelectList(_context.ReaTypeAuditeurs, "Id", "Id", reaAuditeur.IdTypeAuditeur);
            return View(reaAuditeur);
        }

        // GET: ReaAuditeur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAuditeur = await _context.ReaAuditeurs
                .Include(r => r.IdTypeAuditeurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaAuditeur == null)
            {
                return NotFound();
            }

            return View(reaAuditeur);
        }

        // POST: ReaAuditeur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaAuditeur = await _context.ReaAuditeurs.FindAsync(id);
            if (reaAuditeur != null)
            {
                _context.ReaAuditeurs.Remove(reaAuditeur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaAuditeurExists(int id)
        {
            return _context.ReaAuditeurs.Any(e => e.Id == id);
        }
    }
}
