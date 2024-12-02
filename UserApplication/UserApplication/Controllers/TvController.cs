using Microsoft.AspNetCore.Mvc;
using UserApplication.Dtos;
using UserApplication.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TvController : ControllerBase
    {
        [HttpGet("tv/{id}")]
        public ActionResult<TvDto> Get(int id)
        {
            using (var context = new WebApplication1Context())
            {
                var userLikedTv = context.UserLikedTvs
                    .FirstOrDefault(m => m.TvId == id);

                if (userLikedTv == null)
                {
                    return NotFound("Movie not found.");
                }

                var tvDto = new TvDto
                {
                    TvId = userLikedTv.TvId,
                    OriginalName = userLikedTv.OriginalName,
                    Overview = userLikedTv.Overview,
                    FirstAirDate = userLikedTv.FirstAirDate
                };

                return Ok(tvDto);
            }
        }

        [HttpGet("tvshows/by-nickname")]
        public ActionResult<IEnumerable<TvDto>> GetByNickname([FromQuery] string nickname)
        {
            using (var context = new WebApplication1Context())
            {
                var userLikedTvs = context.UserLikedTvs
                    .Where(m => m.Nickname == nickname)
                    .ToList();

                if (!userLikedTvs.Any())
                {
                    return NotFound("No TV shows found for the given nickname.");
                }

                var tvDtos = userLikedTvs.Select(m => new TvDto
                {
                    TvId = m.TvId,
                    OriginalName = m.OriginalName,
                    Overview = m.Overview,
                    FirstAirDate = m.FirstAirDate
                }).ToList();

                return Ok(tvDtos);
            }
        }

       
        [HttpPost]
        public ActionResult<UserLikedTv> Post(UserLikedTv userLikedTv)
        {
            try
            {
                using (var context = new WebApplication1Context())
                {
                    if (userLikedTv != null && userLikedTv.Nickname != null)
                    {
                        // Check if the movie is already liked by this user
                        var existingMovie = context.UserLikedMovies
                                                   .FirstOrDefault(m => m.Nickname == userLikedTv.Nickname && m.MovieId == userLikedTv.TvId);

                        if (existingMovie != null)
                        {
                            return Conflict("This movie is already liked by the user.");
                        }

                        context.Add(userLikedTv);
                        context.SaveChanges();

                        return userLikedTv;
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
        public ActionResult<UserLikedTv> Delete(int id)
        {
            using (var context = new WebApplication1Context())
            {
                // Verilen movie ID'sine göre UserLikedMovie kaydını buluyoruz
                var userLikedTv = context.UserLikedTvs.FirstOrDefault(m => m.TvId == id);

                if (userLikedTv == null)
                {
                    return NotFound("Movie not found.");
                }

                var deletedTvShow = new UserLikedTv
                {
                    TvId = userLikedTv.TvId,
                    Nickname = userLikedTv.Nickname,
                    OriginalLanguage = userLikedTv.OriginalLanguage,
                    OriginalName = userLikedTv.OriginalName,
                    Overview = userLikedTv.Overview,
                    FirstAirDate = userLikedTv.FirstAirDate,
                    VoteAverage = userLikedTv.VoteAverage,
                    VoteCount = userLikedTv.VoteCount,
                };

                context.UserLikedTvs.Remove(userLikedTv);
                context.SaveChanges();


                return Ok(deletedTvShow);
            }
        }
    }
}
