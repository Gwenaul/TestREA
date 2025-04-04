using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaDroitRole
{
    public int Id { get; set; }

    public int? IdRole { get; set; }

    public int? IdUtilisateur { get; set; }

    public int? Autorise { get; set; }

    public virtual ReaRole? IdRoleNavigation { get; set; }

    public virtual ReaUtilisateur? IdUtilisateurNavigation { get; set; }
}
