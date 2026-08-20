using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
//The design-time database interface — 
//only used when you run migration commands, not when the app is actually running.
namespace TicketTracker.Models
{
    public class TicketContextFactory : IDesignTimeDbContextFactory<TicketDbContext>
    //IDesignTimeDbContextFactory-this interface tells EF Core tools -
    //  "use this class to create  a DbContext when running migrations
    {
        public TicketDbContext CreateDbContext(string[] args)//EF Core calls this method automatically when you run dotnet ef migrations add or dotnet ef database update
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketDbContext>();
            optionsBuilder.UseNpgsql(
                "Host=aws-1-us-west-2.pooler.supabase.com;Database=postgres;Username=postgres.nuasecxjbkgylczutmjl;Password=PP@2tickettracker;SSL Mode=Require;Trust Server Certificate=true"
            );//hardcodes the connection string directly so the migration tool can connect to Supabase
            return new TicketDbContext(optionsBuilder.Options);
            //creates and returns the DbContext with those settings
        }
    }
}