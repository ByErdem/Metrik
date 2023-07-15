using AutoMapper;
using Metrik.Data.Abstract;
using Metrik.Entities.Concrete;
using Metrik.Entities.Dtos.UserDtos;
using Metrik.Services.Abstract;
using Metrik.Shared.Utilities.ComplexTypes;
using Metrik.Shared.Utilities.Results.Abstract;
using Metrik.Shared.Utilities.Results.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metrik.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryption;
        private const string TokenKey = "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAKlYLOgN3zCVxVh7jzA+nrCH";

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper, IEncryptionService encryption)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encryption = encryption;
        }

        public async Task<IDataResult<UserLoginDto>> Authenticate(UserLoginDto userDto)
        {
            var user = await _unitOfWork.Users.GetAsync(x => x.Email == userDto.Email);
            if (user != null)
            {
                string dbpassword = user.PasswordHash;
                string enteredpassword = _encryption.AESEncrypt(userDto.Password);

                if (dbpassword != enteredpassword)
                {
                    return new DataResult<UserLoginDto>(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.", new UserLoginDto
                    {
                        ResultStatus = ResultStatus.Error,
                        Message = "Böyle bir kullanıcı bulunamadı."
                    });
                }
                else
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, userDto.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)); // Replace with your secret key
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds
                    );

                    //user.Token = new JwtSecurityTokenHandler().WriteToken(token);

                    return new DataResult<UserLoginDto>(ResultStatus.Success, "Kullanıcı doğrulandı", new UserLoginDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ResultStatus = ResultStatus.Success,
                        Message = "Kullanıcı doğrulandı"
                    });
                }
            }
            else
            {
                return new DataResult<UserLoginDto>(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.", new UserLoginDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = "Böyle bir kullanıcı bulunamadı."
                });
            }
        }

        public async Task<IDataResult<UserDto>> Get(int userId)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userId);
            if (user != null)
            {
                return new DataResult<UserDto>(ResultStatus.Success, new UserDto
                {
                    User = user,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<UserDto>(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.", new UserDto
            {
                User = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kullanıcı bulunamadı."
            });
        }

        public async Task<IDataResult<UserDto>> Get(string email)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Email == email);
            if (user != null)
            {
                return new DataResult<UserDto>(ResultStatus.Success, new UserDto
                {
                    User = user,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<UserDto>(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.", new UserDto
            {
                User = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kullanıcı bulunamadı."
            });
        }

        public async Task<IDataResult<UserListDto>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if (users.Count > -1)
            {
                return new DataResult<UserListDto>(ResultStatus.Success, new UserListDto
                {
                    Users = users,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<UserListDto>(ResultStatus.Error, "Tanımlı herhangi bir kullanıcı bulunamadı.", new UserListDto
            {
                Users = null,
                ResultStatus = ResultStatus.Error,
                Message = "Tanımlı herhangi bir kullanıcı bulunamadı.."
            });
        }

        public async Task<IDataResult<UserListDto>> GetAllByNonDeleted()
        {
            var users = await _unitOfWork.Users.GetAllAsync(c => !c.IsDeleted);
            if (users.Count > -1)
            {
                return new DataResult<UserListDto>(ResultStatus.Success, new UserListDto
                {
                    Users = users,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<UserListDto>(ResultStatus.Error, "Tanımlı herhangi bir kullanıcı bulunamadı.", new UserListDto
            {
                Users = null,
                ResultStatus = ResultStatus.Error,
                Message = "Tanımlı herhangi bir kullanıcı bulunamadı."
            });
        }

        public async Task<IDataResult<UserListDto>> GetAllByNonDeletedAndActive()
        {
            var users = await _unitOfWork.Users.GetAllAsync(c => !c.IsDeleted);
            if (users.Count > -1)
            {
                return new DataResult<UserListDto>(ResultStatus.Success, new UserListDto
                {
                    Users = users,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<UserListDto>(ResultStatus.Error, "Hiç bir kategori bulunamadı.", new UserListDto
            {
                Users = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<UserDto>> Add(UserAddDto userAddDto, int createdByUserId)
        {
            var user = _mapper.Map<User>(userAddDto);
            user.CreatedByUserId = createdByUserId;
            user.ModifiedByUserId = createdByUserId;
            var addedUser = await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return new DataResult<UserDto>(ResultStatus.Success, $"{userAddDto.FirstName + " " + userAddDto.LastName} adlı kullanıcı başarıyla eklenmiştir.", new UserDto
            {
                User = addedUser,
                ResultStatus = ResultStatus.Success,
                Message = $"{userAddDto.FirstName + " " + userAddDto.LastName} isimli kullanıcı başarıyla eklenmiştir."
            });
        }

        public async Task<IDataResult<UserDto>> Update(UserUpdateDto userUpdateDto, int modifiedByUserId)
        {
            var oldUser = await _unitOfWork.Users.GetAsync(c => c.Id == userUpdateDto.Id);
            var user = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
            user.ModifiedByUserId = modifiedByUserId;
            var updatedUser = await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return new DataResult<UserDto>(ResultStatus.Success, $"{userUpdateDto.FirstName + " " + userUpdateDto.LastName} isimli kullanıcı başarıyla güncellenmiştir.", new UserDto
            {
                User = updatedUser,
                ResultStatus = ResultStatus.Success,
                Message = $"{userUpdateDto.FirstName + " " + userUpdateDto.LastName} isimli kullanıcı başarıyla güncellenmiştir."
            });
        }

        public async Task<IDataResult<UserDto>> Delete(int userId, int modifiedByUserId)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userId);
            if (user != null)
            {
                user.IsDeleted = true;
                user.ModifiedByUserId = modifiedByUserId;
                user.ModifiedDate = DateTime.Now;
                var deletedUser = await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
                return new DataResult<UserDto>(ResultStatus.Success, $"{deletedUser.FirstName + " " + deletedUser.LastName} isimli kullanıcı başarıyla silinmiştir.", new UserDto
                {
                    User = deletedUser,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedUser.FirstName + " " + deletedUser.LastName} isimli kullanıcı başarıyla silinmiştir."
                });
            }
            return new DataResult<UserDto>(ResultStatus.Error, $"Böyle bir kullanıcı bulunamadı.", new UserDto
            {
                User = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir kullanıcı bulunamadı."
            });
        }

        public async Task<IResult> HardDelete(int userId)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userId);
            if (user != null)
            {
                await _unitOfWork.Users.DeleteAsync(user);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{user.FirstName + " " + user.LastName} isimli kullanıcı başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.");
        }
    }
}
