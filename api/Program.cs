// Este es el punto de entrada principal de la aplicación API.
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Este es el punto donde se configura la canalización de solicitudes HTTP.
var app = builder.Build();

//Este bloque configura Swagger para el entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Este middleware redirige las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

//app.run inicia la aplicación y comienza a escuchar las solicitudes entrantes.
app.Run();