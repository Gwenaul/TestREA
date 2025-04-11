using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        // GET: ReaUtilisateur
        public async Task<IActionResult> Index(string filtreSaisie, bool UtilisateurTest = false, int page = 1, int nombreLignes = 10)
        {
            var query = _context.ReaUtilisateurs
                .Include(r => r.IdStatutNavigation)
                .Include(r => r.IdGroupeNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .AsQueryable();

            // Appliquer le filtrage
            if (!string.IsNullOrEmpty(filtreSaisie))
            {
                query = query.Where(u => u.NomUtilisateur.Contains(filtreSaisie)
                    || u.EmailUtilisateur.Contains(filtreSaisie)
                    || u.PrenomUtilisateur.Contains(filtreSaisie));
            }

            if (UtilisateurTest)
            {
                query = query.Where(u => u.UtilisateurTest == true);
            }

            // Calculer le nombre total d'éléments après filtrage
            var appDbContextTotal = await query.CountAsync();

            // Appliquer la pagination
            var appDbContextRestreint = await query
                .Skip((page - 1) * nombreLignes)
                .Take(nombreLignes)
                .ToListAsync();

            // Passer les données nécessaires à la vue
            ViewBag.Page = page;
            ViewBag.NombreLignes = nombreLignes;
            ViewBag.AppDbContextTotal = appDbContextTotal;

            return View(appDbContextRestreint);
        }

        // GET: ReaUtilisateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _context.ReaUtilisateurs
                .Include(r => r.IdStatutNavigation)
                .Include(r => r.IdGroupeNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .Include(r => r.ReaDroitProfils)
                .ThenInclude(rr => rr.IdProfilNavigation)
                .Include(r => r.ReaDroitRoles)
                .ThenInclude(rr => rr.IdRoleNavigation)
                .Include(r => r.ReaDroitUtilisateurs)
                .ThenInclude(rr => rr.IdApplicationNavigation)
                .Include(r => r.ReaVerrous)
                .ThenInclude(rr => rr.IdApplicationNavigation)
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
            ViewData["IdStatut"] = new SelectList(_context.ReaStatuts, "Id", "Libelle");
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle");
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "NomSite");
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
            ViewData["IdStatut"] = new SelectList(_context.ReaStatuts, "Id", "Libelle", reaUtilisateur.IdStatut);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "NomSite", reaUtilisateur.IdSite);
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
            ViewData["IdStatut"] = new SelectList(_context.ReaStatuts, "Id", "Libelle", reaUtilisateur.IdStatut);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "NomSite", reaUtilisateur.IdSite);
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
            ViewData["IdStatut"] = new SelectList(_context.ReaStatuts, "Id", "Libelle", reaUtilisateur.IdStatut);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaUtilisateur.IdGroupe);
            ViewData["IdSite"] = new SelectList(_context.ReaSites, "Id", "NomSite", reaUtilisateur.IdSite);
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
                .Include(r => r.IdStatutNavigation)
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
