using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaUtilisateurRh
{
    public int Id { get; set; }

    public DateTime DateEntree { get; set; }

    public DateTime? FinContrat { get; set; }

    public DateTime DateNaissance { get; set; }

    public string? Consignes { get; set; }

    public string? AccesApp { get; set; }

    public string? NomUtilisateurRh { get; set; }

    public string? PrenomUtilisateurRh { get; set; }

    public virtual ICollection<ReaUtilisateur> ReaUtilisateurs { get; set; } = new List<ReaUtilisateur>();
}
