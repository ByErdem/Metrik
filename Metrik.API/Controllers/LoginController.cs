using Metrik.Entities.Dtos;
using Metrik.Services.Abstract;
using Metrik.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Metrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;


        public LoginController(IConfiguration config, IUserService userService, ITokenService tokenService)
        {
            _config = config;
            _userService = userService;
            _tokenService = tokenService;
        }

        private async Task<bool>? AuthenticateUser(UserLoginDto user)
        {
            var _user = await _userService.Get(user.Email);
            if (_user.ResultStatus==ResultStatus.Success)
            {
                return true;
            }
            return false;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto user)
        {

            IActionResult response = Unauthorized();
            if (await AuthenticateUser(user))
            {
                var token = _tokenService.CreateToken(user);
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
