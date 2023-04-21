namespace WebApp.Models.Budgets
{
    public class NewBudget
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Currency { get; set; }
    }
}
