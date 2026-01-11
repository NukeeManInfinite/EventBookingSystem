namespace EventBooking.Application.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class CreateBookingDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}
