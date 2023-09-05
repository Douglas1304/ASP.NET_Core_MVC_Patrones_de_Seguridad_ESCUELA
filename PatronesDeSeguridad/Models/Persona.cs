using System.ComponentModel.DataAnnotations;

namespace PatronesDeSeguridad.Models
{
    public class Persona
    {
        [Key]
        public string idPersona { get; set; }   
        public string NombrePersona { get; set;}
        public string IdUsuario { get; set; }
    }
}
