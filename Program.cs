// Este es el punto de entrada principal de la aplicación API.
using PetSavingBackend.Data;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Databse conection
builder.Services.AddDbContext<ApplicationDBContext>(Options => Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Dependency Injection 
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Este es el punto donde se configura la canalización de solicitudes HTTP.
var app = builder.Build();

//Este bloque configura Swagger para el entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Show SwaggerUI shorcut in console
app.Lifetime.ApplicationStarted.Register(() =>
{
    var addresses = app.Urls;
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Swagger UI is available at {SwaggerUrl}", $"{addresses.First()}/swagger/index.html");
});

//Este middleware redirige las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

app.MapControllers();

//app.run inicia la aplicación y comienza a escuchar las solicitudes entrantes.
app.Run();