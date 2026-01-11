using EventBooking.Application.DTOs;

namespace EventBooking.Application.Services
{
    public interface IBookingService
    {
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<List<BookingDto>> GetUserBookingsAsync(int userId);
        Task<List<BookingDto>> GetEventBookingsAsync(int eventId);
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
        Task CancelBookingAsync(int id);
    }
}
