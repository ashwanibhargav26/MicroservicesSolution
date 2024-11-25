using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileUploadServiceApi.Application.Interfaces;

namespace FileUploadServiceApi.Infrastructure.Storage
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            var blobServiceClient = new BlobServiceClient(configuration["BlobStorage:ConnectionString"]);
            _containerClient = blobServiceClient.GetBlobContainerClient(configuration["BlobStorage:ContainerName"]);

            _containerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        public async Task<string> SaveFileAsync(IFormFile file, string directory = null)
        {
            var blobName = (directory == null ? "" : directory + "/") + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = _containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blobClient.Uri.ToString();
        }

        public async Task<byte[]> GetFileAsync(string filePath)
        {
            var blobClient = new BlobClient(new Uri(filePath));

            if (await blobClient.ExistsAsync())
            {
                var download = await blobClient.DownloadContentAsync();
                return download.Value.Content.ToArray();
            }
            else
            {
                throw new FileNotFoundException("File not found in Azure Blob Storage", filePath);
            }
        }

        public async Task DeleteFileAsync(string filePath)
        {
            var blobClient = new BlobClient(new Uri(filePath));
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
