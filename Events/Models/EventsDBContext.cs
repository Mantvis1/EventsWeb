using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.Models
{
    public partial class EventsDBContext : DbContext
    {
        public DbSet<UserModels.User> User { get; set; }
        public DbSet<Support> Support { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvents> userEvents { get; set; }

        public EventsDBContext()
        {
        }

        public EventsDBContext(DbContextOptions<EventsDBContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=DESKTOP-N9PV4DB\\SQLEXPRESS;Database=Events;Trusted_Connection=True");

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new {Id = "1",Name="Admin",NormalizedName = "ADMIN"},
                new {Id = "2", Name="User", NormalizedName ="USER"}
             );
        }
    }
}
