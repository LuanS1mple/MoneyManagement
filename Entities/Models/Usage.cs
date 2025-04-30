using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Usage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? TypeId { get; set; }

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();

    public virtual TypeUsage? Type { get; set; }
}
