namespace UserApplication.Dtos
{
    //public class TvSearchResultDto
    //{
    //    public int Page { get; set; }
    //    public List<TvShowDto> Results { get; set; }
    //    public int TotalResults { get; set; }
    //    public int TotalPages { get; set; }
    //}

    //public class TvShowDto
    //{
    //    public string Name { get; set; }
    //    public string OriginalName { get; set; }
    //    public string Overview { get; set; }
    //    public string FirstAirDate { get; set; }
    //    public double Popularity { get; set; }
    //    public string PosterPath { get; set; }
    //    public string OriginalLanguage { get; set; }
    //}

    public class TvServiceDto
    {
        public int TvId { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalName { get; set; }
        public string Overview { get; set; }
        public string FirstAirDate { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; } // Added VoteCount property

    }
}
