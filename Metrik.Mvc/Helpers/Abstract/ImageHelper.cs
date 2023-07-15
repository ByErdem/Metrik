using Metrik.Entities.Dtos.ImageDtos;
using Metrik.Shared.Utilities.Results.Abstract;

namespace Metrik.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages");
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
