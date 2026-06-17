namespace AcademicManagementSystem.Services;

public class ValidacionRegistroService : IValidacionRegistroService
{
    public (bool estado, string mensaje) ValidarEdad(int edad)
    {
        if (edad < 18)
        {
            throw new Exception("El usuario debe ser mayor o igual a 18 años.");
        }

        return (true, $"Validación aprobada. Edad registrada: {edad} años.");
    }
}
