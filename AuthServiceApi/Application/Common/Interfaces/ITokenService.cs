using AuthServiceApi.Domain.Entities;
using System.Security.Claims;

namespace AuthServiceApi.Application.Common.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
    public ClaimsPrincipal ValidateToken(string token);
}
