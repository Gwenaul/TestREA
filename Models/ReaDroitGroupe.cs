using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaDroitGroupe
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public int? IdGroupe { get; set; }

    public int? Autorise { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }

    public virtual ReaGroupe? IdGroupeNavigation { get; set; }
}
