using MediatR;
using PMS.Application.Request.Sales.Query;
using PMS.Application.Request.Sales;
using PMS.ViewModel;

namespace PMS.Application.Request.Stock.Query
{
    public class GetStockInfoForRefund : IRequest<GetStockByInvoiceNo>
    {
        public string InvoiceNo { get; set; }
        public GetStockInfoForRefund(string invoiceNo)
        {
            InvoiceNo = invoiceNo;
        }
    }

    public class GetStockInfoForRefundHandler : IRequestHandler<GetStockInfoForRefund, GetStockByInvoiceNo>
    {
        private readonly IStockService _stockService;

        public GetStockInfoForRefundHandler(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task<GetStockByInvoiceNo> Handle(GetStockInfoForRefund request, CancellationToken cancellationToken)
        {
            return await _stockService.GetStockByInvoiceNo(request);
        }
    }
}
