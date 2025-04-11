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
    public class ReaDroitUtilisateurController : Controller
    {
        private readonly AppDbContext _context;

        public ReaDroitUtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaDroitUtilisateur
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaDroitUtilisateurs.Include(r => r.IdApplicationNavigation).Include(r => r.IdUtilisateurNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaDroitUtilisateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitUtilisateur = await _context.ReaDroitUtilisateurs
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaDroitUtilisateur);
        }

        // GET: ReaDroitUtilisateur/Create
        public IActionResult Create()
        {
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application");
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet");
            return View();
        }

        // POST: ReaDroitUtilisateur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdApplication,IdUtilisateur,Autorise")] ReaDroitUtilisateur reaDroitUtilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaDroitUtilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitUtilisateur.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitUtilisateur.IdUtilisateur);
            return View(reaDroitUtilisateur);
        }

        // GET: ReaDroitUtilisateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitUtilisateur = await _context.ReaDroitUtilisateurs.FindAsync(id);
            if (reaDroitUtilisateur == null)
            {
                return NotFound();
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitUtilisateur.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitUtilisateur.IdUtilisateur);
            return View(reaDroitUtilisateur);
        }

        // POST: ReaDroitUtilisateur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdApplication,IdUtilisateur,Autorise")] ReaDroitUtilisateur reaDroitUtilisateur)
        {
            if (id != reaDroitUtilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaDroitUtilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaDroitUtilisateurExists(reaDroitUtilisateur.Id))
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
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitUtilisateur.IdApplication);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitUtilisateur.IdUtilisateur);
            return View(reaDroitUtilisateur);
        }

        // GET: ReaDroitUtilisateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitUtilisateur = await _context.ReaDroitUtilisateurs
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaDroitUtilisateur);
        }

        // POST: ReaDroitUtilisateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaDroitUtilisateur = await _context.ReaDroitUtilisateurs.FindAsync(id);
            if (reaDroitUtilisateur != null)
            {
                _context.ReaDroitUtilisateurs.Remove(reaDroitUtilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaDroitUtilisateurExists(int id)
        {
            return _context.ReaDroitUtilisateurs.Any(e => e.Id == id);
        }
    }
}
