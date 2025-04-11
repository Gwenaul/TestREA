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
    public class ReaDroitRoleController : Controller
    {
        private readonly AppDbContext _context;

        public ReaDroitRoleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaDroitRole
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaDroitRoles.Include(r => r.IdRoleNavigation).Include(r => r.IdUtilisateurNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaDroitRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitRole = await _context.ReaDroitRoles
                .Include(r => r.IdRoleNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitRole == null)
            {
                return NotFound();
            }

            return View(reaDroitRole);
        }

        // GET: ReaDroitRole/Create
        public IActionResult Create()
        {
            ViewData["IdRole"] = new SelectList(_context.ReaRoles, "Id", "Libelle");
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet");
            return View();
        }

        // POST: ReaDroitRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdRole,IdUtilisateur,Autorise")] ReaDroitRole reaDroitRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaDroitRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.ReaRoles, "Id", "Libelle", reaDroitRole.IdRole);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitRole.IdUtilisateur);
            return View(reaDroitRole);
        }

        // GET: ReaDroitRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitRole = await _context.ReaDroitRoles.FindAsync(id);
            if (reaDroitRole == null)
            {
                return NotFound();
            }
            ViewData["IdRole"] = new SelectList(_context.ReaRoles, "Id", "Libelle", reaDroitRole.IdRole);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitRole.IdUtilisateur);
            return View(reaDroitRole);
        }

        // POST: ReaDroitRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRole,IdUtilisateur,Autorise")] ReaDroitRole reaDroitRole)
        {
            if (id != reaDroitRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaDroitRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaDroitRoleExists(reaDroitRole.Id))
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
            ViewData["IdRole"] = new SelectList(_context.ReaRoles, "Id", "Libelle", reaDroitRole.IdRole);
            ViewData["IdUtilisateur"] = new SelectList(_context.ReaUtilisateurs.Select(u => new { u.Id, NomComplet = u.PrenomUtilisateur + " " + u.NomUtilisateur }), "Id", "NomComplet", reaDroitRole.IdUtilisateur);
            return View(reaDroitRole);
        }

        // GET: ReaDroitRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaDroitRole = await _context.ReaDroitRoles
                .Include(r => r.IdRoleNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaDroitRole == null)
            {
                return NotFound();
            }

            return View(reaDroitRole);
        }

        // POST: ReaDroitRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaDroitRole = await _context.ReaDroitRoles.FindAsync(id);
            if (reaDroitRole != null)
            {
                _context.ReaDroitRoles.Remove(reaDroitRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaDroitRoleExists(int id)
        {
            return _context.ReaDroitRoles.Any(e => e.Id == id);
        }
    }
}
