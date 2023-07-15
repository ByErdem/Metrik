using Metrik.Entities.Dtos.UserDtos;

namespace Metrik.Services.Abstract;

public interface ITokenService
{
    public string CreateToken(UserLoginDto userLogin);

}