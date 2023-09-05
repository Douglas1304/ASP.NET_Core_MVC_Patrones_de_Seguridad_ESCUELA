using System.ComponentModel.DataAnnotations;

namespace PatronesDeSeguridad.Models
{
    public class tblNivel
    {
        [Key]
        public int idNivel { get; set; }

        //Nombre Genero
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Nombre")]
        public string nombreNivel { get; set; }

        public ICollection<tblCurso> Cursos { get; } = new List<tblCurso>();
    }
}
