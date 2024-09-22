namespace PMS.Application.Request.Sales.Command
{
    public class SalesDetails
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
