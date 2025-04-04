using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaDirection
{
    public int Id { get; set; }

    public string? NomDirection { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<ReaSite> ReaSites { get; set; } = new List<ReaSite>();
}
