using Metrik.Entities.Dtos;

namespace Metrik.Services.Abstract;

public interface ITokenService
{
    string GetEMail();

    public string CreateToken(UserLoginDto userLogin);

}