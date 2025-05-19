namespace PMS.ViewModel
{
    public class GetStockViewModel
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public decimal TotalPrice { get; set; }
        public int DiscountPercentage { get; set;}
        public decimal DiscountTaka { get; set; }
        public decimal DiscountValue { get; set;}
    }
}
