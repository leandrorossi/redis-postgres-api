using Microsoft.IdentityModel.Tokens;
using RedisPostgres_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RedisPostgres_Api.Auth
{
    public static class TokenGenerator
    {
        public static Token Generate(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token
            {
                token = tokenHandler.WriteToken(token),
                Message = "Token created successfully",
                Expiration = DateTime.UtcNow.AddMinutes(20),
            };
        }
    }
}
