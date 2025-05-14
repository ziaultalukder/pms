namespace PMS.ViewModel
{
    public class GetClientWiseMedicineForSalesViewModel
    {
        public int Sl { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
    }
}
