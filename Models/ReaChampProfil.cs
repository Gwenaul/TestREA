using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChampProfil
{
    public int IdChamp { get; set; }
    public int IdProfil { get; set; }
    public bool Autorise { get; set; }

    // Relation inverse : une ligne profil est lié à une ligne champ
    public virtual ReaChamp ReaChamp { get; set; } = null!;
    // Relation inverse : une ligne profil est liée à un profil
    public virtual ReaProfil IdProfilNavigation { get; set; } = null!;
}

