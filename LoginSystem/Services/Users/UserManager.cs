using LoginSystem.Models.Database;
using LoginSystem.Repository.Users;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace LoginSystem.Services.Users
{
    public class UserManager(IUserDataManager userDataManager) : IUserManager
    {
        private readonly IUserDataManager _userDataManager = userDataManager;
        public async Task CreateUserAsync(User user, UserDetail detail)
        {
            var existing = await _userDataManager.GetByEmailAsync(user.Email);
            if (existing != null)
            {
                throw new Exception("Email already exists");
            }
            user.IsActive = true;
            user.DateCreated = DateTime.Now;
            await _userDataManager.AddUserAsync(user, detail);
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _userDataManager.GetFullUserAsync(userId);
        }
    }
}
