using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Model;
using UserApplication.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TvServiceController : ControllerBase
    {
        private readonly TvService _tvService;

        public TvServiceController(TvService tvService)
        {
            _tvService = tvService;
        }

        [HttpGet("TvSeries-Search")]
        public async Task<IActionResult> SearchTvShows([FromQuery] TvSearch searchModel)
        {
            var result = await _tvService.SearchTvShowsAsync(searchModel);
            return Ok(result);
        }
    }
}
