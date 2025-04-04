using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaAuditeur
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public byte[]? MotPasse { get; set; }

    public string? CodeTiers { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DerniereConnexion { get; set; }

    public int? IdTypeAuditeur { get; set; }

    public DateTime? DateNaissance { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Civilite { get; set; }

    public virtual ReaTypeAuditeur? IdTypeAuditeurNavigation { get; set; }
}
