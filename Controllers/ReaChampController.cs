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
    public class ReaChampController : Controller
    {
        private readonly AppDbContext _context;

        public ReaChampController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaChamp
        public async Task<IActionResult> Index()
        {
            var reaChamp = await _context.ReaChamps
                .Include(r => r.ReaChampApplications)
                .Include(r => r.ReaChampVerrou)
                .ThenInclude(r => r.IdApplicationNavigation)
                .Include(r => r.ReaChampProfils)
                .ThenInclude(r => r.IdProfilNavigation)
                .ToListAsync();

            return View(reaChamp);
        }

        // GET: ReaChamp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaChamp = await _context.ReaChamps
                .Include(r => r.ReaChampApplications)
                .Include(r => r.ReaChampVerrou)
                .ThenInclude(r => r.IdApplicationNavigation)
                .Include(r => r.ReaChampProfils)
                .ThenInclude(r => r.IdProfilNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaChamp == null)
            {
                return NotFound();
            }

            return View(reaChamp);
        }

        // GET: ReaChamp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReaChamp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ReaChamp reaChamp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaChamp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reaChamp);
        }

        // GET: ReaChamp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaChamp = await _context.ReaChamps.FindAsync(id);
            if (reaChamp == null)
            {
                return NotFound();
            }
            return View(reaChamp);
        }

        // POST: ReaChamp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ReaChamp reaChamp)
        {
            if (id != reaChamp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaChamp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaChampExists(reaChamp.Id))
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
            return View(reaChamp);
        }

        // GET: ReaChamp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaChamp = await _context.ReaChamps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaChamp == null)
            {
                return NotFound();
            }

            return View(reaChamp);
        }

        // POST: ReaChamp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaChamp = await _context.ReaChamps.FindAsync(id);
            if (reaChamp != null)
            {
                _context.ReaChamps.Remove(reaChamp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaChampExists(int id)
        {
            return _context.ReaChamps.Any(e => e.Id == id);
        }
    }
}
