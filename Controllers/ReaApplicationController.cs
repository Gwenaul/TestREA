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
    public class ReaApplicationController : Controller
    {
        private readonly AppDbContext _context;

        public ReaApplicationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReaApplication
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReaApplications.Include(r => r.IdTypeNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReaApplication/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaApplication = await _context.ReaApplications
                .Include(r => r.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaApplication == null)
            {
                return NotFound();
            }

            return View(reaApplication);
        }

        // GET: ReaApplication/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.ReaTypeApplications, "Id", "Id");
            return View();
        }

        // POST: ReaApplication/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Application,IdType,Commentaire")] ReaApplication reaApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reaApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdType"] = new SelectList(_context.ReaTypeApplications, "Id", "Id", reaApplication.IdType);
            return View(reaApplication);
        }

        // GET: ReaApplication/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaApplication = await _context.ReaApplications.FindAsync(id);
            if (reaApplication == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.ReaTypeApplications, "Id", "Type", reaApplication.IdType);
            return View(reaApplication);
        }

        // POST: ReaApplication/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Application,IdType,Commentaire")] ReaApplication reaApplication)
        {
            if (id != reaApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaApplicationExists(reaApplication.Id))
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
            ViewData["IdType"] = new SelectList(_context.ReaTypeApplications, "Id", "Id", reaApplication.IdType);
            return View(reaApplication);
        }

        // GET: ReaApplication/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaApplication = await _context.ReaApplications
                .Include(r => r.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaApplication == null)
            {
                return NotFound();
            }

            return View(reaApplication);
        }

        // POST: ReaApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaApplication = await _context.ReaApplications.FindAsync(id);
            if (reaApplication != null)
            {
                _context.ReaApplications.Remove(reaApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaApplicationExists(int id)
        {
            return _context.ReaApplications.Any(e => e.Id == id);
        }
    }
}
