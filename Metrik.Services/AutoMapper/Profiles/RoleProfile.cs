using AutoMapper;
using Metrik.Entities.Concrete;
using Metrik.Entities.Dtos.RoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Services.AutoMapper.Profiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleAddDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
        }
    }
}
