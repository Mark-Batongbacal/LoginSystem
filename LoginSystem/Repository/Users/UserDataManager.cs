using LoginSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Repository.Users
{
    public class UserDataManager(LoginSystemContext context) : IUserDataManager
    {
        private readonly LoginSystemContext _context = context;

        //public UserDataManager(LoginSystemContext context)
        //{
        //    _context = context;
        //}

        public async Task AddUserAsync(User user, UserDetail details)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            details.UserId = user.UserId;
            await _context.UserDetails.AddAsync(details);
            await _context.SaveChangesAsync();
        }


        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetFullUserAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
