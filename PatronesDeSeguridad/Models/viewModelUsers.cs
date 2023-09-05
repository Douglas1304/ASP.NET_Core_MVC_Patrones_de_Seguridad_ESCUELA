using Microsoft.AspNetCore.Identity;

namespace PatronesDeSeguridad.Models
{
    public class viewModelUsers
    {
        public List<IdentityUser>? Usuarios { get; set; }
        public List<IdentityUserRole<string>> rolesFk { get; set; }

        public List<IdentityRole> roles { get; set; }
    }
}
