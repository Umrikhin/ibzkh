using ls.Data;
using ls.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Repository>(options => options.UseSqlServer(conn));

builder.Services.AddScoped<ITarifs, Tarifs>();
builder.Services.AddScoped<IRooms, Rooms>();
builder.Services.AddScoped<IProfits, Profits>();
builder.Services.AddMvc();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
