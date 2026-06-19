using AcademicManagementSystem.Models;
using AcademicManagementSystem.Repositories;
using AcademicManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization; //Controla quién puede acceder a determinadas partes de la aplicación.
using Microsoft.AspNetCore.Mvc; //Importa las funcionalidades principales del patrón MVC
using Microsoft.AspNetCore.Mvc.Rendering; //Permite preparar datos para controles visuales como listas desplegables en las vistas.

namespace AcademicManagementSystem.Controllers;

[Authorize]
public class CursosController : Controller
{
    private readonly IRepositorio<Curso> _cursoRepositorio;
    private readonly IRepositorio<Docente> _docenteRepositorio;

    public CursosController(
        IRepositorio<Curso> cursoRepositorio,
        IRepositorio<Docente> docenteRepositorio)
    {
        _cursoRepositorio = cursoRepositorio;
        _docenteRepositorio = docenteRepositorio;
    }

    public async Task<IActionResult> Index(string? materia, bool? disponibles)
    {
        var cursos = await _cursoRepositorio.FiltrarAsync(
            curso =>
                (string.IsNullOrWhiteSpace(materia) ||
                 curso.Materia.Contains(materia))
                &&
                (!disponibles.HasValue || curso.Disponible == disponibles.Value),
            curso => curso.Docente!
        );

        ViewBag.Materia = materia;
        ViewBag.Disponibles = disponibles;

        return View(cursos);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var cursos = await _cursoRepositorio.FiltrarAsync(
            c => c.Id == id,
            c => c.Docente!
        );

        var curso = cursos.FirstOrDefault();

        if (curso == null)
            return NotFound();

        return View(curso);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new CursoCreateViewModel();
        await CargarDocentesAsync(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CursoCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarDocentesAsync(viewModel);
            return View(viewModel);
        }

        await _cursoRepositorio.AgregarAsync(viewModel.ToEntity());

        TempData["MensajeExito"] = "Curso registrado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Registro");
        }

        var curso = await _cursoRepositorio.ObtenerPorIdAsync(id);

        if (curso == null)
            return NotFound();

        var viewModel = new CursoCreateViewModel
        {
            Id = curso.Id,
            Nombre = curso.Nombre,
            Materia = curso.Materia,
            Descripcion = curso.Descripcion,
            Disponible = curso.Disponible,
            DocenteId = curso.DocenteId
        };

        await CargarDocentesAsync(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CursoCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarDocentesAsync(viewModel);
            return View(viewModel);
        }

        var curso = await _cursoRepositorio.ObtenerPorIdAsync(viewModel.Id);

        if (curso == null)
            return NotFound();

        curso.Nombre = viewModel.Nombre;
        curso.Materia = viewModel.Materia;
        curso.Descripcion = viewModel.Descripcion;
        curso.Disponible = viewModel.Disponible;
        curso.DocenteId = viewModel.DocenteId;

        await _cursoRepositorio.ActualizarAsync(curso);

        TempData["MensajeExito"] = "Curso actualizado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Registro");
        }

        var curso = await _cursoRepositorio.ObtenerPorIdAsync(id);

        if (curso == null)
            return NotFound();

        return View(curso);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var curso = await _cursoRepositorio.ObtenerPorIdAsync(id);

        if (curso == null)
            return NotFound();

        await _cursoRepositorio.EliminarAsync(curso);

        TempData["MensajeExito"] = "Curso eliminado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> JsonCursos()
    {
        var cursos = await _cursoRepositorio.ObtenerTodosAsync(c => c.Docente!);

        var resultado = cursos.Select(c => new
        {
            c.Id,
            c.Nombre,
            c.Materia,
            c.Descripcion,
            c.Disponible,
            Docente = c.Docente != null ? c.Docente.Nombre : "Sin docente"
        });

        return Json(resultado);
    }

    private async Task CargarDocentesAsync(CursoCreateViewModel viewModel)
    {
        var docentes = await _docenteRepositorio.ObtenerTodosAsync();

        viewModel.Docentes = docentes
            .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Nombre
            })
            .ToList();
    }
}
