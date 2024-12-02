
using Microsoft.IdentityModel.Tokens;
using Project01.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Project01.Handler
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //Token üretecek metot.
        public Project01.Model.Token CreateAccessToken(User user)
        {
            Model.Token tokenInstance = new Model.Token();

            //Security  Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            tokenInstance.Expiration = DateTime.Now.AddDays(14);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenInstance.Expiration,//Token süresini 14 gün olarak belirliyorum
                notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token üretiyoruz.
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
