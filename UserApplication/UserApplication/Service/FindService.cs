using UserApplication.Dtos;
using UserApplication.Model;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace UserApplication.Service
{
    public class FindService
    {
        private readonly RestClient _client;

        public FindService(RestClient client)
        {
            _client = client;
        }

        public async Task<FindResultDto> FindTvShowAsync(FindResult findResult)
        {
            var request = new RestRequest($"find/{findResult.externalId}");

            request.AddParameter("api_key", "4000d829361304ad4ef8c5acc6f6247d"); // Replace with your actual API key
            request.AddParameter("external_source", findResult.externalSource);
            if (!string.IsNullOrEmpty(findResult.language))
            {
                request.AddParameter("language", findResult.language);
            }

            var response = await _client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var content = JObject.Parse(response.Content);

                var result = content["tv_results"]?.FirstOrDefault() ??
                             content["tv_episode_results"]?.FirstOrDefault() ??
                             content["tv_season_results"]?.FirstOrDefault();

                if (result != null)
                {
                    return new FindResultDto
                    {
                        Id = (int)result["id"],
                        Name = result["name"].ToString(),
                        Overview = result["overview"].ToString(),
                        VoteAverage = (double)result["vote_average"],
                        VoteCount = (int)result["vote_count"],
                        AirDate = result["air_date"].ToString(),
                        EpisodeNumber = (int)result["episode_number"]
                    };
                }
                else
                {
                    throw new Exception("No TV show results found.");
                }
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }
        }
    }
}
