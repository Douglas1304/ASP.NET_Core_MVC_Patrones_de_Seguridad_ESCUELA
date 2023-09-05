using System.ComponentModel.DataAnnotations;

namespace PatronesDeSeguridad.Models
{
    public class tblGenero
    {
        [Key]
        public int idGenero { get; set; }

        //Nombre Genero
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Genero")]
        public string nombreGenero { get; set; }

        public ICollection<tblEstudiante> Estudiantes { get; } = new List<tblEstudiante>();
    }
}
