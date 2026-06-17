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
        await _context.Database.EnsureCreatedAsync();

        if (!await _context.Docentes.AnyAsync())
        {
            var docente1 = new Docente
            {
                Nombre = "María Fernández",
                Documento = "DOC-1001",
                Especialidad = "Programación"
            };

            var docente2 = new Docente
            {
                Nombre = "Carlos Rojas",
                Documento = "DOC-1002",
                Especialidad = "Base de Datos"
            };

            var docente3 = new Docente
            {
                Nombre = "Ana Morales",
                Documento = "DOC-1003",
                Especialidad = "Desarrollo Web"
            };

            var docente4 = new Docente
            {
                Nombre = "Luis Vargas",
                Documento = "DOC-1004",
                Especialidad = "Redes y Comunicaciones"
            };

            var docente5 = new Docente
            {
                Nombre = "Sofía Pérez",
                Documento = "DOC-1005",
                Especialidad = "Inteligencia Artificial"
            };

            var docente6 = new Docente
            {
                Nombre = "Jorge Castillo",
                Documento = "DOC-1006",
                Especialidad = "Ciberseguridad"
            };

            var docente7 = new Docente
            {
                Nombre = "Elena García",
                Documento = "DOC-1007",
                Especialidad = "Ingeniería de Software"
            };

            var docente8 = new Docente
            {
                Nombre = "Ricardo Mendoza",
                Documento = "DOC-1008",
                Especialidad = "Análisis de Datos"
            };

            _context.Docentes.AddRange(
                docente1, docente2, docente3, docente4,
                docente5, docente6, docente7, docente8
            );
            
            await _context.SaveChangesAsync();

        }
    }
}