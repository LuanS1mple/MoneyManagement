using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TypeUsage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Usage> Usages { get; set; } = new List<Usage>();
}
