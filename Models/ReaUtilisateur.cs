using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaUtilisateur
{
    public int Id { get; set; }

    public string? NomUtilisateur { get; set; }

    public string? PrenomUtilisateur { get; set; }

    public string? CodeCommercial { get; set; }

    public string? Titre { get; set; }

    public int? IdSite { get; set; }

    public int? IdGroupe { get; set; }

    public byte[]? MotPasse { get; set; }

    public string? EmailUtilisateur { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public string? Login { get; set; }

    public int? PersonnelExterne { get; set; }

    public string? CodeCommercial2 { get; set; }

    public int? IdPartenaire { get; set; }

    public string? Telephone { get; set; }

    public int? TestApplication { get; set; }

    public string? CodeCegid { get; set; }

    public bool UtilisateurTest { get; set; }

    public int IdStatut { get; set; }

    public int? IdUtilisateurRh { get; set; }

    public virtual ReaGroupe? IdGroupeNavigation { get; set; }

    public virtual ReaSite? IdSiteNavigation { get; set; }

    public virtual ReaUtilisateurRh? IdUtilisateurRhNavigation { get; set; }

    public virtual ICollection<ReaDroitProfil> ReaDroitProfils { get; set; } = new List<ReaDroitProfil>();

    public virtual ICollection<ReaDroitRole> ReaDroitRoles { get; set; } = new List<ReaDroitRole>();

    public virtual ICollection<ReaDroitUtilisateur> ReaDroitUtilisateurs { get; set; } = new List<ReaDroitUtilisateur>();
}
