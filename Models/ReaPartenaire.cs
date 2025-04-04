using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaPartenaire
{
    public int Id { get; set; }

    public string? NomPartenaire { get; set; }

    public int? IdOpenPortal { get; set; }

    public int? IdYpareo { get; set; }
}
