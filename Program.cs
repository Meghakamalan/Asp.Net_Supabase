using Microsoft.EntityFrameworkCore;
using Project_sem2.Models;
using Project_sem2.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<AppointmentDbContext>(options =>
//options.UseMySql("server=localhost;port=3306;database=AppointmentManager;user=root;password=root;",
//ServerVersion.AutoDetect("server=localhost;port=3306;database=AppointmentManager;user=root;password=root;")));


//this is for postgres database connection from supabase 
builder.Services.AddDbContext<AppointmentDbContext>(options =>
options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    o =>
    {
        o.CommandTimeout(60);
        o.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        );
    }
    )
);


    builder.Services.AddScoped<AppointmentService>();
    builder.Services.AddScoped<MassageService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
