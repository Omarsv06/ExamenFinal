using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models;

public abstract class Usuario : IEntidad
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Documento { get; set; } = string.Empty;

    public virtual string MostrarDetalles()
    {
        return $"Id: {Id} | Nombre: {Nombre} | Documento: {Documento}";
    }
}
