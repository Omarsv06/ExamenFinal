using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.ViewModels;

public class RegistroViewModel
{
    [Required]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Documento")]
    public string Documento { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Edad")]
    public int Edad { get; set; }

    [Required]
    [Display(Name = "Usuario")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Rol")]
    public string TipoUsuario { get; set; } = string.Empty;
}