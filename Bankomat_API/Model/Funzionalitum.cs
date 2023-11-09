using System;
using System.Collections.Generic;

namespace Bankomat_API.Model;

public partial class Funzionalitum
{
    public long Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<BancheFunzionalitum> BancheFunzionalita { get; set; } = new List<BancheFunzionalitum>();
}
