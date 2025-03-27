using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Message>> GetAllMessagesAsync() =>
            await _context.Messages.ToListAsync();
        public async Task<Message> GetMessageByIdAsync(int id) =>
            await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        public async Task CreateMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMessageAsync(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMessageAsync(int id)
        {
            var message = await GetMessageByIdAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
