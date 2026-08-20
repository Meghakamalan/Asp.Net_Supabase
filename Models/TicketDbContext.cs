using Microsoft.EntityFrameworkCore;
// The runtime database interface —
// the class your app uses to talk to Supabase while it's running.
namespace TicketTracker.Models;
public class TicketDbContext : DbContext//inherits from EF Core's DbContext class -
    // This gives your class all the database functionality like querying, saving, and tracking changes
{
    public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) //bring the connection string from the Program.cs file and pass it to the base class constructor
    //:base(options)  passes those settings up to  the DBContext base class
    {
        Console.WriteLine("DB Connected");
    }
    public DbSet<Town> townList { get; set; }// represents the townList table in Supabase
    public DbSet<Ticket> ticketList { get; set; }// represents the ticketList table in Supabase
}