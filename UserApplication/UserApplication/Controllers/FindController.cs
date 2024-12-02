using Microsoft.AspNetCore.Mvc;
using RestSharp;
using UserApplication.Model;
using UserApplication.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApplication.Controllers
{
    public class FindController : ControllerBase
    {
        private readonly FindService _findService;

        public FindController()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3")
            {
                ThrowOnAnyError = true,
            };
            var client = new RestClient(options);
            _findService = new FindService(client);
        }

        [HttpGet("Find-TvSeries-Movie")]
        public async Task<IActionResult> FindTvShow([FromQuery] FindResult findModel)
        {
            try
            {
                var result = await _findService.FindTvShowAsync(findModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
