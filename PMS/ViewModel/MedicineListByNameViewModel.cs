namespace PMS.ViewModel
{
    public class MedicineListByNameViewModel
    {
        public int SL { get; set; }
        public string ManufacturerName { get; set; } = string.Empty;
        public string BrandName { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
