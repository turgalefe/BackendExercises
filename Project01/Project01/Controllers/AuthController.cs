using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project01.Model;
using Project01.ViewModels;
using Project01.Handler;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project01.Controllers
{
    
        public class AuthController : ControllerBase
        {
            readonly Project01Context _context;
            readonly IConfiguration _configuration;
            public AuthController(Project01Context content, IConfiguration configuration)
            {
                _context = content;
                _configuration = configuration;
            }

            //[HttpPost("/user")]
            //public async Task<bool> Create([FromForm] User user)
            //{
            //    _context.Users.Add(user);
            //    await _context.SaveChangesAsync();
            //    return true;
            //}

            [HttpPost("/login")]
            public async Task<Token> Login([FromForm] UserLogin userLogin)
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
                if (user != null)
                {
                    //Token üretiliyor.
                    Handler.TokenHandler tokenHandler = new Handler.TokenHandler(_configuration);
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
            public async Task<Model.Token> RefreshToken([FromForm] string token)
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token && x.RefreshTokenEndDate > DateTime.Now);
                if (user != null)
                {
                    //Token üretiliyor.
                    Handler.TokenHandler tokenHandler = new Handler.TokenHandler(_configuration);
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
