using System.Linq.Expressions;
using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Repositories;

public class RepositorioGenerico<T> : IRepositorio<T> where T : class, IEntidad
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositorioGenerico(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> ObtenerPorIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AgregarAsync(T entidad)
    {
        await _dbSet.AddAsync(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(T entidad)
    {
        _dbSet.Update(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(T entidad)
    {
        _dbSet.Remove(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FiltrarAsync(
    Expression<Func<T, bool>> criterio,
    params Expression<Func<T, object>>[] includes)
    {
    IQueryable<T> query = _dbSet.AsNoTracking();
    foreach (var include in includes)
    {
        query = query.Include(include);
    }
    return await query.Where(criterio).ToListAsync();
    }
}
