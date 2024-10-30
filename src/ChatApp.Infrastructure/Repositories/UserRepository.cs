using ChatApp.Core.Entities;
using ChatApp.Core.Exceptions.User;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            if (user == null) {
                throw new ArgumentNullException("User is null");
            }

            var existingUserByUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            
            if (existingUserByUsername == null) {
                throw new UserAlreadyExistedException("User with this username already existed");
            }

            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            
            if (existingUserByUsername == null) {
                throw new UserAlreadyExistedException("User with this email already existed");
            }

            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(c => c.Chats)
                .FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null) {
                throw new UserNotFoundException("User not found");
            }

            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(c => c.Username == username);
            if (user == null) {
                throw new UserNotFoundException("User not found");
            }

            return user;
        }

        public async Task<List<User>> GetByIdsAsync(List<Guid> userIds)
        {
            var users = await _context.Users
                .Where(c => userIds.Contains(c.Id))
                .ToListAsync();
            if (users == null) {
                throw new UserNotFoundException("Users not found");
            }

            return users;
        }

        public async Task<List<User>> ListAsync(int take, int skip)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Take(take)
                .Skip(skip)
                .ToListAsync();
            return users;
        }

        public User Update(User user)
        {
            if (user == null) {
                throw new ArgumentNullException("User is null");
            }

            _context.Users.Update(user);
            return user;
        }

        public void Delete(User user)
        {
            if (user == null) {
                throw new ArgumentNullException("User is null");
            }

            _context.Users.Remove(user);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
