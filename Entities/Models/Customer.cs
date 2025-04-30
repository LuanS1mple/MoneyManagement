using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Jar> Jars { get; set; } = new List<Jar>();
}
