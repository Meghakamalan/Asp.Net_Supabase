using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Project_sem2.Models
{
    
    public class ContextFactory :
    IDesignTimeDbContextFactory<AppointmentDbContext>
    {
        public AppointmentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppointmentDbContext>
            ();
            optionsBuilder.UseNpgsql(
            "Host=aws-1-us-east-1.pooler.supabase.com;Database=postgres;Username=postgres.hjcqkvpvjkepdougdgiz;Password= Meghakuttu@123;SSL Mode=Require;Trust Server Certificate=true");
            return new AppointmentDbContext(optionsBuilder.Options);
        }
    }
}



  