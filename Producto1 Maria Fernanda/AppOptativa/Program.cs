using Microsoft.EntityFrameworkCore;
using AppOptativa.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DbContext con Neon (PostgreSQL) usando los picos 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Configurar CORS para permitir llamadas desde tu archivo HTML
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontendLocal", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// Habilitar la política de CORS
app.UseCors("PermitirFrontendLocal");

app.UseAuthorization();
app.MapControllers();

app.Run();