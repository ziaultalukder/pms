using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Configuration;

namespace PMS.ViewModel
{
    public class ClientWiseMedicineViewModel
    {
        public string BrandName { get; set; }=string.Empty;
        public string ManufacturerName { get; set; }=string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
    }
}
