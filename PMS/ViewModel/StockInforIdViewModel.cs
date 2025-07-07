namespace PMS.ViewModel
{
    public class StockInforIdViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Invoice { get; set; }
        public decimal TotalPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal GrandTotal { get; set; }
        public string IsRefundable { get; set; }
        public IEnumerable<StockInDetailsforIdViewModel> StockInDetails { get; set; }

    }

    public class StockInDetailsforIdViewModel
    {
        public int Id { get; set; }
        public int StockInfoId { get; set; }
        public int MedicineId { get; set; }
        public string ManufacturerName { get; set; }
        public string BrandName { get; set; }
        public int NewQty { get; set; }
        public int RefundQty { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PruchasePrice { get; set; }
    }

}
