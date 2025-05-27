using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class GetSalesInfoForRefund : IRequest<GetSalesByInvoiceNo>
    {
        public string InvoiceNo { get; set; }
        public GetSalesInfoForRefund(string invoiceNo)
        {
            InvoiceNo = invoiceNo;
        }
    }

    public class GetSalesInfoForRefundHandler : IRequestHandler<GetSalesInfoForRefund, GetSalesByInvoiceNo>
    {
        private readonly ISalesService salesService;

        public GetSalesInfoForRefundHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<GetSalesByInvoiceNo> Handle(GetSalesInfoForRefund request, CancellationToken cancellationToken)
        {
            return await salesService.GetSaleByInvoice(request);
        }
    }
}
