using Microsoft.EntityFrameworkCore;

namespace Project_sem2.Models
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base (options){

        }

        // creating a table called Clientlist into the database
        public DbSet<Client> clientList { get; set; } 
        public DbSet<Massage> massageList { get; set; } 
    }
}
