using ChatApp.Core.Entities;
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

        public async Task<Chat> GetByIdAsync(Guid chatId)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat == null) {
                throw new ArgumentNullException("Chat not found");
            }

            return chat;
        }

        public async Task<List<Chat>> ListChatsByUserAsync(Guid userId)
        {
            var chats = await _context.Chats
                .Where(c => c.Users!.Any(u => u.Id == userId))
                .AsNoTracking()
                .ToListAsync();
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
