using EventBooking.Application.DTOs;

namespace EventBooking.Application.Services
{
    public interface IEventService
    {
        Task<EventDto?> GetEventByIdAsync(int id);
        Task<List<EventDto>> GetAllEventsAsync();
        Task<List<EventDto>> GetUpcomingEventsAsync(int page, int pageSize);
        Task<EventDto> CreateEventAsync(CreateEventDto dto);
        Task<EventDto> UpdateEventAsync(int id, UpdateEventDto dto);
        Task DeleteEventAsync(int id);
    }
}
