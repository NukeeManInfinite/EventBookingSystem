using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(int id);
        Task<int> GetBookingCountAsync(int eventId);
        Task<bool> ExistsAsync(int eventId, int userId);
        Task<List<Booking>> GetUserBookingsAsync(int userId);
        Task<List<Booking>> GetEventBookingsAsync(int eventId);
        Task<Booking> AddAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}
