using Microsoft.AspNetCore.Http;
using RekognitionService.Core.Communication;

namespace RekognitionService.Core.Interfaces
{
    public interface IFileRepository
    {
        Task<AddFileResponse> UploadFilesAsync(IList<IFormFile> filesToUpload);

        Task<GetFilesResponse> GetFilesByCategory(IEnumerable<string> fileNames);
    }
}