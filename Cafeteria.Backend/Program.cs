using System.Text;
using Cafeteria.Backend.Data;
using Cafeteria.Backend.Models;
using Cafeteria.Backend.Repositorios;
using Cafeteria.Backend.Repositorios.Impl;
using Cafeteria.Backend.Services;
using Cafeteria.Backend.Services.Impl;
using Cafeteria.Backend.Token;
using Cafeteria.Backend.Token.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<CafeteriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//INYECCION DE DEPENDENCIAS

#region Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioServiceImpl>();
builder.Services.AddScoped<IProductoService, ProductoServiceImpl>();
builder.Services.AddScoped<ICategoriaService, CategoriaServiceImpl>();
#endregion

#region Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorioImpl>();
builder.Services.AddScoped<IProductosRepositorio, ProductoRepositorioImpl>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorioImpl>();
#endregion

#region Token
builder.Services.AddScoped<IGenerateToken, GenerateToken>();
#endregion

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

// Seed de datos iniciales
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CafeteriaDbContext>();
    context.Database.Migrate();

    if (!context.Roles.Any())
    {
        var rolAdmin = new Rol
        {
            Nombre = "Administrador",
            Descripcion = "Rol con acceso total al sistema",
            EsActivo = true
        };
        context.Roles.Add(rolAdmin);
        context.SaveChanges();

        var admin = new Usuario
        {
            Nombre = "Admin",
            Apellido = "Sistema",
            Correo = "admin@cafeteria.com",
            Clave = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            EsActivo = true,
            RolId = rolAdmin.Id
        };
        context.Usuarios.Add(admin);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
