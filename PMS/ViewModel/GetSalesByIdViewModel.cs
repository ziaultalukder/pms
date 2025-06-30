using PMS.Domain.Models;

namespace PMS.ViewModel
{
    public class GetSalesByIdViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal GrandTotal { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNo { get; set; }
        public List<SalesDetails> SalesDetails { get; set; }

    }
}
