using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace PatronesDeSeguridad.Models
{
    public class tblMateria
    {
        [Key]
        public int idMateria { get; set; }

        //Nombre Materia
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Nombre")]
        public string nombreMateria { get; set; }
        //Descripcion Materia
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Descripción")]
        public string descripcionMateria { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado Materia")]
        public bool EstadoMateria { get; set; }

        public ICollection<tblMateriaCursoUsuario> MateriaCursoUsuarios { get; } = new List<tblMateriaCursoUsuario>();
    }
}
