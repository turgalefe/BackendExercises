namespace UserApplication.Dtos
{
    public class FindResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string AirDate { get; set; }
        public int EpisodeNumber { get; set; }

    }
}
