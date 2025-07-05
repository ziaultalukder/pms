namespace PMS.ViewModel
{
    public class GetStockByInvoiceNo
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal GrandTotal { get; set; }
        public IEnumerable<GetStockDetailsViewModel> StockDetailsViewModels { get; set; }
    }

    public class GetStockDetailsViewModel
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string BrandName { get; set; }
        public int NewQty { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PruchasePrice { get; set; }
        
    }
    
    public class StockDetailsViewModelForRefund
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int ExistingQty { get; set; }
        public int RefundQty { get; set; }
    }
}
