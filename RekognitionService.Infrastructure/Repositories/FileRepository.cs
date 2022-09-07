using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using RekognitionService.Core.Communication;
using RekognitionService.Core.Interfaces;

namespace RekognitionService.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IAmazonS3 _s3Client;

        public FileRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<GetFilesResponse> GetFilesByCategory(IEnumerable<string> fileNames)
        {
            var preSignedUrls = new List<string>();
            string bucketName = "rekognition-service-demo";

            foreach (var fileName in fileNames)
            {
                var expiryUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                var url = _s3Client.GetPreSignedURL(expiryUrlRequest);

                preSignedUrls.Add(url);
            }

            return new GetFilesResponse
            {
                PreSignedUrls = preSignedUrls,
            };
        }

        public async Task<AddFileResponse> UploadFilesAsync(IList<IFormFile> filesToUpload)
        {
            var preSignedUrls = new List<string>();

            // TODO: It should not be static here. Move it to conf
            string bucketName = "rekognition-service-demo";

            foreach (var file in filesToUpload)
            {
                var identifier = Guid.NewGuid();

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = file.OpenReadStream(),
                    Key = string.Concat(identifier, Path.GetExtension(file.FileName)),
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                using (var fileTransferUtility = new TransferUtility(_s3Client))
                {
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                var expiryUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = string.Concat(identifier, Path.GetExtension(file.FileName)),
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                var url = _s3Client.GetPreSignedURL(expiryUrlRequest);

                preSignedUrls.Add(url);
            }

            return new AddFileResponse
            {
                PreSignedUrls = preSignedUrls
            };
        }
    }
}
