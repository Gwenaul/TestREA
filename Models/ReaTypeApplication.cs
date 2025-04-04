using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class ReaTypeApplication
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<ReaApplication> ReaApplications { get; set; } = new List<ReaApplication>();
}
