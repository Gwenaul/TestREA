using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestREA.Models;

namespace TestREA.Repositories
{
    public interface IReaAcceRepository
    {
        Task<List<ReaAcce>> GetAllAsync(int page, int pageSize);
        Task<int> CountAsync();
        Task<ReaAcce?> FindAsync(int id);
        Task AddAsync(ReaAcce acce);
        Task UpdateAsync(ReaAcce acce);
        Task DeleteAsync(ReaAcce acce);
        Task<SelectList> GetApplicationsSelectListAsync(int? selectedId = null);
        Task<bool> ReaAcceExistsAsync(int id);

    }
}
