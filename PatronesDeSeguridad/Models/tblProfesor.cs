using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatronesDeSeguridad.Models
{
    public class tblProfesor
    {
        //Identificador
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Numero de Identificación")]
        public string IdentificationNumber { get; set; }

        //Nombre
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        //Apellido1
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Apellido 1")]
        public string LastName1 { get; set; }
        //Apellido2
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = " Apellido 2")]
        public string LastName2 { get; set; }
        //Telefono
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        //Token
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Token")]
        public string Token { get; set; }
        //Estado
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado Profesor")]
        public bool EstadoProfesor { get; set; }

        public ICollection<tblMateriaCursoUsuario> MateriaCursoUsuarios { get; } = new List<tblMateriaCursoUsuario>();


    }
}
