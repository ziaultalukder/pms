namespace PMS.ViewModel
{
    public class RefundDetailsViewModel
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int SalesQty { get; set; }
        public int RefundQty { get; set; }
        public decimal TotalTaka { get; set; }
    }
}
