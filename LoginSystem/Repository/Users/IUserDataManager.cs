using LoginSystem.Models.Database;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace LoginSystem.Repository.Users
{
    public interface IUserDataManager
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetFullUserAsync(int userId);
        Task AddUserAsync(User user, UserDetail details);
    }
}
