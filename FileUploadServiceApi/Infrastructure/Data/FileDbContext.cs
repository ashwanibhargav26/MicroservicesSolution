using Microsoft.EntityFrameworkCore;

namespace FileUploadServiceApi.Infrastructure.Data
{
    public class FileDbContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().ToTable("Files");
            base.OnModelCreating(modelBuilder);
        }
    }
}
