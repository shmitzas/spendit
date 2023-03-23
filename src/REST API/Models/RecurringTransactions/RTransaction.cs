namespace REST_API.Models.RecurringTransactions
{
    public class RTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Frequency { get; set; }
    }
}
