using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaApplication
{
    public int Id { get; set; }

    public string? Application { get; set; }

    public int? IdType { get; set; }

    public string? Commentaire { get; set; }

    public virtual ReaTypeApplication? IdTypeNavigation { get; set; }

    public virtual ICollection<ReaAcce> ReaAcces { get; set; } = new List<ReaAcce>();

    public virtual ICollection<ReaDroitGroupe> ReaDroitGroupes { get; set; } = new List<ReaDroitGroupe>();

    public virtual ICollection<ReaDroitUtilisateur> ReaDroitUtilisateurs { get; set; } = new List<ReaDroitUtilisateur>();

    public virtual ICollection<ReaService> ReaServices { get; set; } = new List<ReaService>();

    public virtual ICollection<ReaVerrou> ReaVerrous { get; set; } = new List<ReaVerrou>();

    public virtual ICollection<ReaChampVerrou> ReaChampVerrous { get; set; } = new List<ReaChampVerrou>();
}
