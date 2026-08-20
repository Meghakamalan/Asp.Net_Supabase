using Microsoft.EntityFrameworkCore;
using TicketTracker.Models;
using TicketTracker.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the TicketDbContext to the services container
// This allows the application to use the TicketDbContext for database operations
// this line is configuring the TicketDbContext to use a PostgreSQL database provider with the connection string specified in the appsettings.json file under the "Default Connection" key. 
//This allows the application to connect to the PostgreSQL database and perform CRUD operations on the entities defined in the model.
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//this is called dependency injection, it allows us to inject the TicketDbContext into our controllers and services, so we can use it to interact with the database.
builder.Services.AddScoped<TicketServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
