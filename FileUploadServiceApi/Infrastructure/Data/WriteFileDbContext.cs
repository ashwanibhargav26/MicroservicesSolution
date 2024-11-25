using FileUploadServiceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileUploadServiceApi.Infrastructure.Data
{
    public class WriteFileDbContext : DbContext
    {
        public WriteFileDbContext(DbContextOptions<WriteFileDbContext> options) : base(options)
        {
        }

        public DbSet<FileEntity> FileRecords { get; set; }

        // Other DbSets for write operations

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Add any custom logic for saving changes if necessary, like auditing or logging

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
