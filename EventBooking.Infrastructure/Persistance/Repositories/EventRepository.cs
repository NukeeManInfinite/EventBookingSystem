using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistance.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> GetUpcomingAsync(int page, int pageSize)
        {
            return await _context.Events
                .Where(e => e.Date >= DateTime.UtcNow)
                .OrderBy(e => e.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        public async Task<Event> AddAsync(Event @event)
        {
            @event.CreatedAt = DateTime.UtcNow;
            @event.AvailableSeats = @event.Capacity;
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task UpdateAsync(Event @event)
        {
            @event.UpdatedAt = DateTime.UtcNow;
            _context.Events.Update(@event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }
        }
    }
}
