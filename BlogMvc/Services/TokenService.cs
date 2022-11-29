using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogMvc.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlogMvc.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // importando manipulador de token

        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // importando nossa key econvertendo em bytes

        /* configurando o token com todas as config necessaria */
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name,"ryangabriel"), // User.Identity.Name
                new (ClaimTypes.Role,"user"), // User.Identity.Rolle
                new (ClaimTypes.Role,"admin"), // User.Identity.Rolle
                new (ClaimTypes.Role,"author") // User.Identity.Rolle
            } ),
            Expires = DateTime.UtcNow.AddHours(8), // tempo de expiração
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}