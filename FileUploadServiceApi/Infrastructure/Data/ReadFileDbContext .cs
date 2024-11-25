using FileUploadServiceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileUploadServiceApi.Infrastructure.Data
{
    public class ReadFileDbContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public ReadFileDbContext(DbContextOptions<ReadFileDbContext> options) : base(options) { }
    }
}
