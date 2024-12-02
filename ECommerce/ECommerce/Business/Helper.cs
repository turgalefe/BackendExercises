//using ecommerce.dataaccess;
//using ECommerce.DataAccess;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace ECommerce.Business
//{
//    public class Helper
//    {
//        public string GenerateJWTToken(User user, IConfiguration configuration)
//        {
//            var claims = new List<Claim> {
//                    new Claim(ClaimTypes.NameIdentifier, user.Email),
//            };
//            var jwtToken = new JwtSecurityToken(
//                claims: claims,
//                notBefore: DateTime.UtcNow,
//                expires: DateTime.UtcNow.AddDays(30),
//                signingCredentials: new SigningCredentials(
//                    new SymmetricSecurityKey(
//                       Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
//                        ),
//                    SecurityAlgorithms.HmacSha256Signature)
//                );
//            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//        }
//    }

//}
