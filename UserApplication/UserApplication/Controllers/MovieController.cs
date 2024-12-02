using Microsoft.AspNetCore.Mvc;
using UserApplication.Dtos;
using UserApplication.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpGet("movies/{id}")]
        public ActionResult<MovieDto> Get(int id)
        {
            using (var context = new WebApplication1Context())
            {
                var userLikedMovie = context.UserLikedMovies.FirstOrDefault(m => m.MovieId == id);

                if (userLikedMovie == null)
                {
                    return NotFound("Movie not found.");
                }

                var movieDto = new MovieDto
                {
                    MovieId = userLikedMovie.MovieId,
                    Title = userLikedMovie.Title,
                    Overview = userLikedMovie.Overview,
                    ReleaseDate = userLikedMovie.ReleaseDate
                };

                return Ok(movieDto);
            }
        }

        [HttpGet("movies/by-nickname")]
        public ActionResult<IEnumerable<MovieDto>> GetByNickname([FromQuery] string nickname)
        {
            using (var context = new WebApplication1Context())
            {
                var userLikedMovies = context.UserLikedMovies.Where(m => m.Nickname == nickname).ToList();

                if (!userLikedMovies.Any())
                {
                    return NotFound("No movies found for the given nickname.");
                }

                var movieDtos = userLikedMovies.Select(m => new MovieDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Overview = m.Overview,
                    ReleaseDate = m.ReleaseDate
                }).ToList();

                return Ok(movieDtos);
            }
        }

        // POST api/<MovieController>
        [HttpPost]
        public ActionResult<UserLikedMovie> Post(UserLikedMovie userLikedMovie)
        {
            try
            {
                using (var context = new WebApplication1Context())
                {
                    if (userLikedMovie != null && userLikedMovie.Nickname != null)
                    {
                        // Check if the movie is already liked by this user
                        var existingMovie = context.UserLikedMovies.FirstOrDefault(m => m.Nickname == userLikedMovie.Nickname && m.MovieId == userLikedMovie.MovieId);

                        if (existingMovie != null)
                        {
                            return Conflict("This movie is already liked by the user.");
                        }

                        context.Add(userLikedMovie);
                        context.SaveChanges();

                        return userLikedMovie;
                    }
                    else
                    {
                        return BadRequest("Invalid data.");
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner Exception: " + ex.InnerException.Message;
                }
                return StatusCode(500, $"Internal server error: {errorMessage}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<UserLikedMovie> Delete(int id)
        {
            using (var context = new WebApplication1Context())
            {
                // Verilen movie ID'sine göre UserLikedMovie kaydını buluyoruz
                var userLikedMovie = context.UserLikedMovies.FirstOrDefault(m => m.MovieId == id);

                if (userLikedMovie == null)
                {
                    return NotFound("Movie not found.");
                }

                var deletedMovie = new UserLikedMovie
                {
                    MovieId = userLikedMovie.MovieId,
                    Nickname = userLikedMovie.Nickname,
                    Title = userLikedMovie.Title,
                    Overview = userLikedMovie.Overview,
                    ReleaseDate = userLikedMovie.ReleaseDate,
                    PosterPath = userLikedMovie.PosterPath,
                    VoteAverage = userLikedMovie.VoteAverage,
                    VoteCount = userLikedMovie.VoteCount,
                };
                context.UserLikedMovies.Remove(userLikedMovie);
                context.SaveChanges();


                return Ok(deletedMovie);
            }
        }
    }
}
