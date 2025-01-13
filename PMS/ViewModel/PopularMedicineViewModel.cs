namespace PMS.ViewModel
{
    public class PopularMedicineViewModel
    {
        public int SL { get; set; }
        public string ManufacturerName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string Strength { get; set; } = string.Empty;
        public string DosageDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
