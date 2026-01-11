using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return null;

            return MapToDto(@event);
        }

        public async Task<List<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Select(MapToDto).ToList();
        }

        public async Task<List<EventDto>> GetUpcomingEventsAsync(int page, int pageSize)
        {
            var events = await _eventRepository.GetUpcomingAsync(page, pageSize);
            return events.Select(MapToDto).ToList();
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            var @event = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                Capacity = dto.Capacity,
                AvailableSeats = dto.Capacity
            };

            var created = await _eventRepository.AddAsync(@event);
            return MapToDto(created);
        }

        public async Task<EventDto> UpdateEventAsync(int id, UpdateEventDto dto)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null)
                throw new Exception("Event not found");

            @event.Title = dto.Title;
            @event.Description = dto.Description;
            @event.Date = dto.Date;
            @event.Location = dto.Location;
            @event.Capacity = dto.Capacity;

            await _eventRepository.UpdateAsync(@event);
            return MapToDto(@event);
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }

        private static EventDto MapToDto(Event @event)
        {
            return new EventDto
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                Date = @event.Date,
                Location = @event.Location,
                Capacity = @event.Capacity,
                AvailableSeats = @event.AvailableSeats
            };
        }
    }
}
