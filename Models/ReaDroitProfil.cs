using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaDroitProfil
{
    public int Id { get; set; }

    public int? IdUtilisateur { get; set; }

    public int? IdProfil { get; set; }

    public bool Autorise { get; set; }

    public virtual ReaProfil? IdProfilNavigation { get; set; }

    public virtual ReaUtilisateur? IdUtilisateurNavigation { get; set; }
}
