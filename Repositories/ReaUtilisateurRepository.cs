using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestREA.Models;
using TestREA.Repositories;

namespace TestREA.Repositories
{
    public class ReaUtilisateurRepository : IReaUtilisateurRepository
    {
        private readonly AppDbContext _context;

        public ReaUtilisateurRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReaUtilisateur>> GetUtilisateursAsync(string filtreSaisie, bool utilisateurTest, int page, int nombreLignes)
        {
            var query = _context.ReaUtilisateurs
                .Include(r => r.IdStatutNavigation)
                .Include(r => r.IdGroupeNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtreSaisie))
            {
                query = query.Where(u => u.NomUtilisateur.Contains(filtreSaisie)
                    || u.EmailUtilisateur.Contains(filtreSaisie)
                    || u.PrenomUtilisateur.Contains(filtreSaisie));
            }

            if (utilisateurTest)
            {
                query = query.Where(u => u.UtilisateurTest == true);
            }

            return await query
                .Skip((page - 1) * nombreLignes)
                .Take(nombreLignes)
                .ToListAsync();
        }

        public async Task<int> CountUtilisateursAsync(string filtreSaisie, bool utilisateurTest)
        {
            var query = _context.ReaUtilisateurs.AsQueryable();

            if (!string.IsNullOrEmpty(filtreSaisie))
            {
                query = query.Where(u => u.NomUtilisateur.Contains(filtreSaisie)
                    || u.EmailUtilisateur.Contains(filtreSaisie)
                    || u.PrenomUtilisateur.Contains(filtreSaisie));
            }

            if (utilisateurTest)
            {
                query = query.Where(u => u.UtilisateurTest == true);
            }

            return await query.CountAsync();
        }

        public async Task<ReaUtilisateur?> GetDetailsAsync(int id)
        {

            return await _context.ReaUtilisateurs
                .Include(r => r.IdStatutNavigation)
                .Include(r => r.IdGroupeNavigation).ThenInclude(r => r.ReaDroitGroupes).ThenInclude(rr => rr.IdApplicationNavigation)
                .Include(r => r.IdSiteNavigation)
                .Include(r => r.IdUtilisateurRhNavigation)
                .Include(r => r.ReaDroitProfils).ThenInclude(rr => rr.IdProfilNavigation).ThenInclude(rrr => rrr.ReaChampProfils).ThenInclude(cp => cp.ReaChamp).ThenInclude(cpp => cpp.ReaChampApplications)
                .Include(r => r.ReaDroitRoles).ThenInclude(rr => rr.IdRoleNavigation)
                .Include(r => r.ReaDroitUtilisateurs).ThenInclude(rr => rr.IdApplicationNavigation)
                .Include(r => r.ReaVerrous).ThenInclude(rr => rr.IdApplicationNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ReaUtilisateur?> FindAsync(int id)
        {
            return await _context.ReaUtilisateurs.FindAsync(id);
        }

        public void Add(ReaUtilisateur u) => _context.Add(u);

        public void Update(ReaUtilisateur u) => _context.Update(u);

        public void Remove(ReaUtilisateur u) => _context.ReaUtilisateurs.Remove(u);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public bool Exists(int id) => _context.ReaUtilisateurs.Any(e => e.Id == id);

        public async Task<(SelectList statuts, SelectList groupes, SelectList sites, SelectList utilisateursRh, SelectList droitsGroupes)> GetSelectListsAsync(ReaUtilisateur utilisateur)
        {
            var statuts = await _context.ReaStatuts.ToListAsync();
            var groupes = await _context.ReaGroupes.ToListAsync();
            var sites = await _context.ReaSites.ToListAsync();
            var utilisateursRh = await _context.ReaUtilisateurRhs.ToListAsync();
            var droitsGroupes = await _context.ReaDroitGroupes
                .Where(d => d.IdGroupe == utilisateur.IdGroupe)
                .Select(d => new 
                { 
                    d.Id, 
                    d.IdApplicationNavigation.Application, 
                    d.Autorise 
                })
                .ToListAsync();


            return (
                new SelectList(statuts, "Id", "Libelle"),
                new SelectList(groupes, "Id", "Libelle"),
                new SelectList(sites, "Id", "NomSite"),
                new SelectList(utilisateursRh, "Id", "Id"),
                new SelectList(droitsGroupes, "Id", "Application")
            );
        }

    }
}
