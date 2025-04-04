using System;
using System.Collections.Generic;

namespace TestREA.Models;

public partial class VueReaAccesMobile
{
    public string? Login { get; set; }

    public byte[]? MotPasse { get; set; }

    public string? CodeTiers { get; set; }

    public int IdTypeAuditeur { get; set; }

    public string? TLibelle { get; set; }

    public DateTime? DateNaissance { get; set; }

    public string? TTiers { get; set; }
}
