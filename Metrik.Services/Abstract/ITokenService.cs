using Metrik.Entities.Dtos;

namespace Metrik.Services.Abstract;

public interface ITokenService
{
    public string CreateToken(UserLoginDto userLogin);

}