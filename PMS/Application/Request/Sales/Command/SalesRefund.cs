using MediatR;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Command
{
    public class SalesRefund : IRequest<Result>
    {
        public int Id { get; set; }
        public decimal TotalTaka { get; set; }
        public int Discount { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal GrandTotal { get; set; }
        public IEnumerable<RefundDetailsViewModel> RefundDetails { get; set; }

        public SalesRefund(int id, decimal totalTaka, int discount, decimal discountTaka, decimal grandTotal, IEnumerable<RefundDetailsViewModel> refundDetails)
        {
            Id= id;
            TotalTaka= totalTaka;
            Discount= discount;
            DiscountTaka= discountTaka;
            GrandTotal= grandTotal;
            RefundDetails= refundDetails;
        }
    }

    public class SalesRefundHandler : IRequestHandler<SalesRefund, Result>
    {
        private readonly ISalesService salesService;

        public SalesRefundHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<Result> Handle(SalesRefund request, CancellationToken cancellationToken)
        {
            return await salesService.SalesRefund(request);
        }
    }
}
