using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace PatronesDeSeguridad.Models
{
    public class tblMatriculaEstudiante
    {
        [Key]
        public int idMatriculaEstudiante { get; set; }

        //Estudiante
        [Display(Name = "Estudiante:")]
        public string EstudianteId { get; set; }
        [Display(Name = "Estudiante:")]
        public tblEstudiante Estudiante { get; set; }

        //Asignacion
        [Display(Name = "Asignación:")]
        public int AsignacionId { get; set; }
        [Display(Name = "Asignación:")]
        public tblMateriaCursoUsuario Asignacion { get; set; }
        //Fecha 
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Fecha Matricula")]
        public DateTime FechaMatricula { get; set; }
        //Estado
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado Matricula")]
        public bool EstadoMatricula { get; set; }

        //Fecha calificacion
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Fecha Calificacion")]
        public DateTime FechaCalificacion { get; set; }

        // Nueva calificacion
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Calificacion")]
        [Range(0, 100, ErrorMessage = "The calificacion must be between 0 and 100")]
        public double Calificacion { get; set; }

    }
}