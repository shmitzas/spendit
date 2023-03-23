namespace REST_API.Models.Goals
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Currency { get; set; }
    }
}
