namespace Spendit.Server.Models.Budgets
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
