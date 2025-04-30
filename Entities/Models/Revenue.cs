using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Revenue
{
    public int Id { get; set; }

    public int? UsageId { get; set; }

    public int? Amount { get; set; }

    public DateOnly? TakenDate { get; set; }

    public int? JarId { get; set; }

    public string? Note { get; set; }

    public virtual Jar? Jar { get; set; }

    public virtual Usage? Usage { get; set; }
}
