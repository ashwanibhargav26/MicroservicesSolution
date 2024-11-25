using FileUploadServiceApi.Application.Interfaces;
using FileUploadServiceApi.Domain.Entities;
using FileUploadServiceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FileUploadServiceApi.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileDbContext _context;

        // Constructor injects the FileDbContext
        public FileRepository(FileDbContext context)
        {
            _context = context;
        }

        // Retrieve a file by its ID from the database
        public async Task<FileEntity> GetFileByIdAsync(Guid id)
        {
            // Query the Files table for the file entity with the provided ID
            return await _context.Files.FindAsync(id);
        }

        // Add a new file to the database
        public async Task AddAsync(FileEntity file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File entity cannot be null.");
            }

            // Add the file to the Files DbSet and save changes
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }

        // Delete a file by its ID from the database
        public async Task DeleteAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file != null)
            {
                _context.Files.Remove(file);
                await _context.SaveChangesAsync();
            }
        }
    }
}
