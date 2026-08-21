using Microsoft.EntityFrameworkCore;

namespace TicketTracker.Models
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
            Console.WriteLine("DB Connected");
        }

        public DbSet<Town> Towns { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        // Place it right here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
        }
    }
}