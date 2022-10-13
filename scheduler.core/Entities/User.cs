using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace scheduler.core.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; } // pasar a enum (activo, inactivo, vacas)

        public int? RolId { get; set; }

        public virtual Rol Rol { get; set; }
    }

}
