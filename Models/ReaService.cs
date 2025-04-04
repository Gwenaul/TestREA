using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaService
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public DateTime? DateExecution { get; set; }

    public int? Traitement { get; set; }

    public string? Detail { get; set; }

    public int? Niveau { get; set; }

    public int? Unite { get; set; }

    public DateTime? DateIntervention { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }

    public virtual ReaNiveau? NiveauNavigation { get; set; }
}
