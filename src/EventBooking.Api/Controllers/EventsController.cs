using EventBooking.Application.DTOs;
using EventBooking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<List<EventDto>>> GetUpcomingEvents(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var events = await _eventService.GetUpcomingEventsAsync(page, pageSize);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var @event = await _eventService.GetEventByIdAsync(id);
            if (@event == null)
                return NotFound(new { message = "Event not found" });

            return Ok(@event);
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEventDto dto)
        {
            try
            {
                var @event = await _eventService.CreateEventAsync(dto);
                return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventDto>> UpdateEvent(int id, [FromBody] UpdateEventDto dto)
        {
            try
            {
                var @event = await _eventService.UpdateEventAsync(id, dto);
                return Ok(@event);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
