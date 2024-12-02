namespace UserApplication.Dtos
{
    public class MovieServiceDto
    {
        public int MovieId { get; set; } // Added Id property
        public string Title { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; } // Added VoteCount property
    }
}
