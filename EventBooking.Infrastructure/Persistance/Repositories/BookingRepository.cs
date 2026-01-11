using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistance.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> GetBookingCountAsync(int eventId)
        {
            return await _context.Bookings
                .CountAsync(b => b.EventId == eventId);
        }

        public async Task<bool> ExistsAsync(int eventId, int userId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.EventId == eventId && b.UserId == userId);
        }

        public async Task<List<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetEventBookingsAsync(int eventId)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Where(b => b.EventId == eventId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            booking.CreatedAt = DateTime.UtcNow;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
