using JWT.Handler;
using JWT.Models;
using JWT.Models.DB;
using JWT.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly DataContext _context;
        readonly IConfiguration _configuration;
        public AuthController(DataContext content, IConfiguration configuration)
        {
            _context = content;
            _configuration = configuration;
        }
        [HttpPost("/user")]
        public async Task<bool> Create([FromForm] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        [HttpPost("/login")]
        public async Task<Token> Login([FromForm] UserLogin userLogin)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
            if (user != null)
            {
                //Token üretiliyor.
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                //Refresh token Users tablosuna işleniyor.
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return token;
            }
            return null;
        }
        [HttpPost("/refresh/token")]
        public async Task<Models.Token> RefreshToken([FromForm] string token)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token && x.RefreshTokenEndDate > DateTime.Now);
            if (user != null)
            {
                //Token üretiliyor.
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token data = tokenHandler.CreateAccessToken(user);

                //Refresh token Users tablosuna işleniyor.
                user.RefreshToken = data.RefreshToken;
                user.RefreshTokenEndDate = data.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return data;
            }
            return null;
        }
    }
}
