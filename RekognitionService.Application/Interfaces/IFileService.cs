using Microsoft.AspNetCore.Http;
using RekognitionService.Core.Communication;

namespace RekognitionService.Application.Interfaces
{
    public interface IFileService
    {
        Task<GetFilesResponse> GetFilesByCategory(string category);

        Task<AddFileResponse> UploadFilesAsync(IList<IFormFile> filesToUpload);
    }
}
