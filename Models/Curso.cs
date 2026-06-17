using System.ComponentModel.DataAnnotations;
namespace AcademicManagementSystem.Models;

public class Curso : IEntidad
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Materia { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public bool Disponible { get; set; } = true;

    [Required]
    public int DocenteId { get; set; }

    public Docente? Docente { get; set; }
}
