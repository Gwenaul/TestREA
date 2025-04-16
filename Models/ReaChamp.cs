using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaChamp
{
    public int Id { get; set; }
    public string? Description { get; set; }

    // Relation : un champ peut avoir 0 ou 1 verrou
    public virtual ReaChampVerrou? ReaChampVerrou { get; set; }
    // Relation : un champ peut avoir 0 ou plusieurs profils
    public virtual ICollection<ReaChampProfil> ReaChampProfils { get; set; } = new List<ReaChampProfil>();
    // Relation : un champ peut avoir 0 ou plusieurs applications
    public virtual ICollection<ReaChampApplication> ReaChampApplications { get; set; } = new List<ReaChampApplication>();
}


