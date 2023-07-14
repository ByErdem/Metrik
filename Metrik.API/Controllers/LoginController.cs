using Metrik.Entities.Dtos;
using Metrik.Services.Abstract;
using Metrik.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Metrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserService _userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        private async Task<bool> AuthenticateUser(UserLoginDto user)
        {
            var _user = await _userService.Get(user.Email);
            if (_user.ResultStatus==ResultStatus.Success)
            {
                return true;
            }
            return false;
        }

        private string GenerateToken(UserLoginDto users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto user)
        {

            IActionResult response = Unauthorized();
            if (await AuthenticateUser(user))
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });
            }

            return response;
        }


        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(UserLoginDto user)
        {
            IActionResult response = Unauthorized();
            var _user = await _userService.Get(user.Email);
            if(_user != null)
            {
                response = Ok(_user.Data.User);
            }
            return response;
        }



    }
}
