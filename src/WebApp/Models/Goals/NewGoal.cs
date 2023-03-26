namespace WebApp.Models.Goals
{
    public class NewGoal
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
