namespace PMS.ViewModel
{
    public class SalesReportViewModel
    {
        public int Sl { get; set; }
        public string ManufacturerName { get; set; }
        public string BrandName { get; set; }
        public decimal StockPrice { get; set; }
        public decimal SalesPrice { get; set;}
        public int Quantity { get; set; }
        public int RefundQty { get; set; }
        public decimal TotalTaka { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Reveniue { get; set; }
    }
}
