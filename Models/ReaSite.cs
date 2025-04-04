using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaSite
{
    public int Id { get; set; }

    public string? NomSite { get; set; }

    public int? IdDirection { get; set; }

    public string? CodeSite { get; set; }

    public string? CodeDepartement { get; set; }

    public string? Adresse { get; set; }

    public int? Archive { get; set; }

    public virtual ReaDirection? IdDirectionNavigation { get; set; }

    public virtual ICollection<ReaUtilisateur> ReaUtilisateurs { get; set; } = new List<ReaUtilisateur>();
}
