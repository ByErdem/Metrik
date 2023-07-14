using Metrik.Entities.Dtos;
using Metrik.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metrik.Services.Concrete;

public class TokenService : ITokenService
{

    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TokenService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
    {
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreateToken(UserLoginDto userLogin)
    {
        byte[] key = Encoding.ASCII.GetBytes(_config["Auth:SecretKey"]);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Email, userLogin.Email),
                    new Claim(ClaimTypes.NameIdentifier,userLogin.Email)

            }),

            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _config["Auth:Issuer"],
            Audience = _config["Auth:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetEMail()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

}