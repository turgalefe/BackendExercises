using Newtonsoft.Json.Linq;
using RestSharp;
using UserApplication.Dtos;
using UserApplication.Model;

namespace UserApplication.Service
{
    public class TvService
    {
        private readonly RestClient _client;

        public TvService(HttpClient httpClient)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3")
            {
                ThrowOnAnyError = true,
            };
            _client = new RestClient(options);
        }

        //public async Task<List<TvDto>> SearchTvShowsAsync(TvSearch searchModel)
        public async Task<TvSearchResultDto> SearchTvShowsAsync(TvSearch searchModel)
        {
            var request = new RestRequest("search/tv");

            // Add query parameters, including the API key
            request.AddParameter("api_key", "4000d829361304ad4ef8c5acc6f6247d"); // Replace with your actual API key
            request.AddParameter("query", searchModel.Query);
            request.AddParameter("first_air_date_year", searchModel.first_air_date_year);
            request.AddParameter("language", searchModel.Language);
            request.AddParameter("include_adult", searchModel.IncludeAdult);
            request.AddParameter("page", searchModel.Page);

            if (!string.IsNullOrEmpty(searchModel.Year))
            {
                request.AddParameter("first_air_date_year", searchModel.first_air_date_year);
            }

            var response = await _client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var content = JObject.Parse(response.Content);

                // Extract total_pages and total_results
                int totalPages = (int)content["total_pages"];
                int totalResults = (int)content["total_results"];

                // Extract Tv shows list
                var results = content["results"];
                var tvShows = results.Select(result => new TvServiceDto
                {
                    TvId = (int)result["id"], // Map Id
                    OriginalLanguage = result["original_language"].ToString(), // Map OriginalLanguage
                    OriginalName = result["original_name"].ToString(), // Map OriginalName
                    Overview = result["overview"].ToString(),
                    FirstAirDate = result["first_air_date"].ToString(), // Map FirstAirDate
                    VoteAverage = (double)result["vote_average"],
                    VoteCount = (int)result["vote_count"] // Map VoteCount
                }).ToList();

                // Return the combined result
                return new TvSearchResultDto
                {

                    TvShows = tvShows,
                    TotalPages = totalPages,
                    TotalResults = totalResults
                };
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
        }
    }
}
