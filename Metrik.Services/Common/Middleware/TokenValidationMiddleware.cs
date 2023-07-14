using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metrik.Services.Common.Middleware;

public class TokenValidationMiddleware
{

    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Endpoint endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
        {
            await _next(context);
            return;
        }

        try
        {
            string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_config["Auth:SecretKey"]);
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidIssuer = _config["Auth:Issuer"],
                ValidAudience = _config["Auth:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Auth:SecretKey"]))
            };

            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            string companyId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string companyName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            context.Items["User"] = new { companyId, companyName };
        }
        catch (SecurityTokenException ex)
        {
            context.Response.StatusCode = 403;
            return;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 403;
            return;
        }
        await _next.Invoke(context);

    }
}