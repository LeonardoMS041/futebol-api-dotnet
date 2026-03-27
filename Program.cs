using Microsoft.EntityFrameworkCore;
using futebolAPI.Data;
using futebolAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("ApiFootball", client =>
{
    client.BaseAddress = new Uri("https://v3.football.api-sports.io/");
    client.DefaultRequestHeaders.Add("x-apisports-key", "d91740fe81f4873caf425e26484be9b3");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<FootballApiService>();

// Adiciona o suporte a Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Mapeia os Controllers
app.MapControllers();

app.Run();