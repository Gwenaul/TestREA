using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestREA.Models;
using System.Text.Json;

namespace TestREA.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Test(int id)
        {
            var utilisateur = await _context.ReaUtilisateurs
                .Include(u => u.IdStatutNavigation)
                .Include(u => u.IdGroupeNavigation)
                    .ThenInclude(g => g.ReaDroitGroupes)
                        .ThenInclude(dg => dg.IdApplicationNavigation)
                .Include(u => u.ReaDroitProfils)
                    .ThenInclude(dp => dp.IdProfilNavigation)
                        .ThenInclude(p => p.ReaChampProfils)
                            .ThenInclude(cp => cp.ReaChamp)
                                .ThenInclude(c => c.ReaChampApplications)
                .Include(u => u.ReaDroitRoles)
                    .ThenInclude(dr => dr.IdRoleNavigation)
                .Include(u => u.ReaDroitUtilisateurs)
                    .ThenInclude(du => du.IdApplicationNavigation)
                .Include(u => u.ReaVerrous)
                    .ThenInclude(v => v.IdApplicationNavigation)
                .Include(u => u.IdSiteNavigation)
                .Include(u => u.IdUtilisateurRhNavigation)
                .Include(u => u.ReaAcces
                    .Where(a => a.DateConnexion >= DateTime.Now.AddMonths(-12)))
                    .ThenInclude(a => a.IdApplicationNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            //var accessData = utilisateur.ReaAcces
            //    .Where(a => a.DateConnexion >= DateTime.Now.AddMonths(-12))
            //    .Select(a => new {
            //        date = a.DateConnexion,
            //        app = a.IdApplicationNavigation.Application
            //        })
            //    .ToList();

            var accessData = utilisateur.ReaAcces
                .Where(a => a.DateConnexion >= DateTime.Now.AddMonths(-12))
                .GroupBy(a => new {
                    Date = a.DateConnexion.Value.Date,
                    App = a.IdApplicationNavigation.Application
                    })
                .Select(g => new {
                    date = g.Key.Date,
                    app = g.Key.App,
                    count = g.Count()
                    })
                .ToList();

            ViewData["AccesChartJson"] = JsonSerializer.Serialize(new
            {
                dateConnexion = accessData.Select(a => a.date),
                application = accessData.Select(a => a.app)
            });

            return View("TestDashboard", utilisateur);
        }
    }
}
