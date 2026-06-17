using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.ViewModels;

public class RegistroViewModel
{
    [Required]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Documento")]
    public string Documento { get; set; } = string.Empty;

    [Required]
    [Range(1, 120, ErrorMessage = "La edad debe ser válida")]
    [Display(Name = "Edad")]
    public int Edad { get; set; }

    [Required]
    [Display(Name = "Tipo de Usuario")]
    public string TipoUsuario { get; set; } = string.Empty; // "Docente" o "Estudiante"
}