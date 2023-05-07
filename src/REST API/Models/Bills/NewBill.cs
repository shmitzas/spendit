namespace REST_API.Models.Bills
{
    public class NewBill
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime DueDate { get; set; }
        public List<DateTime>? Reminders { get; set; }
    }
}
