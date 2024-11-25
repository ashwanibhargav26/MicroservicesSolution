using AuthServiceApi.Application.Common.Utilities;
using AuthServiceApi.Domain.Entities;
using AuthServiceApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AuthServiceApi.Infrastructure.Data
{
    public class AuthServiceDbContextInitializer(AuthServiceDbContext context, ILoggerFactory logger)
    {
        private readonly AuthServiceDbContext _context = context;
        private readonly ILogger _logger = logger.CreateLogger<AuthServiceDbContextInitializer>();

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
                await SeedUser();
            }
            catch (Exception exception)
            {
                _logger.LogError("Migration error {exception}", exception);
                throw;
            }
        }

        public async Task SeedUser()
        {
            await _context.Users.AddRangeAsync(
            new List<User>{
                new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "P@ssw0rd".Hash(),
                    Role = Role.Admin
                },
                new User
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    Password = "P@ssw0rd".Hash(),
                    Role = Role.User
                },
                }
            );
            await _context.SaveChangesAsync();
        }
    }
}
