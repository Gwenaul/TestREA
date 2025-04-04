using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaTache
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public string? HeureExecution { get; set; }

    public string? Serveur { get; set; }

    public string? AdresseIp { get; set; }

    public int? Actif { get; set; }

    public string? Commentaire { get; set; }

    public string? DetailPeriodicite { get; set; }

    public string? CompteService { get; set; }
}
