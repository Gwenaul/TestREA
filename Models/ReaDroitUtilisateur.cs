using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaDroitUtilisateur
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public int? IdUtilisateur { get; set; }

    public bool Autorise { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }

    public virtual ReaUtilisateur? IdUtilisateurNavigation { get; set; }
}
