using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.Repositories;
using Tutorial12.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApbdContext>(
    opt => 
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("Default")
            )
        );
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


