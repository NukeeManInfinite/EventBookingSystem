using EventBooking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}
