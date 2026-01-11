using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.AvailableSeats).IsRequired();
                entity.HasIndex(e => e.Date);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Status).HasMaxLength(50);

                entity.HasOne(b => b.Event)
                    .WithMany(e => e.Bookings)
                    .HasForeignKey(b => b.EventId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(b => new { b.EventId, b.UserId }).IsUnique();
            });
        }
    }
}
