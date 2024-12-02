using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    // Film Ekleme
    [HttpPost]
    public async Task<IActionResult> AddMovie([FromBody] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Film başarıyla eklendi." });
        }
        return BadRequest(new { success = false, message = "Geçersiz veri gönderildi." });
    }

    // Tüm Filmleri Getirme
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }

    // Film Silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound(new { success = false, message = "Film bulunamadı." });
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return Ok(new { success = true, message = "Film başarıyla silindi." });
    }

    // Film Güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
        if (id != updatedMovie.Id)
        {
            return BadRequest(new { success = false, message = "ID'ler eşleşmiyor." });
        }

        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound(new { success = false, message = "Film bulunamadı." });
        }

        // Mevcut değerleri güncelle
        movie.Title = updatedMovie.Title;
        movie.Description = updatedMovie.Description;
        movie.ReleaseDate = updatedMovie.ReleaseDate;
        movie.Genre = updatedMovie.Genre;

        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "Film başarıyla güncellendi." });
    }
}
