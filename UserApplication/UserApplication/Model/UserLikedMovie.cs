using System;
using System.Collections.Generic;

namespace UserApplication.Model;

public partial class UserLikedMovie
{
    public string Nickname { get; set; } = null!;

    public string? Title { get; set; }

    public int MovieId { get; set; }

    public string? Overview { get; set; }

    public string? ReleaseDate { get; set; }

    public string? PosterPath { get; set; }

    public double? VoteAverage { get; set; }

    public int? VoteCount { get; set; }
}
