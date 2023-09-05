using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PatronesDeSeguridad.Models
{
    public class tblEstudiante
    {
        //Identificador
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Identificación")]
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
        [Display(Name = "Apellido 2")]
        public string LastName2 { get; set; }
        //Genero
        [Display(Name = "Género")]
        public int GenderId { get; set; }
        [Display(Name = "Género")]
        public tblGenero Gender { get; set; }
        //Fecha 
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        //Telefono
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Teléfono Emergencia")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        //Direccion
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Direccion")]
        public string direccion { get; set; }
     
        //Estado
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Estado")]
        public bool EstadoEstudiante { get; set; }

        public ICollection<tblMatriculaEstudiante> matriculaEstudiantes { get; } = new List<tblMatriculaEstudiante>();
    }
}
