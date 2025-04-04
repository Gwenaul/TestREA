using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaRole
{
    public int Id { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaDroitRole> ReaDroitRoles { get; set; } = new List<ReaDroitRole>();
}
