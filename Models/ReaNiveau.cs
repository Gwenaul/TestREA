using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaNiveau
{
    public int Id { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaService> ReaServices { get; set; } = new List<ReaService>();
}
