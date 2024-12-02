namespace UserApplication.Dtos
{
    public class TvSearchResultDto
    {
        public List<TvServiceDto> TvShows { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

    }
}
