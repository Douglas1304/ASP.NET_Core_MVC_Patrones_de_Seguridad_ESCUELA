using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatronesDeSeguridad.Models;

namespace PatronesDeSeguridad.Data;

public class DbContext1 : IdentityDbContext<IdentityUser>
{
    public DbContext1(DbContextOptions<DbContext1> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Persona>()
            .Property(p => p.idPersona)
            .ValueGeneratedNever();

       // modelBuilder.Entity<tblCalificacion>()
       //.HasOne(c => c.Matricula)
       //.WithMany()
       //.HasForeignKey(c => c.MatriculaId);

       // modelBuilder.Entity<tblCurso>()
       //     .HasOne(c => c.Level)
       //     .WithMany()
       //     .HasForeignKey(c => c.LevelId);

       // modelBuilder.Entity<tblEstudiante>()
       //     .HasOne(e => e.Gender)
       //     .WithMany()
       //     .HasForeignKey(e => e.GenderId);

       // modelBuilder.Entity<tblMateriaCursoUsuario>()
       //     .HasOne(mc => mc.Curso)
       //     .WithMany()
       //     .HasForeignKey(mc => mc.CursoId);

       // modelBuilder.Entity<tblMateriaCursoUsuario>()
       //     .HasOne(mc => mc.Materia)
       //     .WithMany()
       //     .HasForeignKey(mc => mc.MateriaId);

       // modelBuilder.Entity<tblMateriaCursoUsuario>()
       //     .HasOne(mc => mc.Profesor)
       //     .WithMany()
       //     .HasForeignKey(mc => mc.ProfesorId);

       // modelBuilder.Entity<tblMatriculaEstudiante>()
       //     .HasOne(me => me.Estudiante)
       //     .WithMany()
       //     .HasForeignKey(me => me.EstudianteId);

       // modelBuilder.Entity<tblMatriculaEstudiante>()
       //     .HasOne(me => me.Asignacion)
       //     .WithMany()
       //     .HasForeignKey(me => me.AsignacionId);

    }
    public DbSet<Persona> persona { get; set; }
    public DbSet<tblCurso> tblCursos { get; set; }
    public DbSet<tblEstudiante> tblEstudiantes { get; set; }
    public DbSet<tblGenero> tblGeneros { get; set; }
    public DbSet<tblMateria> tblMaterias { get; set; }
    public DbSet<tblMateriaCursoUsuario> tblMateriasCursosUsuarios { get; set; }
    public DbSet<tblMatriculaEstudiante> tblMatriculaEstudiantes { get; set; }
    public DbSet<tblNivel> tblNiveles { get; set; }
    public DbSet<tblProfesor> tblProfesores { get; set; }
}
