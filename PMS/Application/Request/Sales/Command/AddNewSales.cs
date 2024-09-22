using MediatR;
using PMS.Application.Request.Stock.Command;
using PMS.Application.Request.Stock;
using PMS.Helpers;

namespace PMS.Application.Request.Sales.Command
{
    public class AddNewSales : IRequest<Result>
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public List<SalesDetails> SalesDetails { get; set; }
        public AddNewSales(int id, string customerName, string contactNo, decimal totalTaka, int discountPercentage, decimal discountTaka, decimal subTotal, decimal grandTotal, List<SalesDetails> salesDetails)
        {
            Id = id;
            CustomerName = customerName;
            ContactNo = contactNo;
            TotalTaka = totalTaka;
            DiscountPercentage = discountPercentage;
            DiscountTaka = discountTaka;
            SubTotal = subTotal;
            GrandTotal = grandTotal;
            SalesDetails = salesDetails;

        }
    }

    public class AddNewSalesHandler : IRequestHandler<AddNewSales, Result>
    {
        private readonly ISalesService salesService;
        public AddNewSalesHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<Result> Handle(AddNewSales request, CancellationToken cancellationToken)
        {

            var result = await salesService.MedicineSales(request);
            return result;
        }
    }
}
