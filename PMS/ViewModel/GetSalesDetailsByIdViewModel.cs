namespace PMS.ViewModel
{
    public class GetSalesDetailsByIdViewModel
    {
        public int Id { get; set; }
        public int SalesInfoId { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
        public decimal GrandTotal { get; set; }
        public string BrandName { get; set; }
        public string ManufacturerName { get; set; }
    }
}
