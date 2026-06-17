using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models;

public class Docente : Usuario
{
    [Required]
    public string Especialidad { get; set; } = string.Empty;

    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public override string MostrarDetalles()
    {
        return base.MostrarDetalles() + $" | Especialidad: {Especialidad}";
    }
}
