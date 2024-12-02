using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using UserApplication.Model;
using UserApplication.Utils;
using UserApplication.Validators;
using UserApplication.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApplication.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(string nickname)
        {
            using (var context = new WebApplication1Context())
            {
                var users = context.Users.Where(data => data.Nickname == nickname).FirstOrDefault();

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    return users;
                }
            }
        }


        [HttpPost]
        public ActionResult<PostUserViewModel> Post(User user)
        {
            using (var context = new WebApplication1Context())
            {
                UserValidator validate = new UserValidator();

                if (validate.Validate(user).IsValid)
                {
                    // Check if a user with the same nickname already exists
                    var existingUser = context.Users
                                              .FirstOrDefault(u => u.Nickname == user.Nickname);

                    if (existingUser != null)
                    {
                        return Conflict("A user with this nickname already exists.");
                    }

                    context.Add(user);
                    context.SaveChanges();

                    //olmasada olur öylesine gösterdik.
                    PostUserViewModel viewModel = new PostUserViewModel()
                    {
                        Nickname = user.Nickname,
                        Email = user.Email,
                        Password = user.Password,
                        Age = user.Age
                    };
                    return Ok(viewModel);
                }
                else
                {
                    return BadRequest();
                }
            }
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = AuthenticateUser(loginModel);
            if (user != null)
            {
                var token = GenerateJWTToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }


        private User AuthenticateUser(LoginModel loginModel)
        {
            using (var context = new WebApplication1Context())
            {
                //todo hash yapılması lazım password için
                var user = context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
                if (user!= null)
                {
                    return new User
                    {
                        Email = user.Email,
                        Nickname = user.Nickname
                    };
                }
                return null;
            }
        }

        private string GenerateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = new Helper().GenerateJWTToken(user, _configuration);
            return token;
        }


        [HttpDelete("{id}")]
        public ActionResult<User> Delete(string nickname)
        {
            using (var context = new WebApplication1Context())
            {
                var data = context.Users.Where(data => data.Nickname == nickname).FirstOrDefault();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Users.Remove(data);
                    context.SaveChanges();
                    return data;

                }
            }
        }
    }
}
