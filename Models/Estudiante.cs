using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models;

public class Estudiante : Usuario
{
    [Required]
    public string Matricula { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public override string MostrarDetalles()
    {
        return base.MostrarDetalles() + $" | Matrícula: {Matricula} | Activo: {Activo}";
    }
}
