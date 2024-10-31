using ChatApp.Core.Entities;
using ChatApp.Core.Exceptions.Chat;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChatService.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Chat chat)
        {
            if (chat == null) {
                throw new ArgumentNullException("Chat is null");
            }

            await _context.Chats.AddAsync(chat);
        }

        public async Task AppendUsersAsync(Chat chat, List<User> users)
        {
            if (chat == null || users == null) {
                throw new ArgumentNullException("Chat is null");
            }
            
            foreach (var user in users)
            {
                if (user.Chats == null)
                {
                    user.Chats = new List<Chat>();
                }
                    
                user.Chats.Add(chat);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUsersAsync(Chat chat, List<User> users)
        {
            if (chat == null || users == null) {
                throw new ArgumentNullException("Chat is null");
            }
            
            foreach (var user in users)
            {
                if (user.Chats == null)
                {
                    break;
                }
                    
                user.Chats.Remove(chat);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> GetByIdAsync(Guid chatId)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat == null) {
                throw new ChatNotFoundException("Chat not found");
            }

            return chat;
        }

        public async Task<Chat?> FindPrivateChatAsync(List<Guid> usersIds)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .Include(c => c.Users)
                .Where(c => c.IsGroupChat == false && c.Users.Count == 2)
                .FirstOrDefaultAsync(c => c.Users.All(u => usersIds.Contains(u.Id)));
            return chat;
        }

        public async Task<List<Chat>> ListChatsByUserAsync(Guid userId)
        {
            var chats = await _context.Chats
                .Where(c => c.Users!.Any(u => u.Id == userId))
                .AsNoTracking()
                .ToListAsync();
            if (chats == null)
            {
                throw new ChatNotFoundException("Chats not found");
            }
            
            return chats;
        }

        public Chat Update(Chat chat)
        {
            if (chat == null) {
                throw new ArgumentNullException("Chat is null");
            }

            _context.Chats.Update(chat);
            return chat;
        }

        public void Delete(Chat chat)
        {
            if (chat == null) {
                throw new ArgumentNullException("Chat is null");
            }

            _context.Chats.Remove(chat);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
