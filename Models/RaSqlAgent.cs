using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class RaSqlAgent
{
    public int? InstanceId { get; set; }

    public string? Name { get; set; }

    public int? StatutExecution { get; set; }

    public int? DateExecution { get; set; }

    public int? HeureExecution { get; set; }

    public int? DureeExecution { get; set; }

    public int? StepId { get; set; }

    public string? Message { get; set; }

    public string? Server { get; set; }

    public DateTime? DateCreation { get; set; }
}
