using System;
using System.Collections.Generic;

namespace ProjectFinal.NET.Data;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool IsAdmin { get; set; }
}
