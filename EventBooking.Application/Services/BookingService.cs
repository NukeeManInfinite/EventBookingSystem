using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IEventRepository eventRepository,
            IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return null;

            return MapToDto(booking);
        }

        public async Task<List<BookingDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetUserBookingsAsync(userId);
            return bookings.Select(MapToDto).ToList();
        }

        public async Task<List<BookingDto>> GetEventBookingsAsync(int eventId)
        {
            var bookings = await _bookingRepository.GetEventBookingsAsync(eventId);
            return bookings.Select(MapToDto).ToList();
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        {
            var @event = await _eventRepository.GetByIdAsync(dto.EventId);
            if (@event == null)
                throw new Exception("Event not found");

            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found");

            if (@event.AvailableSeats <= 0)
                throw new Exception("No available seats for this event");

            var exists = await _bookingRepository.ExistsAsync(dto.EventId, dto.UserId);
            if (exists)
                throw new Exception("User has already booked this event");

            var booking = new Booking
            {
                EventId = dto.EventId,
                UserId = dto.UserId,
                Status = "Confirmed"
            };

            var created = await _bookingRepository.AddAsync(booking);

            @event.AvailableSeats--;
            await _eventRepository.UpdateAsync(@event);

            var result = await _bookingRepository.GetByIdAsync(created.Id);
            return MapToDto(result!);
        }

        public async Task CancelBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking != null)
            {
                var @event = await _eventRepository.GetByIdAsync(booking.EventId);
                if (@event != null)
                {
                    @event.AvailableSeats++;
                    await _eventRepository.UpdateAsync(@event);
                }

                await _bookingRepository.DeleteAsync(id);
            }
        }

        private static BookingDto MapToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                EventId = booking.EventId,
                EventTitle = booking.Event?.Title ?? string.Empty,
                UserId = booking.UserId,
                UserName = booking.User?.Name ?? string.Empty,
                CreatedAt = booking.CreatedAt,
                Status = booking.Status
            };
        }
    }
}
