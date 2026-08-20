using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketTracker.Models
{
    public class TicketContextFactory
        : IDesignTimeDbContextFactory<TicketDbContext>
    {
        public TicketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<TicketDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=aws-1-us-west-2.pooler.supabase.com;" +
                "Database=postgres;" +
                "Username=postgres.nuasecxjbkgylczutmjl;" +
                "Password=PP@2tickettracker;" +
                "SSL Mode=Require;" +
                "Trust Server Certificate=true"
            );

            return new TicketDbContext(
                optionsBuilder.Options
            );
        }
    }
}