using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace scheduler.core.Entities
{
    public class User : IdentityUser
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string State { get; set; } // pasar a enum (activo, inactivo, vacas)
        public bool IsAdmin { get; set; }
        public virtual UserRol UserRol{ get; set;}
    }
}
