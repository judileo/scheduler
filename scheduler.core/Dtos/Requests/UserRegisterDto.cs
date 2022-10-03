namespace scheduler.core.Dtos.Requests
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AdminKey { get; set; }
    }
}
