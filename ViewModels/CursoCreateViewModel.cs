using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.ViewModels;

public class CursoCreateViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nombre del curso")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Materia { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Disponible")]
    public bool Disponible { get; set; } = true;

    [Display(Name = "Docente")]
    [Required]
    public int DocenteId { get; set; }

    public List<SelectListItem> Docentes { get; set; } = new();

    public Curso ToEntity()
    {
        return new Curso
        {
            Nombre = Nombre,
            Materia = Materia,
            Descripcion = Descripcion,
            Disponible = Disponible,
            DocenteId = DocenteId
        };
    }
}
