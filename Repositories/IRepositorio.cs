using System.Linq.Expressions;
using AcademicManagementSystem.Models;

namespace AcademicManagementSystem.Repositories;

public interface IRepositorio<T> where T : class, IEntidad
{
    Task<IEnumerable<T>> ObtenerTodosAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(T entidad);
    Task ActualizarAsync(T entidad);
    Task EliminarAsync(T entidad);
    Task<IEnumerable<T>> FiltrarAsync(Expression<Func<T, bool>> criterio, params Expression<Func<T, object>>[] includes);
}
