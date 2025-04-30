using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Jar
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Total { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();
}
