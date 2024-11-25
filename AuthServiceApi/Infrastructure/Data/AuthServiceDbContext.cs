using AuthServiceApi.Domain.Entities;
using AuthServiceApi.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace AuthServiceApi.Infrastructure.Data
{
    public class AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options) : DbContext(options)
    {       
        public DbSet<User> Users { get; set; }       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
