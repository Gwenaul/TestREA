namespace TestREA.Models;

public partial class ReaVerrou
{
    public int Id { get; set; }

    public int? IdUtilisateur { get; set; }

    public int? NbTentative { get; set; }

    public DateTime? DateVerrou { get; set; }

    public int? IdApplication { get; set; }

    public virtual ReaApplication? IdApplicationNavigation { get; set; }

    public virtual ReaUtilisateur IdUtilisateurNavigation { get; set; }

}

