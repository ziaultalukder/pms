namespace PMS.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string BillingCycle { get; set; } // Monthly, Yearly, etc.
        public bool IsActive { get; set; }
        public string SPlan { get; set; }
    }
}
