using System;
using System.Collections.Generic;

namespace UserApplication.Model;

public partial class UserLikedTv
{
    public string? Nickname { get; set; }

    public int TvId { get; set; }

    public string? OriginalLanguage { get; set; }

    public string? OriginalName { get; set; }

    public string? Overview { get; set; }

    public string? FirstAirDate { get; set; }

    public double? VoteAverage { get; set; }

    public int? VoteCount { get; set; }
}
