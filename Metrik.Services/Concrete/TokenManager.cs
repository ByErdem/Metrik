using Metrik.Entities.Dtos.UserDtos;
using Metrik.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metrik.Services.Concrete;

public class TokenManager : ITokenService
{

    private readonly IConfiguration _config;

    public TokenManager(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(UserLoginDto userLogin)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new Claim[]
            {
                    new Claim(ClaimTypes.Email, userLogin.Email)
            },
            expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:Time"])),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}