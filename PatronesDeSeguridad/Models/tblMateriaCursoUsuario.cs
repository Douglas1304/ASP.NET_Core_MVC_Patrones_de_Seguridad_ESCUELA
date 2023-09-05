using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace PatronesDeSeguridad.Models
{
    public class tblMateriaCursoUsuario
    {
        [Key]
        public int idMateriaCursoUsuario{ get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Identificador Asignacion")]
        public string identificadorAsignacion { get; set; }
        //Cursofk
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        [Display(Name = "Curso")]
        public tblCurso Curso { get; set; }
        //Materiafk
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Materia")]
        public int MateriaId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Materia")]
        public tblMateria Materia { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //Usuariofk
        [Display(Name = "Profesor")]
        public string ProfesorId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Profesor")]
        public tblProfesor Profesor{ get; set; }
        //Estado
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado Asignacion")]
        public bool EstadoAsignacion { get; set; }

        public ICollection<tblMatriculaEstudiante> MatriculaEstudiantes { get; } = new List<tblMatriculaEstudiante>();
    }
}
