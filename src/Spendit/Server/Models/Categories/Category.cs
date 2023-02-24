namespace Spendit.Server.Models.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
