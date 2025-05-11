using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Data;
using ProjetoProtech.API.Repositories; 
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();  

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseAuthorization();

app.MapControllers(); 
app.Run(); 
