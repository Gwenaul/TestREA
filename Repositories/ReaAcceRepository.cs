using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestREA.Models;
using TestREA.Repositories;

namespace TestREA.Repositories
{
    public class ReaAcceRepository : IReaAcceRepository
    {
        private readonly AppDbContext _context;

        public ReaAcceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReaAcce>> GetAllAsync(int page, int pageSize)
        {
            return await _context.ReaAcces
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.ReaAcces.CountAsync();
        }

        public async Task<ReaAcce?> FindAsync(int id)
        {
            return await _context.ReaAcces
                .Include(r => r.IdApplicationNavigation)
                .Include(r => r.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ReaAcce acce)
        {
            _context.ReaAcces.Add(acce);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReaAcce acce)
        {
            _context.ReaAcces.Update(acce);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ReaAcce acce)
        {
            _context.ReaAcces.Remove(acce);
            await _context.SaveChangesAsync();
        }

        public async Task<SelectList> GetApplicationsSelectListAsync(int? selectedId = null)
        {
            var apps = await _context.ReaApplications
                .OrderBy(a => a.Id)
                .ToListAsync();

            return new SelectList(apps, "Id", "Id", selectedId);
        }

        public async Task<bool> ReaAcceExistsAsync(int id)
        {
            return await _context.ReaAcces.AnyAsync(e => e.Id == id);
        }

    }
}
