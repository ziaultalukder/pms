namespace PMS.Models
{
    public class InvoiceGenerator
    {
        public int Id { get; set; }
        public string StockInvoice { get; set; }
        public string SalesInvoice { get; set; }
        public int ClientId { get; set; }
    }
}
