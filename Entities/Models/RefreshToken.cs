using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpireTime { get; set; }

    public bool IsEnable { get; set; }
}
