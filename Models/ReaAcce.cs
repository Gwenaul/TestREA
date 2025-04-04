using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaAcce
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public DateTime? DateConnexion { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }
}
