using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]!);

        var token = new JwtSecurityToken(
            claims: new[]
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim("UserId",user.Id.ToString())
            },
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}