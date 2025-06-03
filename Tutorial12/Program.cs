using Microsoft.EntityFrameworkCore;
using Tutorial12;
using Tutorial12.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TravelDbContext>(
    opt => 
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("Default")
            )
        );
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


