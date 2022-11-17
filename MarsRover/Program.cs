using MarsRover.Models;
using MarsRover.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<PlateauOptions>(
    builder.Configuration.GetSection("PlateauOptions"));

builder.Services.AddScoped<ISpaceVehicleInstructionParser, MarsRoverInstructionParser>();
builder.Services.AddScoped<IFormFileReader, InstructionsFormFileReader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MarsRover}/{action=Index}/{id?}");

app.Run();
