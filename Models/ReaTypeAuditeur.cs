using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaTypeAuditeur
{
    public int Id { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaAuditeur> ReaAuditeurs { get; set; } = new List<ReaAuditeur>();
}
