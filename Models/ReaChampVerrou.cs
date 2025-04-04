using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChampVerrou
{
    public int? IdChamp { get; set; }

    public int? IdVerrou { get; set; }

    public int? IdApplication { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }
}
