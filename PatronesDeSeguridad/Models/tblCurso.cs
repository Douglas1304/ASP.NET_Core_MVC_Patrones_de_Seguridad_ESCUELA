using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatronesDeSeguridad.Models
{
    public class tblCurso
    {

        [Key]
        public int idCurso { get; set; }

        //Nombre 
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Nombre")]
        public string nombreCurso { get; set; }
        //Nivelfk
        [Display(Name = "Nivel")]
        public int LevelId { get; set; }
        [Display(Name = "Nivel")]
        public tblNivel Level { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado Curso")]
        public bool EstadoCurso { get; set; }
        public ICollection<tblMateriaCursoUsuario> MateriaCursoUsuarios { get; } = new List<tblMateriaCursoUsuario>();
    }
}
