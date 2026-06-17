using AcademicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers;

[Route("Usuarios")]
public class UsuariosController : Controller
{
    private readonly IValidacionRegistroService _validacionRegistroService;

    public UsuariosController(IValidacionRegistroService validacionRegistroService)
    {
        _validacionRegistroService = validacionRegistroService;
    }

    [HttpGet("ValidarEdad/{edad:int}")]
    public IActionResult ValidarEdad(int edad)
    {
        try
        {
            var resultado = _validacionRegistroService.ValidarEdad(edad);
            return Json(new
            {
                valido = resultado.estado,
                mensaje = resultado.mensaje
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                valido = false,
                mensaje = ex.Message
            });
        }
    }
}
