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
    public class ReaUtilisateurController : Controller
    {
        private readonly AppDbContext _context;

        public ReaUtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaUtilisateur
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaUtilisateurs.Include(r => r.IdGroupeNavigation).Include(r => r.IdSiteNavigation).Include(r => r.IdUtilisateurRhNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaUtilisateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _context.ReaUtilisateurs
                .Include(r => r.IdGroupeNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Create
        public IActionResult Create()
        {
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Id");
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "Id");
            ViewData["IdUtilisateurRh"] = new SelectList(_context.ReaUtilisateurRhs, "Id", "Id");
            return View();
        }

        // POST: ReaUtilisateur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomUtilisateur,PrenomUtilisateur,CodeCommercial,Titre,IdSite,IdGroupe,MotPasse,EmailUtilisateur,DateCreation,DateModification,Login,PersonnelExterne,CodeCommercial2,IdPartenaire,Telephone,TestApplication,CodeCegid,UtilisateurTest,IdStatut,IdUtilisateurRh")] ReaUtilisateur reaUtilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaUtilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Id", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "Id", reaUtilisateur.IdSite);
            ViewData["IdUtilisateurRh"] = new SelectList(_context.ReaUtilisateurRhs, "Id", "Id", reaUtilisateur.IdUtilisateurRh);
            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _context.ReaUtilisateurs.FindAsync(id);
            if (reaUtilisateur == null)
            {
                return NotFound();
            }
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Id", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "Id", reaUtilisateur.IdSite);
            ViewData["IdUtilisateurRh"] = new SelectList(_context.ReaUtilisateurRhs, "Id", "Id", reaUtilisateur.IdUtilisateurRh);
            return View(reaUtilisateur);
        }

        // POST: ReaUtilisateur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomUtilisateur,PrenomUtilisateur,CodeCommercial,Titre,IdSite,IdGroupe,MotPasse,EmailUtilisateur,DateCreation,DateModification,Login,PersonnelExterne,CodeCommercial2,IdPartenaire,Telephone,TestApplication,CodeCegid,UtilisateurTest,IdStatut,IdUtilisateurRh")] ReaUtilisateur reaUtilisateur)
        {
            if (id != reaUtilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaUtilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaUtilisateurExists(reaUtilisateur.Id))
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
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Id", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "Id", reaUtilisateur.IdSite);
            ViewData["IdUtilisateurRh"] = new SelectList(_context.ReaUtilisateurRhs, "Id", "Id", reaUtilisateur.IdUtilisateurRh);
            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _context.ReaUtilisateurs
                .Include(r => r.IdGroupeNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaUtilisateur);
        }

        // POST: ReaUtilisateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaUtilisateur = await _context.ReaUtilisateurs.FindAsync(id);
            if (reaUtilisateur != null)
            {
                _context.ReaUtilisateurs.Remove(reaUtilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaUtilisateurExists(int id)
        {
            return _context.ReaUtilisateurs.Any(e => e.Id == id);
        }
    }
}
