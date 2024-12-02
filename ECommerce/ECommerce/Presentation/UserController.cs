//using ECommerce.DataAccess;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Security.Claims;
//using System;
//using ECommerce.Business;
//using ecommerce.dataaccess; // Assuming User and LoginModel are in this namespace

//namespace ECommerce.Presentation
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ECommerceContext _context;

//        public UserController(IConfiguration configuration, ECommerceContext context)
//        {
//            _configuration = configuration;
//            _context = context;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel loginModel)
//        {
//            var user = AuthenticateUser(loginModel);
//            if (user != null)
//            {
//                var token = GenerateJWTToken(user);
//                return Ok(new { token });
//            }

//            return Unauthorized();
//        }


//        private User AuthenticateUser(LoginModel loginModel)
//        {
//            using (var context = new ECommerceContext())
//            {
//                //todo hash yapılması lazım password için
//                var user = context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
//                if (user != null)
//                {
//                    return new User
//                    {
//                        Email = user.Email,

//                    };
//                }
//                return null;
//            }
//        }

//        private string GenerateJWTToken(User user)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var token = new Helper().GenerateJWTToken(user, _configuration);
//            return token;
//        }
//    }
//}
