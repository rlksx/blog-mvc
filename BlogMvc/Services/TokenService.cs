using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogMvc.Extensions;
using BlogMvc.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlogMvc.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // importando manipulador de token
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // importando nossa key econvertendo em bytes
        var claims = user.getClaim();
        
        /* configurando o token com todas as config necessaria */
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8), // tempo de expiração
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}