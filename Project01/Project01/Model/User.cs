using System;
using System.Collections.Generic;

namespace Project01.Model;

public partial class User
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? Age { get; set; }

    public int Id { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenEndDate { get; set; }
}
