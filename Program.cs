using Microsoft.EntityFrameworkCore;
using TicketTracker.Models;
using TicketTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Register PostgreSQL database
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Register TicketServices for Dependency Injection
builder.Services.AddScoped<TicketServices>();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.Run();