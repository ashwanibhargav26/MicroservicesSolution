using FileUploadServiceApi.Application.Interfaces;

namespace FileUploadServiceApi.Infrastructure.Storage
{    
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _baseDirectory;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _baseDirectory = configuration["LocalStorage:BaseDirectory"] ?? "uploads";
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string directory = null)
        {
            var folderPath = directory == null ? _baseDirectory : Path.Combine(_baseDirectory, directory);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, Guid.NewGuid() + Path.GetExtension(file.FileName));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        public async Task<byte[]> GetFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        public Task DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
    }

}
