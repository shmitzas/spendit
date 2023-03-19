namespace WebApp.Models.Users
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Settings { get; set; }
    }
}
