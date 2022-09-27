using System;

namespace scheduler.core.Entities
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string State { get; set; } // pasar a enum (activo, inactivo, vacas)
        public string Rol { get; set; } // pasar a enum (estudiante, profe, admin)
    }

}
