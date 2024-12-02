using System;
using System.Collections.Generic;

namespace Project01.Model;

public partial class UserLiked
{
    public int UserId { get; set; }

    public bool IsLiked { get; set; }

    public int? Ratings { get; set; }

    public string? Comments { get; set; }

    public string? PostComments { get; set; }
}
