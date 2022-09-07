using Microsoft.AspNetCore.Http;
using RekognitionService.Application.Interfaces;
using RekognitionService.Core.Communication;
using RekognitionService.Core.Interfaces;

namespace RekognitionService.Application.Services
{
    public class FileService : IFileService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileRepository _fileRepository;

        public FileService(ICategoryRepository categoryRepository, IFileRepository fileRepository)
        {
            _categoryRepository = categoryRepository;
            _fileRepository = fileRepository;
        }

        public async Task<GetFilesResponse> GetFilesByCategory(string category)
        {
            var fileNames = await _categoryRepository.GetFileNamesByCategoryAsync(category);

            var responseFromRepo = await _fileRepository.GetFilesByCategory(fileNames);

            return responseFromRepo;
        }

        public async Task<AddFileResponse> UploadFilesAsync(IList<IFormFile> filesToUpload)
        {
            return await _fileRepository.UploadFilesAsync(filesToUpload);
        }
    }
}