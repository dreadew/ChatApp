using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatByIdAsync(Guid chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<IEnumerable<Message>> ListMessagesByChatAsync(Guid chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task SendMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
