using AutoMapper;
using Metrik.Data.Abstract;
using Metrik.Entities.Concrete;
using Metrik.Entities.Dtos.RoleDtos;
using Metrik.Services.Abstract;
using Metrik.Shared.Utilities.ComplexTypes;
using Metrik.Shared.Utilities.Results.Abstract;
using Metrik.Shared.Utilities.Results.Concrete;

namespace Metrik.Services.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<RoleDto>> Get(int roleId)
        {
            var role = await _unitOfWork.Roles.GetAsync(x => x.Id == roleId);
            if (role != null)
            {
                return new DataResult<RoleDto>(ResultStatus.Success, new RoleDto
                {
                    Role = role,
                    ResultStatus = ResultStatus.Success,
                    Message = "İşlem başarılı."
                });
            }
            return new DataResult<RoleDto>(ResultStatus.Error, "Böyle bir rol bulunamadı", null);
        }

        public async Task<IDataResult<RoleListDto>> GetAll()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            if (roles.Count > -1)
            {
                return new DataResult<RoleListDto>(ResultStatus.Success, new RoleListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Roles = roles
                });
            }
            return new DataResult<RoleListDto>(ResultStatus.Error, "Henüz tanımlanmış bir rol bulunamadı.", null);
        }

        public async Task<IDataResult<RoleListDto>> GetAllByNonDeleted()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync(x => !x.IsDeleted);
            if (roles.Count > -1)
            {
                return new DataResult<RoleListDto>(ResultStatus.Success, new RoleListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Roles = roles
                });
            }
            return new DataResult<RoleListDto>(ResultStatus.Error, "Henüz tanımlanmış bir rol bulunamadı.", null);
        }

        public async Task<IDataResult<RoleListDto>> GetGetAllByNonDeletedAndActive()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync(x => !x.IsDeleted && x.IsActive);
            if (roles.Count > -1)
            {
                return new DataResult<RoleListDto>(ResultStatus.Success, new RoleListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Roles = roles
                });
            }
            return new DataResult<RoleListDto>(ResultStatus.Error, "Henüz tanımlanmış bir rol bulunamadı.", null);
        }

        public async Task<IDataResult<RoleDto>> Add(RoleAddDto roleAddDto, int createdByUserId)
        {
            var role = _mapper.Map<Role>(roleAddDto);
            role.CreatedByUserId = createdByUserId;
            role.ModifiedByUserId = createdByUserId;
            var addedRole = await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.SaveAsync();
            return new DataResult<RoleDto>(ResultStatus.Success, $"{roleAddDto.Name} adlı rol başarıyla eklenmiştir.", new RoleDto
            {
                Role = addedRole,
                ResultStatus = ResultStatus.Success,
                Message = $"{roleAddDto.Name} adlı kategori başarıyla eklenmiştir."
            });
        }

        public async Task<IDataResult<RoleDto>> Update(RoleUpdateDto roleUpdateDto, int modifiedByUserId)
        {
            var oldRole = await _unitOfWork.Roles.GetAsync(c => c.Id == roleUpdateDto.Id);
            var role = _mapper.Map<RoleUpdateDto, Role>(roleUpdateDto, oldRole);
            role.ModifiedByUserId = modifiedByUserId;
            var updatedCategory = await _unitOfWork.Roles.UpdateAsync(role);
            await _unitOfWork.SaveAsync();
            return new DataResult<RoleDto>(ResultStatus.Success, $"{roleUpdateDto.Name} adlı rol başarıyla güncellenmiştir.", new RoleDto
            {
                Role = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{roleUpdateDto.Name} adlı rol başarıyla güncellenmiştir."
            });
        }

        public async Task<IDataResult<RoleDto>> Delete(int roleId, int modifiedByUserId)
        {
            var role = await _unitOfWork.Roles.GetAsync(c => c.Id == roleId);
            if (role != null)
            {
                role.IsDeleted = true;
                role.ModifiedByUserId = modifiedByUserId;
                role.ModifiedDate = DateTime.Now;
                var deletedrole = await _unitOfWork.Roles.UpdateAsync(role);
                await _unitOfWork.SaveAsync();
                return new DataResult<RoleDto>(ResultStatus.Success, $"{deletedrole.Name} adlı kategori başarıyla silinmiştir.", new RoleDto
                {
                    Role = deletedrole,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedrole.Name} adlı kategori başarıyla silinmiştir."
                });
            }
            return new DataResult<RoleDto>(ResultStatus.Error, $"Böyle bir kategori bulunamadı.", new RoleDto
            {
                Role = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir kategori bulunamadı."
            });
        }

        public async Task<IResult> HardDelete(int roleId)
        {
            var category = await _unitOfWork.Roles.GetAsync(c => c.Id == roleId);
            if (category != null)
            {
                await _unitOfWork.Roles.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }
    }
}
