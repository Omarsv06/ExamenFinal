namespace AcademicManagementSystem.Services;

public interface IValidacionRegistroService
{
    (bool estado, string mensaje) ValidarEdad(int edad);
}
