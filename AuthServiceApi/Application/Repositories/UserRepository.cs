using AuthServiceApi.Domain.Entities;
using AuthServiceApi.Infrastructure.Data;
using AuthServiceApi.Infrastructure.Interface;

namespace AuthServiceApi.Application.Repositories;

public class UserRepository(AuthServiceDbContext context) : GenericRepository<User>(context), IUserRepository
{
}
