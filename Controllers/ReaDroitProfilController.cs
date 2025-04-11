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
    public class ReaDroitProfilController : Controller
    {
        private readonly AppDbContext _context;

        public ReaDroitProfilController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaDroitProfil
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaDroitProfils.Include(r => r.IdProfilNavigation).Include(r => r.IdUtilisateurNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaDroitProfil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitProfil = await _context.ReaDroitProfils
                .Include(r => r.IdProfilNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitProfil == null)
            {
                return NotFound();
            }

            return View(reaDroitProfil);
        }

        // GET: ReaDroitProfil/Create
        public IActionResult Create()
        {
            ViewData["IdProfil"] = new SelectList(_context.ReaProfils, "Id", "Libelle");
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet");
            return View();
        }

        // POST: ReaDroitProfil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUtilisateur,IdProfil,Autorise")] ReaDroitProfil reaDroitProfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaDroitProfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProfil"] = new SelectList(_context.ReaProfils, "Id", "Libelle", reaDroitProfil.IdProfil);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitProfil.IdUtilisateur);
            return View(reaDroitProfil);
        }

        // GET: ReaDroitProfil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitProfil = await _context.ReaDroitProfils.FindAsync(id);
            if (reaDroitProfil == null)
            {
                return NotFound();
            }
            ViewData["IdProfil"] = new SelectList(_context.ReaProfils, "Id", "Libelle", reaDroitProfil.IdProfil);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new {u.Id,NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur}),"Id","NomComplet",reaDroitProfil.IdUtilisateur);
            return View(reaDroitProfil);
        }

        // POST: ReaDroitProfil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUtilisateur,IdProfil,Autorise")] ReaDroitProfil reaDroitProfil)
        {
            if (id != reaDroitProfil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaDroitProfil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaDroitProfilExists(reaDroitProfil.Id))
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
            ViewData["IdProfil"] = new SelectList(_context.ReaProfils, "Id", "Libelle", reaDroitProfil.IdProfil);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitProfil.IdUtilisateur);
            return View(reaDroitProfil);
        }

        // GET: ReaDroitProfil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitProfil = await _context.ReaDroitProfils
                .Include(r => r.IdProfilNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitProfil == null)
            {
                return NotFound();
            }

            return View(reaDroitProfil);
        }

        // POST: ReaDroitProfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaDroitProfil = await _context.ReaDroitProfils.FindAsync(id);
            if (reaDroitProfil != null)
            {
                _context.ReaDroitProfils.Remove(reaDroitProfil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaDroitProfilExists(int id)
        {
            return _context.ReaDroitProfils.Any(e => e.Id == id);
        }
    }
}
