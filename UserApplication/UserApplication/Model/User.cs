using System;
using System.Collections.Generic;

namespace UserApplication.Model;

public partial class User
{
    public string Nickname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Age { get; set; }
}
