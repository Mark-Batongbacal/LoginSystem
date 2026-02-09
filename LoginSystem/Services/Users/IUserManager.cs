using LoginSystem.Models.Database;

namespace LoginSystem.Services.Users
{
    public interface IUserManager
    {

        Task CreateUserAsync(User user, UserDetail detail);
        Task<User?> GetUserAsync(int userId);

    }
}
