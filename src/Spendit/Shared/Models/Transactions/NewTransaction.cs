namespace Spendit.Shared.Models.Transactions
{
    public class NewTransaction
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
