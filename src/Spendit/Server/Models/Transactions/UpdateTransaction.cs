namespace Spendit.Server.Models.Transactions
{
    public class UpdateTransaction
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
