using Newtonsoft.Json.Linq;
using RestSharp;
using UserApplication.Dtos;
using UserApplication.Model;

namespace UserApplication.Service
{
    public class MovieService
    {
        private readonly RestClient _client;

        public MovieService(HttpClient httpClient)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3")
            {
                ThrowOnAnyError = true,
            };
            _client = new RestClient(options);
        }

        public async Task<List<MovieServiceDto>> SearchMoviesAsync(MovieSearch searchModel)
        {
            var request = new RestRequest("search/movie");

            // Add headers
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0MDAwZDgyOTM2MTMwNGFkNGVmOGM1YWNjNmY2MjQ3ZCIsIm5iZiI6MTcyMzY0MjMwNC4yNjc5NCwic3ViIjoiNjZhMjJkNTc4MzU4Y2E1MDI0ODBmMWZhIiwic2NvcGVzIjpbImFwaV9yZWFkIl0sInZlcnNpb24iOjF9.cX8FR6vg_hLIKtPgD78X__jp2h3hXpf185BdM7QhKAg");
            //request.AddHeader("accept", "application/json");

            // Add query parameters, including the API key
            request.AddParameter("api_key", "4000d829361304ad4ef8c5acc6f6247d"); // Replace with your actual API key
            request.AddParameter("query", searchModel.Query);
            request.AddParameter("include_adult", searchModel.IncludeAdult);
            request.AddParameter("language", searchModel.Language);
            request.AddParameter("page", searchModel.Page);


            if (!string.IsNullOrEmpty(searchModel.PrimaryReleaseYear))
            {
                request.AddParameter("primary_release_year", searchModel.PrimaryReleaseYear);
            }

            if (!string.IsNullOrEmpty(searchModel.Region))
            {
                request.AddParameter("region", searchModel.Region);
            }

            if (!string.IsNullOrEmpty(searchModel.Year))
            {
                request.AddParameter("year", searchModel.Year);
            }

            var response = await _client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var content = JObject.Parse(response.Content);
                var results = content["results"];

                var movies = results.Select(result => new MovieServiceDto
                {
                    MovieId = (int)result["id"], // Map Id
                    Title = result["title"].ToString(),
                    Overview = result["overview"].ToString(),
                    ReleaseDate = result["release_date"].ToString(),
                    PosterPath = result["poster_path"].ToString(),
                    VoteAverage = (double)result["vote_average"],
                    VoteCount = (int)result["vote_count"] // Map VoteCount
                }).ToList();

                return movies;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
        }
    }
}
