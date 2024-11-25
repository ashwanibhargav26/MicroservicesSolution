using AuthServiceApi.Application.Common.Models;
using System.Data;

namespace AuthServiceApi.Domain.Entities
{
    public class User:BaseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.User;
    }
}
