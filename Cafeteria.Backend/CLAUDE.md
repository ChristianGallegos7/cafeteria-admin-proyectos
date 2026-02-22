# Cafeteria Backend - .NET

## Stack
- **Framework**: .NET 10 / ASP.NET Core
- **ORM**: Entity Framework Core
- **Base de datos**: SQL Server
- **Autenticación**: JWT Bearer
- **Hashing**: BCrypt

## Comandos
- `dotnet run` - Servidor desarrollo (puerto 5092)
- `dotnet build` - Compilar
- `dotnet ef migrations add <Nombre>` - Nueva migración
- `dotnet ef database update` - Aplicar migraciones

## Estructura
```
Cafeteria.Backend/
├── Controllers/      # Endpoints API
├── Services/         # Lógica de negocio (interfaces + impl)
├── Repositorios/     # Acceso a datos (interfaces + impl)
├── Models/           # Entidades de base de datos
├── Dtos/             # Data Transfer Objects
├── Data/             # DbContext
├── Token/            # Generación JWT
└── Migrations/       # Migraciones EF
```

## Reglas de C# (OBLIGATORIO)

### Tipado y nullabilidad
- SIEMPRE especificar tipos de retorno
- Usar nullable reference types (`string?` vs `string`)
- Validar nulls con pattern matching
- Usar `required` para propiedades obligatorias

```csharp
// MAL
public async Task Obtener(int id) { ... }

// BIEN
public async Task<UsuarioDto?> Obtener(int id) { ... }
```

### DTOs
- Separar Request y Response DTOs
- Usar records para DTOs inmutables cuando sea apropiado
- Nunca exponer entidades directamente

```csharp
public class LoginRequestDto
{
    public required string Correo { get; set; }
    public required string Clave { get; set; }
}
```

### Arquitectura en capas
```
Controller -> Service -> Repositorio -> DbContext
```
- Controllers: Solo reciben request y retornan response
- Services: Lógica de negocio, validaciones
- Repositorios: Queries a base de datos

### Inyección de dependencias
- Interfaces para Services y Repositorios
- Registrar en Program.cs con `AddScoped<>`
- Usar constructor injection

### Async/Await
- Métodos async retornan `Task<T>` o `Task`
- Sufijo `Async` en métodos (ObtenerUsuarioAsync)
- Usar `await` correctamente, evitar `.Result`

### Manejo de errores
- Retornar respuestas consistentes
- Usar `ActionResult<T>` en controllers
- Mensajes de error en español para el usuario

```csharp
if (usuario == null)
    return NotFound(new { mensaje = "Usuario no encontrado" });
```

## Convenciones de código
- Nombres de clases: PascalCase
- Métodos: PascalCase (IniciarSesion)
- Variables locales: camelCase
- Propiedades: PascalCase
- Interfaces: Prefijo I (IUsuarioService)
- Implementaciones: Sufijo Impl (UsuarioServiceImpl)

## Seguridad
- Contraseñas hasheadas con BCrypt
- JWT para autenticación
- Validar tokens en endpoints protegidos con `[Authorize]`
- CORS configurado para frontend

## Base de datos
- Migraciones para cambios de schema
- Seed de datos iniciales en Program.cs
- Usar transacciones para operaciones múltiples
