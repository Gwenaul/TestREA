using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChampVerrou
{
    public int IdChamp { get; set; }
    public int IdVerrou { get; set; }
    public int IdApplication { get; set; }

    // Relation inverse : une ligne verrou est lié à une ligne champ
    public virtual ReaChamp ReaChamp { get; set; } = null!;
    // Relation inverse : une ligne verrou est liée à une application
    public virtual ReaApplication IdApplicationNavigation { get; set; } = null!;
}
