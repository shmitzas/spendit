namespace Spendit.Server.Models.Users
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Settings { get; set; }
    }
}
