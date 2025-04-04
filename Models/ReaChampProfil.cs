using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChampProfil
{
    public int IdChamp { get; set; }

    public int IdProfil { get; set; }

    public int Autorise { get; set; }

    public virtual ReaProfil IdProfilNavigation { get; set; } = null!;
}
