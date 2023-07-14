using Metrik.Entities.Dtos;
using Metrik.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Services.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserLoginDto>> Authenticate(UserLoginDto userDto);
        Task<IDataResult<UserDto>> Get(int userId);
        Task<IDataResult<UserDto>> Get(string email); 
        Task<IDataResult<UserListDto>> GetAll();
        Task<IDataResult<UserListDto>> GetAllByNonDeleted();
        Task<IDataResult<UserListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<UserDto>> Add(UserAddDto userAddDto, int createdByUserId);
        Task<IDataResult<UserDto>> Update(UserUpdateDto userUpdateDto, int modifiedByUserId);
        Task<IDataResult<UserDto>> Delete(int userId, int modifiedByUserId);
        Task<IResult> HardDelete(int userId);
    }
}
