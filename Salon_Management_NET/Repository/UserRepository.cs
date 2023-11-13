using Microsoft.EntityFrameworkCore;
using Salon_Management_NET.Data;
using Salon_Management_NET.Model;

namespace Salon_Management_NET.Repository
{
    public class UserRepository
    {
        private readonly AppAPIDbContext _context;

        public UserRepository(AppAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Guid id, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Password = updatedUser.Password; // Note: Consider hashing the password
                existingUser.Email = updatedUser.Email;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
