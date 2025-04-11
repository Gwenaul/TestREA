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
    public class ReaDroitGroupeController : Controller
    {
        private readonly AppDbContext _context;

        public ReaDroitGroupeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaDroitGroupe
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaDroitGroupes.Include(r => r.IdApplicationNavigation).Include(r => r.IdGroupeNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaDroitGroupe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitGroupe = await _context.ReaDroitGroupes
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdGroupeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitGroupe == null)
            {
                return NotFound();
            }

            return View(reaDroitGroupe);
        }

        // GET: ReaDroitGroupe/Create
        public IActionResult Create()
        {
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application");
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle");
            return View();
        }

        // POST: ReaDroitGroupe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdApplication,IdGroupe,Autorise")] ReaDroitGroupe reaDroitGroupe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaDroitGroupe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitGroupe.IdApplication);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaDroitGroupe.IdGroupe);
            return View(reaDroitGroupe);
        }

        // GET: ReaDroitGroupe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitGroupe = await _context.ReaDroitGroupes.FindAsync(id);
            if (reaDroitGroupe == null)
            {
                return NotFound();
            }
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitGroupe.IdApplication);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaDroitGroupe.IdGroupe);
            return View(reaDroitGroupe);
        }

        // POST: ReaDroitGroupe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdApplication,IdGroupe,Autorise")] ReaDroitGroupe reaDroitGroupe)
        {
            if (id != reaDroitGroupe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaDroitGroupe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaDroitGroupeExists(reaDroitGroupe.Id))
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
            ViewData["IdApplication"] = new SelectList(_context.ReaApplications, "Id", "Application", reaDroitGroupe.IdApplication);
            ViewData["IdGroupe"] = new SelectList(_context.ReaGroupes, "Id", "Libelle", reaDroitGroupe.IdGroupe);
            return View(reaDroitGroupe);
        }

        // GET: ReaDroitGroupe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitGroupe = await _context.ReaDroitGroupes
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdGroupeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitGroupe == null)
            {
                return NotFound();
            }

            return View(reaDroitGroupe);
        }

        // POST: ReaDroitGroupe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaDroitGroupe = await _context.ReaDroitGroupes.FindAsync(id);
            if (reaDroitGroupe != null)
            {
                _context.ReaDroitGroupes.Remove(reaDroitGroupe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaDroitGroupeExists(int id)
        {
            return _context.ReaDroitGroupes.Any(e => e.Id == id);
        }
    }
}
