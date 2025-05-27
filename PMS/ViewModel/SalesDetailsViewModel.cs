namespace PMS.ViewModel
{
    public class SalesDetailsViewModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalTaka { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
