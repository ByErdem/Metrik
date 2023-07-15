using AutoMapper;
using Metrik.Entities.Concrete;
using Metrik.Entities.Dtos.UserDtos;

namespace Metrik.Services.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
