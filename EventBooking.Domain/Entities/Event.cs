
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date {  get; set; }
        public int Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; }

    }
}
