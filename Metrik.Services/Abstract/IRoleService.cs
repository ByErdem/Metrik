using Metrik.Entities.Dtos.RoleDtos;
using Metrik.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Services.Abstract
{
    public interface IRoleService
    {
        Task<IDataResult<RoleDto>> Get(int roleId);
        Task<IDataResult<RoleListDto>> GetAll();
        Task<IDataResult<RoleListDto>> GetAllByNonDeleted();
        Task<IDataResult<RoleListDto>> GetGetAllByNonDeletedAndActive();
        Task<IDataResult<RoleDto>> Add(RoleAddDto roleAddDto, int createdByUserId);
        Task<IDataResult<RoleDto>> Update(RoleUpdateDto roleUpdateDto, int modifiedByUserId);
        Task<IDataResult<RoleDto>> Delete(int roleId, int modifiedByUserId);
        Task<IResult> HardDelete(int roleId);

    }
}
