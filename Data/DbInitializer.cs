using AcademicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Data;

public class DbInitializer
{
    private readonly AppDbContext _context;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(AppDbContext context, ILogger<DbInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();

        if (!await _context.Docentes.AnyAsync())
        {
            var docentes = new List<Docente>
            {
                new Docente
                {
                    Nombre = "María Fernández",
                    Documento = "DOC-1001",
                    Especialidad = "Programación"
                },
                new Docente
                {
                    Nombre = "Carlos Rojas",
                    Documento = "DOC-1002",
                    Especialidad = "Base de Datos"
                },
                new Docente
                {
                    Nombre = "Ana Morales",
                    Documento = "DOC-1003",
                    Especialidad = "Desarrollo Web"
                },
                new Docente
                {
                    Nombre = "Luis Vargas",
                    Documento = "DOC-1004",
                    Especialidad = "Redes y Comunicaciones"
                },
                new Docente
                {
                    Nombre = "Sofía Pérez",
                    Documento = "DOC-1005",
                    Especialidad = "Inteligencia Artificial"
                },
                new Docente
                {
                    Nombre = "Jorge Castillo",
                    Documento = "DOC-1006",
                    Especialidad = "Ciberseguridad"
                },
                new Docente
                {
                    Nombre = "Elena García",
                    Documento = "DOC-1007",
                    Especialidad = "Ingeniería de Software"
                },
                new Docente
                {
                    Nombre = "Ricardo Mendoza",
                    Documento = "DOC-1008",
                    Especialidad = "Análisis de Datos"
                }
            };

            await _context.Docentes.AddRangeAsync(docentes);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Se crearon los docentes iniciales correctamente.");
        }
        else
        {
            _logger.LogInformation("Los docentes ya existen en la base de datos.");
        }
    }
}