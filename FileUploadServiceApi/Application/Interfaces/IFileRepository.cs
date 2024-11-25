using FileUploadServiceApi.Domain.Entities;

namespace FileUploadServiceApi.Application.Interfaces
{
    public interface IFileRepository
    {
        Task<FileEntity> GetFileByIdAsync(Guid id);
        Task AddAsync(FileEntity file);
        Task DeleteAsync(Guid id);
    }
}
