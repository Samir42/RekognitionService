using Microsoft.AspNetCore.Mvc;
using RekognitionService.Application.Interfaces;
using RekognitionService.Core.Communication;

namespace RekognitionService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IFileService _fileService;

        public PhotosController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult<AddFileResponse>> AddPhoto(IList<IFormFile> filesToUpload)
        {
            if (filesToUpload is null)
            {
                return BadRequest("Request does not contain any files to be uploaded");
            }

            var uploadResponseFromRepository = await _fileService.UploadFilesAsync(filesToUpload);

            if (uploadResponseFromRepository is null)
            {
                return BadRequest();
            }

            return Ok(uploadResponseFromRepository);
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<GetFilesResponse>> GetPhotos(string category)
        {
            if (category is null)
            {
                return BadRequest("Request does not contain any category to be returned");
            }

            var filesFromS3 = await _fileService.GetFilesByCategory(category);

            return Ok(filesFromS3);
        }
    }
}
