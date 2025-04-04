using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaGroupe
{
    public int Id { get; set; }

    public string? NomGroupe { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaDroitGroupe> ReaDroitGroupes { get; set; } = new List<ReaDroitGroupe>();

    public virtual ICollection<ReaUtilisateur> ReaUtilisateurs { get; set; } = new List<ReaUtilisateur>();
}
