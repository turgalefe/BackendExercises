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
    public class MovieServiceController : ControllerBase
    {
        private readonly MovieService _movieDbService;

        public MovieServiceController(MovieService movieDbService)
        {
            _movieDbService = movieDbService;
        }

        [HttpGet("Movie-search")]
        public async Task<IActionResult> SearchMovies([FromQuery] MovieSearch searchModel)
        {
            var result = await _movieDbService.SearchMoviesAsync(searchModel);
            return Ok(result);
        }
    }
}
