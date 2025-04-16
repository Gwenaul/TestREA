using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChampApplication
{
    public int IdApplication { get; set; }
    public int IdChamp { get; set; }
    public string? Libelle { get; set; }

    public virtual ReaChamp ReaChamp { get; set; } = null!;
}

