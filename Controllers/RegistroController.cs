using AcademicManagementSystem.Models;
using AcademicManagementSystem.Services;
using AcademicManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers;

public class RegistroController : Controller
{
    private readonly IValidacionRegistroService _validacion;

    public RegistroController(IValidacionRegistroService validacion)
    {
        _validacion = validacion;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(RegistroViewModel model)
    {
        try
        {
            var resultado = _validacion.ValidarEdad(model.Edad);

            if (model.TipoUsuario == "Docente")
            {
                var docente = new Docente
                {
                    Nombre = model.Nombre,
                    Documento = model.Documento,
                    Especialidad = "General"
                };

                return Json(new { resultado, docente });
            }
            else
            {
                var estudiante = new Estudiante
                {
                    Nombre = model.Nombre,
                    Documento = model.Documento,
                    Matricula = "AUTO-001"
                };

                return Json(new { resultado, estudiante });
            }
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message });
        }
    }
}