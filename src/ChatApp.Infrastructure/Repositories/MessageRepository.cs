using ChatApp.Core.Entities;
using ChatApp.Core.Exceptions.Message;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Message message)
        {
            if (message == null) {
                throw new ArgumentNullException("Message is null");
            }

            await _context.Messages.AddAsync(message);
        }

        public async Task<List<Message>> ListByChatAsync(Guid chatId)
        {
            var messages = await _context.Messages
                .Where(c => c.ChatId == chatId)
                .ToListAsync();
            if (messages == null) {
                throw new MessageNotFoundException("Messages not found");
            }

            return messages;
        }

        public async Task<Message> GetByIdAsync(Guid messageId)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(c => c.Id == messageId);
            if (message == null) {
                throw new MessageNotFoundException("Message not found");
            }

            return message;
        }

        public Message Update(Message message)
        {
            if (message == null) {
                throw new ArgumentNullException("Message is null");
            }

            _context.Messages.Update(message);
            return message;
        }

        public void Delete(Message message)
        {
            if (message == null) {
                throw new ArgumentNullException("Message is null");
            }

            _context.Messages.Remove(message);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
