using AcademicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers;

public class PruebaController : Controller
{
    private readonly IValidacionRegistroService _validacion;

    public PruebaController(IValidacionRegistroService validacion)
    {
        _validacion = validacion;
    }

    public IActionResult Test(int edad)
    {
        try
        {
            var resultado = _validacion.ValidarEdad(edad);

            return Json(new
            {
                resultado.estado,
                resultado.mensaje
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                estado = false,
                mensaje = ex.Message
            });
        }
    }
}