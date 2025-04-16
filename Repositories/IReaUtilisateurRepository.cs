using Microsoft.AspNetCore.Mvc.Rendering;
using TestREA.Models;

namespace TestREA.Repositories
{
    public interface IReaUtilisateurRepository
    {
        Task<List<ReaUtilisateur>> GetUtilisateursAsync(string filtreSaisie, bool utilisateurTest, int page, int nombreLignes);
        Task<int> CountUtilisateursAsync(string filtreSaisie, bool utilisateurTest);
        Task<ReaUtilisateur?> GetDetailsAsync(int id);
        Task<ReaUtilisateur?> FindAsync(int id);
        void Add(ReaUtilisateur u);
        void Update(ReaUtilisateur u);
        void Remove(ReaUtilisateur u);
        Task SaveChangesAsync();
        bool Exists(int id);
        Task<(SelectList statuts, SelectList groupes, SelectList sites, SelectList utilisateursRh, SelectList droitsGroupes)> GetSelectListsAsync(ReaUtilisateur utilisateur);
    }
}
