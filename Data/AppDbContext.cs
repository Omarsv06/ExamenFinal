using AcademicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Docente> Docentes => Set<Docente>();
    public DbSet<Curso> Cursos => Set<Curso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Documento).HasMaxLength(30).IsRequired();
            entity.HasDiscriminator<string>("TipoUsuario")
                  .HasValue<Estudiante>("Estudiante")
                  .HasValue<Docente>("Docente");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.Property(x => x.Matricula).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.Property(x => x.Especialidad).HasMaxLength(100).IsRequired();
            entity.HasMany(x => x.Cursos)
                  .WithOne(x => x.Docente)
                  .HasForeignKey(x => x.DocenteId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Materia).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Descripcion).HasMaxLength(500);
        });
    }
}
