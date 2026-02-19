# Pet Saving Backend

Proyecto de .net webapi para Pet Saving.

## Inicializar base de datos

1. Agregar migraciones

   ```bash
   dotnet ef migrations add Init
   ```

2. Crear carpeta db en la carpeta Data

3. Crear database.db

   ```bash
   dotnet ef database update
   ```

## Ejecutar

   ```bash
   dotnet run
   ```

## SwaggerUI
- http://localhost:5126/swagger/index.html