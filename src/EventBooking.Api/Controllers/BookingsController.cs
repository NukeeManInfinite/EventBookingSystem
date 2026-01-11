using EventBooking.Application.DTOs;
using EventBooking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<BookingDto>>> GetUserBookings(int userId)
        {
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<BookingDto>>> GetEventBookings(int eventId)
        {
            var bookings = await _bookingService.GetEventBookingsAsync(eventId);
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] CreateBookingDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateBookingAsync(dto);
                return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                await _bookingService.CancelBookingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
