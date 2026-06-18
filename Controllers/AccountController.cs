using AcademicManagementSystem.Models;
using AcademicManagementSystem.Repositories;
using AcademicManagementSystem.Services;
using AcademicManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AcademicManagementSystem.Controllers;

public class AccountController : Controller
{
    private readonly IRepositorio<Usuario> _usuarioRepositorio;
    private readonly IValidacionRegistroService _validacionService;

    public AccountController(
        IRepositorio<Usuario> usuarioRepositorio,
        IValidacionRegistroService validacionService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _validacionService = validacionService;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var usuarios = await _usuarioRepositorio.FiltrarAsync(
            u => u.UserName == model.UserName &&
                 u.Password == model.Password
        );
        var usuario = usuarios.FirstOrDefault();
        if (usuario == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Usuario o contraseña incorrectos."
            );

            return View(model);
        }
        string rol = usuario is Docente
            ? "Docente"
            : "Estudiante";
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.UserName),
            new Claim(ClaimTypes.Role, rol)
        };
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            }
        );
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegistroViewModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(
        RegistroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            var resultado = _validacionService
                .ValidarEdad(model.Edad);

            Usuario usuario;
            if (model.TipoUsuario == "Docente")
            {
                usuario = new Docente
                {
                    Nombre = model.Nombre,
                    Documento = model.Documento,
                    Edad = model.Edad,
                    UserName = model.UserName,
                    Password = model.Password,
                    Especialidad = "Sin especialidad"
                };
            }
            else
            {
                usuario = new Estudiante
                {
                    Nombre = model.Nombre,
                    Documento = model.Documento,
                    Edad = model.Edad,
                    UserName = model.UserName,
                    Password = model.Password,
                    Matricula = "SIN-MATRICULA"
                };
            }
            await _usuarioRepositorio.AgregarAsync(usuario);

            TempData["MensajeExito"] =
                resultado.mensaje;

            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message
            );

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Login");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
}