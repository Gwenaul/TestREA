using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestREA.Repositories;
using TestREA.Models;

namespace TestREA.Controllers
{
    public class ReaUtilisateurController : Controller
    {
        private readonly IReaUtilisateurRepository _repository;
        public ReaUtilisateurController(AppDbContext context, IReaUtilisateurRepository repository)
        {
            _repository = repository;
        }

        // GET: ReaUtilisateur
        public async Task<IActionResult> Index(string filtreSaisie, bool UtilisateurTest = false, int page = 1, int nombreLignes = 10)
        {
            var utilisateurs = await _repository.GetUtilisateursAsync(filtreSaisie, UtilisateurTest, page, nombreLignes);
            var total = await _repository.CountUtilisateursAsync(filtreSaisie, UtilisateurTest);

            ViewBag.Page = page;
            ViewBag.NombreLignes = nombreLignes;
            ViewBag.AppDbContextTotal = total;

            return View(utilisateurs);
        }


        // GET: ReaUtilisateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _repository.GetDetailsAsync(id.Value);

            if (reaUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Create
        public async Task<IActionResult> Create()
        {
            var (statuts, groupes, sites, utilisateursRh, droitGroupes) = await _repository.GetSelectListsAsync(new ReaUtilisateur());
            ViewData["IdStatut"] = statuts;
            ViewData["IdGroupe"] = groupes;
            ViewData["IdSite"] = sites;
            ViewData["IdUtilisateurRh"] = utilisateursRh;
            ViewData["DroitsGroupes"] = droitGroupes;

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
                _repository.Add(reaUtilisateur);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var (statuts, groupes, sites, utilisateursRh, droitsGroupes) = await _repository.GetSelectListsAsync(reaUtilisateur);
            ViewData["IdStatut"] = statuts;
            ViewData["IdGroupe"] = groupes;
            ViewData["IdSite"] = sites;
            ViewData["IdUtilisateurRh"] = utilisateursRh;
            ViewData["DroitsGroupes"] = droitsGroupes;

            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _repository.FindAsync(id.Value);

            if (reaUtilisateur == null)
            {
                return NotFound();
            }
            var (statuts, groupes, sites, utilisateursRh, droitsGroupes) = await _repository.GetSelectListsAsync(reaUtilisateur);
            ViewData["IdStatut"] = statuts;
            ViewData["IdGroupe"] = groupes;
            ViewData["IdSite"] = sites;
            ViewData["IdUtilisateurRh"] = utilisateursRh;
            ViewData["DroitsGroupes"] = droitsGroupes;

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
                    _repository.Update(reaUtilisateur);
                    await _repository.SaveChangesAsync();
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
            var (statuts, groupes, sites, utilisateursRh, droitsGroupes) = await _repository.GetSelectListsAsync(reaUtilisateur);
            ViewData["IdStatut"] = statuts;
            ViewData["IdGroupe"] = groupes;
            ViewData["IdSite"] = sites;
            ViewData["IdUtilisateurRh"] = utilisateursRh;
            ViewData["DroitsGroupes"] = droitsGroupes;

            return View(reaUtilisateur);
        }

        // GET: ReaUtilisateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _repository.FindAsync(id.Value);

            if (reaUtilisateur == null)
            {
                return NotFound();
            }

            return View(reaUtilisateur);
        }

        // POST: ReaUtilisateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaUtilisateur = await _repository.FindAsync(id.Value);

            if (reaUtilisateur != null)
            {
                _repository.Remove(reaUtilisateur);
            }

            await _repository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ReaUtilisateurExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
