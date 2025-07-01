using PMS.Domain.Models;

namespace PMS.ViewModel
{
    public class GetSalesByIdViewModel
    {
        public int Id { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal GrandTotal { get; set; }
        public string InvoiceNo { get; set; }
        public string IsRefundable { get; set; }
        public DateTime CreateDate { get; set; }
        public List<GetSalesDetailsByIdViewModel> SalesDetails { get; set; }

    }
}
