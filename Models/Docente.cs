using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models;

public class Docente : Usuario
{
    public string Especialidad { get; set; } = string.Empty;

    public override string MostrarDetalles()
    {
        return base.MostrarDetalles() + $", Especialidad: {Especialidad}";
    }

    public List<Curso>? Cursos { get; set; }
}