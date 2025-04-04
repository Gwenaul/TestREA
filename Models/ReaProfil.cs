using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaProfil
{
    public int Id { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaDroitProfil> ReaDroitProfils { get; set; } = new List<ReaDroitProfil>();
}
