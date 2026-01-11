using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Confirmed";
    }
}
