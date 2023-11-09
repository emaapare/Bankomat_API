using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankomat_API.Model;

public partial class Utenti
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Il campo Id Banca è obbligatorio")]
    public long IdBanca { get; set; }

    [Required(ErrorMessage = "Il campo Nome Utente è obbligatorio")]
    public string NomeUtente { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Password è obbligatorio")]
    public string Password { get; set; } = null!;

    public bool Bloccato { get; set; }

    public virtual ICollection<ContiCorrente> ContiCorrentes { get; set; } = new List<ContiCorrente>();

    public virtual Banche IdBancaNavigation { get; set; } = null!;
}
