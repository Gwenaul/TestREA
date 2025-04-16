using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestREA.Models;
using TestREA.Repositories;

namespace TestREA.Controllers
{
    public class ReaAcceController : Controller
    {
        //private readonly AppDbContext _context;
        private readonly IReaAcceRepository _repository;

        public ReaAcceController(AppDbContext context, IReaAcceRepository repository)
        {
            _repository = repository;
            //_context = context;
        }

        // GET: ReaAcce
        public async Task<IActionResult> Index(int page = 1, int nombreLignes = 10)
        {

            var appDbContextTotal = await _repository.CountAsync();
            var appDbContextRestreint = await _repository.GetAllAsync(page, nombreLignes);

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

            var reaAcce = await _repository.FindAsync(id.Value);
            if (reaAcce == null)
            {
                return NotFound();
            }

            return View(reaAcce);
        }

        // GET: ReaAcce/Create
        public IActionResult Create()
        {
            ViewData["IdApplication"] =_repository.GetApplicationsSelectListAsync();
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
                _repository.AddAsync(reaAcce);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApplication"] = _repository.GetApplicationsSelectListAsync();
            return View(reaAcce);
        }

        // GET: ReaAcce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAcce = await _repository.FindAsync(id.Value);
            if (reaAcce == null)
            {
                return NotFound();
            }
            ViewData["IdApplication"] = _repository.GetApplicationsSelectListAsync();
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
                    await _repository.UpdateAsync(reaAcce);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReaAcceExistsAsync(reaAcce.Id))
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
            ViewData["IdApplication"] = _repository.GetApplicationsSelectListAsync();
            return View(reaAcce);
        }

        // GET: ReaAcce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaAcce = await _repository.FindAsync(id.Value);
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
            var reaAcce = await _repository.FindAsync(id);
            if (reaAcce != null)
            {
                _repository.DeleteAsync(reaAcce);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReaAcceExistsAsync(int id)
        {
            return await _repository.ReaAcceExistsAsync(id);
        }

    }
}
