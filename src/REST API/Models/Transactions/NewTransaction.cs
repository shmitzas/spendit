namespace REST_API.Models.Transactions
{
    public class NewTransaction
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
