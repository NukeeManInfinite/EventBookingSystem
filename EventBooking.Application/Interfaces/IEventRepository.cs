using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id);
        Task<List<Event>> GetUpcomingAsync(int page, int pageSize);
        Task<List<Event>> GetAllAsync();
        Task<Event> AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(int id);
    }
}
